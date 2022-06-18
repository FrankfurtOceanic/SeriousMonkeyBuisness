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

    public void RefindTarget()
    {

    }

    private void Start()
    {
       foreach(var collider in GetComponentsInChildren<Collider2Event>())
        {
            collider.EventTriggerEnter += Collider_EventTriggerEnter;
        }
    }

    private void OnDestroy()
    {
        
       foreach(var collider in GetComponentsInChildren<Collider2Event>())
        {
            collider.EventTriggerEnter -= Collider_EventTriggerEnter;
        }
    }

    private void Collider_EventTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        var enemy=other.GetComponent<EnemyBehaviour>();
        if (enemy != null)
        {
            enemy.TakeDamage(DamageAmt);
        }

        smoke.StopAndDetach();
        Instantiate(explosion, transform.position, transform.rotation, null);

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        rotationSpeed += (Random.value - 0.5f + rotationSpeedBias) * 2 * Time.deltaTime * rotationSpeedChangeRate;
        rotationSpeed = Mathf.Clamp(rotationSpeed, rotationSpeedMin, rotationSpeedMax);
        Vector3 enemyDir = target.transform.position - transform.position;
        transform.forward = Vector3.Slerp(transform.forward,enemyDir ,Time.deltaTime * rotationSpeed);
        speed = Mathf.Min(maxSpeed, speed + accel * Time.deltaTime);
        var delta = transform.forward * Time.deltaTime * speed;
        speed += -gravityEffect * delta.y;
        transform.position += delta;
    }
}