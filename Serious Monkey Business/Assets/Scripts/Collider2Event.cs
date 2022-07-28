using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Converts onTRiggerEnter to a event
/// </summary>
public class Collider2Event : MonoBehaviour
{
    public event Action<Collider> EventTriggerEnter;
    public event Action<Collider> EventTriggerExit;
    public event Action<Collider> EventTriggerStay;

    private void OnTriggerEnter(Collider other)
    {
        EventTriggerEnter?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        EventTriggerExit?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        EventTriggerStay?.Invoke(other);
    }
}
