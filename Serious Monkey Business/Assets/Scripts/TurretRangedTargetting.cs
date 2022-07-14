using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRangedTargetting : MonoBehaviour
{
    [SerializeField] float range;
   
    
    EnemySpawner m_Spawner;
    EnemyBehaviour m_EnemyBehaviour;
    Transform m_EnemyTransform;

    public EnemyBehaviour Target => m_EnemyBehaviour;
    public Action<EnemyBehaviour> TargetChanged;

    // Start is called before the first frame update
    void Start()
    {
        m_Spawner = FindObjectOfType<EnemySpawner>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(m_EnemyBehaviour == null)
        {
            TargetEnemy();
        }
        else if ((transform.position - m_EnemyTransform.position).magnitude > range)
        {
            TargetEnemy();
        }
    }

    void TargetEnemy()
    {
        m_EnemyBehaviour = null;
        m_EnemyTransform = null;
        float minDistance = range;

        foreach(Transform spawn in m_Spawner.transform)
        {
            float distance = (transform.position - spawn.position).magnitude;
            if(distance < minDistance)
            {
                m_EnemyTransform = spawn;
                m_EnemyBehaviour = spawn.GetComponent<EnemyBehaviour>();
                minDistance = distance;
            }
        }

        TargetChanged?.Invoke(m_EnemyBehaviour);
    }

}
