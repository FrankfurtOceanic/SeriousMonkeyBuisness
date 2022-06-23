using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The exit where the enemies go
/// </summary>
public class PlayerBase : MonoBehaviour
{
    public PlayerController owner;
    private Flash flash;

    private void Awake()
    {
        flash = GetComponentInChildren<Flash>();
    }

    public void TakeDmg(float health)
    {
        owner.TakeDmg(health);
        flash.FlashMe();
    }
}
