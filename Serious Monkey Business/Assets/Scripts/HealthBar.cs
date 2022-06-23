using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Text healthText;
    
    public void SetHealthAmt(float hp)
    {
        healthText.text = $"HP: {hp}";
    }
}
