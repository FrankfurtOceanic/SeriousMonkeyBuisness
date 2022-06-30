using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionCircleTest : MonoBehaviour
{
    SelectionCircle select;
    // Start is called before the first frame update
    void Start()
    {
        select = GetComponent<SelectionCircle>();
        select.ScanChildren();
    }

    // Update is called once per frame
    void Update()
    {

        //testing code
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            select.ScrollLeft();


        //testing code
        if (Input.GetKeyDown(KeyCode.RightArrow))
            select.ScrollRight();

    }
}
