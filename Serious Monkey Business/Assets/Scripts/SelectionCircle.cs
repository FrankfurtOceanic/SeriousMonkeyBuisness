using System.Collections.Generic;
using UnityEngine;

public class SelectionCircle : MonoBehaviour
{
    public float maxSize=1.5f;
    public float minSize=0f;
    public float distanceScale = 0.5f;
    public float lerpSpeed=6;
    public float padding=0.2f;
    public int leftMax=2,rightMax=2;

    public float childSize = 1;

    public int SelectionIndex { get; set; } = 0;

    Vector3 lineScale(float distance)
    {
        return Vector3.one* Mathf.Lerp(maxSize, minSize, Mathf.Abs(distance*distanceScale));
    }

    Vector3 line(float distance)
    {
        return Vector3.right * distance;
    }

    class Item
    {
        public Transform obj;
        //public Bounds _localbounds;
        public float width;
        public float linePos;

        public Transform original;
    }

    List<Item> children=new();

    public void AddChild(Transform child)
    {
        Bounds childBounds=new Bounds(child.position,Vector3.zero);
        foreach(Renderer r in child.GetComponentsInChildren<Renderer>())
        {
            childBounds.Encapsulate(r.bounds);
        }

        GameObject holder = new($"Holder for {child.name}");
        holder.transform.parent = transform;
        child.parent = holder.transform;
        child.localPosition = Vector3.zero;
        // scale holder so that child size is 1
      //  holder.transform.localScale = new Vector3(1/childBounds.size.x, 1,1)*childSize;
        children.Add(new() { 
            original=child,
            obj=holder.transform,
            width = childSize,
            linePos=float.NaN,
        });
    }

    void ScanChildren()
    {
        List<Transform> tmp=new();
        foreach(Transform child in transform)
        {
            tmp.Add(child);
        }
        foreach(var child in tmp)
            AddChild(child);
    }
    //List<Item> items;

    Item GetItem(int index)
    {
        return children[index];

    }

    public Transform SelectedItem => children[SelectionIndex].original;

    public int GetItemCount() => children.Count;

    private void Start()
    {
        //ScanChildren();
    }

    public void ScrollLeft()
    {
        SelectionIndex = (SelectionIndex +1) % GetItemCount();
    }

    public void ScrollRight()
    {
        SelectionIndex = (SelectionIndex + GetItemCount()-1) % GetItemCount();
    }

    // TODO this whole thing needs to be reworked
    void Update()
    {
        //testing code
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
          //  SelectionIndex--;


        //testing code
       // if (Input.GetKeyDown(KeyCode.RightArrow))
         //   SelectionIndex++;

        int N = GetItemCount();

        SelectionIndex = (SelectionIndex + N) % N;

        var centerObj = GetItem(SelectionIndex);
        float linePos = float.IsNaN(centerObj.linePos)?0: Mathf.LerpUnclamped(centerObj.linePos, 0, Time.deltaTime * lerpSpeed);

        centerObj.linePos = linePos;
        centerObj.obj.localPosition = line(linePos);
        centerObj.obj.gameObject.SetActive(true);
        centerObj.obj.localScale = lineScale(linePos);

        linePos -= centerObj.width / 2 ;

        int lidx = SelectionIndex;
        int cnt=0;
        while (true)
        {
            lidx--;
            linePos -= padding;
            cnt++;
            if (cnt > leftMax)
                break;

            Item item = GetItem((lidx+N)%N);
            linePos -= item.width;
            if (float.IsNaN(item.linePos))
                item.linePos = linePos;
            else
            item.linePos = Mathf.Lerp(item.linePos, linePos, 1);

            item.obj.localScale = lineScale(item.linePos);
            item.obj.position = line(item.linePos);
            item.obj.gameObject.SetActive(true);
        }

        linePos = centerObj.linePos + centerObj.width / 2;
        int ridx = SelectionIndex;
        cnt = 0;
        while (true)
        {
            ridx++;
            linePos += padding;
            cnt++;
            if (cnt > rightMax || ridx> lidx+N)
                break;

            Item item = GetItem((ridx+N)%N);
            linePos += item.width;
            
            if (float.IsNaN(item.linePos))
                item.linePos = linePos;
            else
            item.linePos = Mathf.Lerp(item.linePos, linePos,1);

            item.obj.localScale = lineScale(item.linePos);
            item.obj.position = line(item.linePos);
            item.obj.gameObject.SetActive(true);
        }

        for (int i = lidx + N; i >= ridx; i--)
        {
            Item item = GetItem((i+N)%N);
            item.linePos = float.NaN;
            item.obj.gameObject.SetActive(false);
        }
    }
}
