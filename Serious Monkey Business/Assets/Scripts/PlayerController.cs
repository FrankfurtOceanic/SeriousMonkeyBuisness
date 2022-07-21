using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public Transform leftHand;
    public Transform rightHand;

    public float initialHealth;
    public float initialMoney;

    float health;
    float money;

    public float Health
    {
        get => health; set
        {
            health = value;
            HealthChanged?.Invoke(value);
        }
    }

    public float Money
    {
        get => money; set
        {
            money = value;
            MoneyChanged?.Invoke(value);
        }
    }

    public event Action<float> HealthChanged;
    public event Action<float> MoneyChanged;

    public void Equip(Gun g)
    {
        g.EquipTo(this);
    }


    // Start is called before the first frame update
    void Start()
    {
        Health = initialHealth;
        Money = initialMoney;
    }

    int index = 0;

    // Update is called once per frame
    void Update()
    {


        /*
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger)&&OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            transform.position = godPosition.position;
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            index++;
            if (index >= spawnPositions.Length)
                index = 0;
            transform.position = spawnPositions[index].position;
        }
        else if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            index--;
            if (index < 0)
                index = spawnPositions.Length-1;
            transform.position = spawnPositions[index].position;
        }*/



    }
}
