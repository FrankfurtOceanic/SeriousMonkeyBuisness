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

    public HealthBar healthBar;
    public MoneyUI moneyUI;

    float health;
    float money;

    [SerializeField] GameObject m_Gun;
    [SerializeField] GameObject m_Sword;

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
        HealthChanged += PlayerController_HealthChanged;
        MoneyChanged += PlayerController_MoneyChanged;

        selectedWeaponIndex = 0;
    }

    private void OnDestroy()
    {
        HealthChanged -= PlayerController_HealthChanged;
        MoneyChanged -= PlayerController_MoneyChanged;
    }

    private void PlayerController_HealthChanged(float hp)
    {
        healthBar.SetHealthAmt(hp, initialHealth);
    }
    private void PlayerController_MoneyChanged(float m)
    {
        moneyUI.SetMoney(m);
    }

    int index = 0;

    int selectedWeaponIndex{
        get => index;
        set {
            index = value;
            for (int i = 0; i < weapons.Count; i++)
                weapons[i].SetActive(i == index);

        }
    } 
    
    public List<GameObject> weapons;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.GetDown(MonkeyKey.SwitchWeapon))
        {
            selectedWeaponIndex = (selectedWeaponIndex + 1) % weapons.Count;
        }

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
