using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Text healthText;
    public Image healthBar;
    public TMP_Text TMPhealthText;



    public float lerpSpeed= 3f;
    float lerpValue;
    float initialHealth;
    float currHealth;
    bool hasTakenDamage = false;


    private void Update()
    {
        lerpValue = lerpSpeed * Time.deltaTime;
        if (hasTakenDamage) fillBar();
    }

    private void fillBar()
    {
        
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currHealth / initialHealth, lerpValue);
        Debug.Log(currHealth);
        Debug.Log("initial health" + initialHealth);
      
    }

    public void SetHealthAmt(float hp, float initialHP)
    {
        if (!hasTakenDamage) hasTakenDamage = true;
        currHealth = hp;
        initialHealth = initialHP;
        healthText.text = $"HP: {hp}";
        TMPhealthText.SetText(hp.ToString("F0"));
    }

    
}
