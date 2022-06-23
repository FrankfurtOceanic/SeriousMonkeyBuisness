using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    public PlayerController player;

    public HealthBar healthBar;

    void Start()
    {
        player.HealthChanged += Player_healthChanged;   
    }

    private void OnDestroy()
    {
        player.HealthChanged -= Player_healthChanged;
    }

    private void Player_healthChanged(float hp)
    {
        healthBar.SetHealthAmt(hp);
    }
}
