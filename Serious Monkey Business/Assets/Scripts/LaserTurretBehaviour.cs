using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TurretRangedTargetting))]
public class LaserTurretBehaviour : MonoBehaviour, ITurretComponent
{    
    public float DPS { get { return m_DPS; } }
    public float Cost { get { return m_Cost; } }
    public float Range { get { return m_Range; } }
    public GameObject Blueprint  { get { return m_Blueprint; } }
    public EnemySpawner Spawner { get; set; }

    [SerializeField] float m_DPS;
    [SerializeField] float m_Cost;
    [SerializeField] float m_Range;
    [SerializeField] GameObject m_Blueprint;
    [SerializeField] LineRenderer m_LineRenderer;

    TurretRangedTargetting targetting;
    EnemyBehaviour m_EnemyBehaviour;
    Transform m_EnemyTransform;
    

    // Start is called before the first frame update
    void Awake()
    {
        targetting = GetComponent<TurretRangedTargetting>();
        targetting.TargetChanged += Targetting_TargetChanged;
        if(Spawner == null)
            Spawner = Object.FindObjectOfType<EnemySpawner>();
        targetting.Initialize(Range, Spawner);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(m_EnemyBehaviour == null)
        {
            m_LineRenderer.SetPositions(new Vector3[2]);
        }
        else
        {
            transform.LookAt(m_EnemyTransform);
            m_LineRenderer.SetPositions(new Vector3[] { Vector3.zero, transform.InverseTransformPoint(m_EnemyTransform.position) });
            DamageEnemy();
        }
    }

    void DamageEnemy()
    {
        m_EnemyBehaviour.TakeDamage(DPS * Time.deltaTime);
        if(m_EnemyBehaviour.HP < 0)
        {
            Destroy(m_EnemyBehaviour.gameObject);
        }
    }
    
    void Targetting_TargetChanged()
    {
        m_EnemyBehaviour = targetting.Target;
        m_EnemyTransform = m_EnemyBehaviour.transform;
    }
}
