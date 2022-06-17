using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject target;

    float speed = 0f;
    public float maxSpeed = 1f, accel = 1f;
    public float rotationSpeed = 1f;
    public float gravityEffect = 1f;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        Vector3 enemyDir = target.transform.position - transform.position;
        transform.forward = Vector3.Slerp(transform.forward,enemyDir ,Time.deltaTime * rotationSpeed);
        speed = Mathf.Min(maxSpeed, speed + accel * Time.deltaTime);
        var delta = transform.forward * Time.deltaTime * speed;
        speed += -gravityEffect * delta.y;
        transform.position += delta;
    }
}
