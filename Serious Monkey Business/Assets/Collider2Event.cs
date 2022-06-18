using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Converts onTRiggerEnter to a event
public class Collider2Event : MonoBehaviour
{
    public event Action<Collider> EventTriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        EventTriggerEnter?.Invoke(other);
    }
}
