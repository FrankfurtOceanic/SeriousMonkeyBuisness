using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject turret;

    public void spawn_turret()
    {
        Instantiate(turret);
    }
}

