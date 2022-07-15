using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TurretRangedTargetting))]
public class LaserTurretBehaviour : MonoBehaviour, ITurretComponent
{
    [SerializeField] float DPS;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform gun;

    TurretRangedTargetting targetting;

    EnemyBehaviour m_EnemyBehaviour;
    Transform m_EnemyTransform;

    // Start is called before the first frame update
    void Start()
    {
        targetting = GetComponent<TurretRangedTargetting>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_EnemyBehaviour = targetting.Target;
        m_EnemyTransform = m_EnemyBehaviour?.transform;

        if(m_EnemyBehaviour == null)
        {
            lineRenderer.SetPositions(new Vector3[2]);
        }
        else
        {
            transform.LookAt(m_EnemyTransform);
            lineRenderer.SetPositions(new Vector3[] { Vector3.zero, transform.InverseTransformPoint(m_EnemyTransform.position) });
            DamageEnemy();
        }
    }

    void DamageEnemy()
    {
        m_EnemyBehaviour.TakeDamage(DPS * Time.deltaTime);
    }
}
