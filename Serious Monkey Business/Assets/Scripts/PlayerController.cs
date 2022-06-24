using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform godPosition;
    [SerializeField] Transform[] spawnPositions;

    public Transform leftHand;
    public Transform rightHand;

    public float health;
    public float money;

    public event Action<float> HealthChanged;

    public void TakeDmg(float dmg)
    {
        health -= dmg;
        HealthChanged?.Invoke(health);
    }

    public void Equip(Gun g)
    {
        g.EquipTo(this);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    int index = 0;

    // Update is called once per frame
    void Update()
    {


        
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
        }

    }
}
