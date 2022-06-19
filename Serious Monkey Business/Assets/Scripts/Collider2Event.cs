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

    private void OnTriggerEnter(Collider other)
    {
        EventTriggerEnter?.Invoke(other);
    }
}
