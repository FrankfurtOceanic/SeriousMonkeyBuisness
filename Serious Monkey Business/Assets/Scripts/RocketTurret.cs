using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TurretRangedTargetting))]
public class RocketTurret : MonoBehaviour, ITurretComponent
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

    [SerializeField] Transform hRotation;
    [SerializeField] Transform vRotation;

    /// <summary>
    /// Degrees per second
    /// </summary>
    [SerializeField] float vRotationSpeed = 45;
    [SerializeField] float hRotationSpeed = 45;

    /// <summary>
    /// Max degrees difference in dirrection allowed before firing
    /// </summary>
    [SerializeField] float maxAngleForFire = 20;

    private TurretRangedTargetting m_Targeting;

    // Start is called before the first frame update
    void Awake()
    {
        m_Targeting = GetComponent<TurretRangedTargetting>();
        if(Spawner == null)
            Spawner = Object.FindObjectOfType<EnemySpawner>();
        m_Targeting.Initialize(Range, Spawner);
    }

    float h,v;

    // Update is called once per frame
    void Update()
    {
        if (m_Targeting.Target != null)
        {
            Vector3 targetDir = m_Targeting.Target.transform.position - vRotation.position;

            var targetRot = Quaternion.LookRotation(targetDir);

            float targetH = targetRot.eulerAngles.y;
            float targetV = targetRot.eulerAngles.x;

            h = Mathf.MoveTowardsAngle(h, targetH, vRotationSpeed * Time.deltaTime);
            v = Mathf.MoveTowardsAngle(v, targetV, hRotationSpeed * Time.deltaTime);

            v = Mathf.Clamp(v, 0, 90);

            hRotation.localRotation = Quaternion.Euler(0, h, 0);
            vRotation.localRotation = Quaternion.Euler(v, 0, 0);

            if (rocket != null)
            {
                float now=  Time.realtimeSinceStartup;
                if (now - lastFireTime > reloadTime)
                {
                    if (now - lastFireTime > trackingTimeout || 
                        (Mathf.DeltaAngle(targetH, h) < maxAngleForFire &&
                        Mathf.DeltaAngle(targetV, v) < maxAngleForFire) )
                    {
                        lastFireTime = now;
                        Rocket rocketSpawned = Instantiate(rocket, rocketSpawnPos);
                        rocketSpawned.target = m_Targeting.Target.gameObject;
                        rocketSpawned.transform.parent = null;
                    }
                }
            }
        }
    }

    [SerializeField] float reloadTime = 1;
    [SerializeField] float trackingTimeout = 10;
    [SerializeField] Rocket rocket;
    [SerializeField] Transform rocketSpawnPos;

    float lastFireTime = 0;
}
