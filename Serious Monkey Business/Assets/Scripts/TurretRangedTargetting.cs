using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRangedTargetting : MonoBehaviour
{
    public EnemyBehaviour Target;
    public event Action TargetChanged;

    float m_Range;
    EnemySpawner m_Spawner;
    Transform m_EnemyTransform;

    // Start is called before the first frame update
    void Start()
    {
        var turret = GetComponent<ITurretComponent>();
        Initialize(turret.Range, FindObjectOfType<EnemySpawner>());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Target == null)
        {
            TargetEnemy();
        }
        else if ((transform.position - m_EnemyTransform.position).magnitude > m_Range)
        {
            TargetEnemy();
        }
    }

    void TargetEnemy()
    {
        Target = null;
        m_EnemyTransform = null;
        float minDistance = m_Range;

        foreach(Transform spawn in m_Spawner.transform)
        {
            float distance = (transform.position - spawn.position).magnitude;
            if(distance < minDistance)
            {
                m_EnemyTransform = spawn;
                Target = spawn.GetComponent<EnemyBehaviour>();
                minDistance = distance;
            }
        }

        if(Target != null)
            TargetChanged?.Invoke();
    }

    public void Initialize(float range, EnemySpawner spawner)
    {
        m_Range = range;
        m_Spawner = spawner;
    }
}
