using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public PlayerController player;

    public HealthBar healthBar;

    public Text money;

    void Start()
    {
        player.HealthChanged += Player_healthChanged;
        player.MoneyChanged += Player_MoneyChanged;
    }

    private void OnDestroy()
    {
        player.HealthChanged -= Player_healthChanged;
        player.MoneyChanged -= Player_MoneyChanged;
    }

    private void Player_MoneyChanged(float obj)
    {
        money.text = $"Money: {obj}";
    }

    private void Player_healthChanged(float hp)
    {
        healthBar.SetHealthAmt(hp);
    }
}
