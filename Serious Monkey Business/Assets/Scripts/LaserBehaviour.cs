using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    [SerializeField] float DPS;
    [SerializeField] LineRenderer lineRenderer;

    EnemySpawner m_Spawner;
    EnemyBehaviour m_EnemyBehaviour;
    Transform m_EnemyTransform;

    // Start is called before the first frame update
    void Start()
    {
        m_Spawner = Object.FindObjectOfType<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_EnemyBehaviour == null)
        {
            TargetEnemy();
        }
        else
        {
            lineRenderer.SetPositions(new Vector3[] { transform.position, m_EnemyTransform.position });
            DamageEnemy();
        }
    }

    void TargetEnemy()
    {
        m_EnemyBehaviour = null;
        m_EnemyTransform = null;
        float minDistance = int.MaxValue;

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

    }

    void DamageEnemy()
    {
        m_EnemyBehaviour.TakeDamage(DPS * Time.deltaTime);
        if(m_EnemyBehaviour.HP < 0)
        {
            Destroy(m_EnemyBehaviour.gameObject);
            TargetEnemy();
        }
    }
}
