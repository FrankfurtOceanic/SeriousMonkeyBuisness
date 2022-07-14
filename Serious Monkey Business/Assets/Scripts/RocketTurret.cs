using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TurretRangedTargetting))]
public class RocketTurret : MonoBehaviour, ITurretComponent
{
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

    private TurretRangedTargetting targetting;

    // Start is called before the first frame update
    void Start()
    {
        targetting = GetComponent<TurretRangedTargetting>();
    }

    float h,v;

    // Update is called once per frame
    void Update()
    {
        if (targetting.Target != null)
        {
            Vector3 targetDir = targetting.Target.transform.position - vRotation.position;

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
                        rocketSpawned.target = targetting.Target.gameObject;
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
