using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurretComponent 
{    float DPS { get; }
    float Cost { get; }
    float Range { get; }
    GameObject Blueprint { get; }
    EnemySpawner Spawner { get; }
}
