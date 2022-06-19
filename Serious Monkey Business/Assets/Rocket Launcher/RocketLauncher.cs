using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public Transform reloadHand;
    public Transform mainHandle, reloadHandle;


    public GameObject Projectile;
    private Transform mainHand;

    public Transform MainHand
    {
        get => mainHand; 
        set
        {
            mainHand = value;

            //make this a child of the hand
            transform.parent = value;

            if (value == null)
                return;

            //match handle position to hand (sus)
            transform.localPosition = -mainHandle.localPosition;
            transform.localRotation = Quaternion.FromToRotation(mainHandle.forward, mainHand.forward);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
