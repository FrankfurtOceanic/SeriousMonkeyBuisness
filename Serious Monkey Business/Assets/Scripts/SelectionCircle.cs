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
        public Bounds _localbounds;
        public float width=>_localbounds.size.y*obj.transform.localScale.y;
        public float linePos;
    }

    List<Item> children=new();

    void ScanChildren()
    {
        foreach(Transform child in transform)
        {
            children.Add(new()
            {
                obj = child,
                _localbounds = child.GetComponent<Renderer>().localBounds,
            });
        }
    }
    //List<Item> items;

    Item GetItem(int index)
    {
        return children[index];

    }

    public Transform SelectedItem => children[SelectionIndex].obj;

    int GetItemCount() => children.Count;

    float rotation;

    private void Start()
    {
        ScanChildren();
    }

    void Update()
    {
        //testing code
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            SelectionIndex--;


        //testing code
        if (Input.GetKeyDown(KeyCode.RightArrow))
            SelectionIndex++;

        int N = GetItemCount();

        SelectionIndex = (SelectionIndex + N) % N;

        var centerObj = GetItem(SelectionIndex);
        float linePos = Mathf.LerpUnclamped(centerObj.linePos, 0, Time.deltaTime * lerpSpeed);

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
            item.linePos = linePos;

            item.obj.localScale = lineScale(linePos);
            item.obj.position = line(linePos);
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
            if (cnt > rightMax || ridx+N>lidx)
                break;

            Item item = GetItem((ridx+N)%N);
            linePos += item.width;
            item.linePos = linePos;
            
            item.obj.localScale = lineScale(linePos);
            item.obj.position = line(linePos);
            item.obj.gameObject.SetActive(true);
        }

        for (int i = lidx + N; i >= ridx; i--)
        {
            Item item = GetItem((i+N)%N);
            item.obj.gameObject.SetActive(false);
        }
    }
}
