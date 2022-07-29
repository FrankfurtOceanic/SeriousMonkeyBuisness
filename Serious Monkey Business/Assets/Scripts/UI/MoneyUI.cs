using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] TMP_Text TMPhealthText;

    public void SetMoney(float money)
    {
        TMPhealthText.text = money.ToString();
    }
}
