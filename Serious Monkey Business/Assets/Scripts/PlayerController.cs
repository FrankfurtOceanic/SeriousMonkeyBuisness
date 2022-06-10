using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform godPosition;
    [SerializeField] Transform[] spawnPositions;

    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger)&&OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            transform.position = godPosition.position;
        }
        else if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            index++;
            if (index >= spawnPositions.Length)
                index = 0;
            transform.position = spawnPositions[index].position;
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            index--;
            if (index < 0)
                index = spawnPositions.Length-1;
            transform.position = spawnPositions[index].position;
        }

    }
}
