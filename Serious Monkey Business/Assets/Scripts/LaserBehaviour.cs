using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    [SerializeField] float DPS;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform gun;
    [SerializeField] float range;

    EnemySpawner m_Spawner;
    EnemyBehaviour m_EnemyBehaviour;
    Transform m_EnemyTransform;

    // Start is called before the first frame update
    void Start()
    {
        m_Spawner = Object.FindObjectOfType<EnemySpawner>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(m_EnemyBehaviour == null)
        {
            lineRenderer.SetPositions(new Vector3[2]);
            TargetEnemy();
        }
        else if ((transform.position - m_EnemyTransform.position).magnitude > range)
        {
            TargetEnemy();
        }
        else
        {
            transform.LookAt(m_EnemyTransform);
            lineRenderer.SetPositions(new Vector3[] { Vector3.zero, transform.InverseTransformPoint(m_EnemyTransform.position) });
            DamageEnemy();
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
