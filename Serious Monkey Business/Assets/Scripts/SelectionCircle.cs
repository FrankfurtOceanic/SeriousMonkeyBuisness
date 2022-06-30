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
        return Vector3.one * Mathf.Lerp(maxSize, minSize, Mathf.Abs(distance * distanceScale));
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
    bool itemsLayout=false;
    public void AddChild(Transform child)
    {
        Bounds childBounds=new Bounds(child.position,Vector3.zero);
        foreach (Renderer r in child.GetComponentsInChildren<Renderer>())
        {
            childBounds.Encapsulate(r.bounds);
        }

        GameObject holder = new($"Holder for {child.name}");
        holder.transform.parent = transform;
        holder.transform.localPosition = Vector3.zero;
        
        child.parent = holder.transform;
        child.localScale =Vector3.one*( childSize/  childBounds.size.y) ;
        child.localPosition = Vector3.zero;
        
        itemsLayout = false;
        children.Add(new()
        {
            original = child,
            obj = holder.transform,
            width = childSize,
            linePos = float.NaN,
        });
        
    }

   public void LayoutItems()
    {
        float linePos=0;
        foreach (var child in children)
        {
            linePos += child.width;
            child.linePos = linePos;
            linePos += child.width / 2 + padding;
        }
        itemsLayout = true;
    }

    public void ScanChildren()
    {
        List<Transform> tmp=new();
        foreach (Transform child in transform)
        {
            tmp.Add(child);
        }
        foreach (var child in tmp)
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
        SelectionIndex = (SelectionIndex + 1) % GetItemCount();
    }

    public void ScrollRight()
    {
        SelectionIndex = (SelectionIndex + GetItemCount() - 1) % GetItemCount();
    }

    float curLinePos=0;

    // TODO this whole thing needs to be reworked
    void Update()
    {
        if (!itemsLayout)
            LayoutItems();

        int N = GetItemCount();
        curLinePos = Mathf.Lerp(curLinePos, children[SelectionIndex].linePos, Time.deltaTime * lerpSpeed);

        for (int i = 0; i < N; i++)
        {
            Item item=GetItem(i);
            if (i < SelectionIndex - leftMax || i > SelectionIndex + rightMax)
            {
                item.obj.gameObject.SetActive(false);
            }
            else
            {
                item.obj.gameObject.SetActive(true);
                var relLinePos = item.linePos - curLinePos;
                item.obj.localPosition = line(relLinePos);
                item.obj.localScale = lineScale(relLinePos);
            }
        }
    }
}
