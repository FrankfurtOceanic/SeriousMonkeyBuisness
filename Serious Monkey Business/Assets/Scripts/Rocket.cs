using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject target;

    float speed = 0f;
    public float maxSpeed = 1f, accel = 1f;
    public float rotationSpeed = 1f;
    public float rotationSpeedMax=8f,rotationSpeedMin=0f;
    public float rotationSpeedChangeRate =1f, rotationSpeedBias=0.25f;
    public float gravityEffect = 1f;

    public ParticleStopper smoke;
    public GameObject explosion;

    public float DamageAmt;

    void Awake()
    {
    }

    private void OnTriggerEnter(Collider obj)
    {
        var enemy = obj.GetComponent<EnemyBehaviour>();
        if(enemy != null)
            Explosion();
    }
    
    private void Explosion()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2);
        foreach (var hitCollider in hitColliders)
        {
            var enemy = hitCollider.GetComponent<EnemyBehaviour>();
            enemy?.TakeDamage(9000);
        }
        smoke.StopAndDetach();
        Instantiate(explosion, transform.position, transform.rotation, null);
        Destroy(gameObject);
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            Explosion();
            return;
        }

        // gets faster at rotating as it goes
        rotationSpeed += (Random.value - 0.5f + rotationSpeedBias) * 2 * Time.deltaTime * rotationSpeedChangeRate;
        rotationSpeed = Mathf.Clamp(rotationSpeed, rotationSpeedMin, rotationSpeedMax);

        // rotate towards enemy
        Vector3 enemyDir = target.transform.position - transform.position;
        transform.forward = Vector3.Slerp(transform.forward, enemyDir, Time.deltaTime * rotationSpeed);

        //acceleration
        speed = Mathf.Min(maxSpeed, speed + accel * Time.deltaTime);

        //gets faster as it goes down,
        var delta = transform.forward * Time.deltaTime * speed;
        speed += -gravityEffect * delta.y;

        transform.position += delta;
    }
}
