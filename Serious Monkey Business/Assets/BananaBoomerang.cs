using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaBoomerang : MonoBehaviour
{
    public float initialVelocityMultiplier=2;
    public float initialRotMultiplier=2;

    float rotationSpeed;
    public Vector3 Velocity;// => _velocity * direction;
    public bool isReturning => state == State.Returning;

    // float _velocity;
    //Vector3 direction;

    Transform thrower;

    enum State
    {
        Held,
        GoingOut,
        Returning
    }

    State state=State.Held;
    Vector3 launchPoint;
    Quaternion initialRot;

    private void Start()
    {
        trail = GetComponent<TrailRenderer>();
    }

    public void Throw(Vector3 velocity, float rotSpeed, Transform thrower)
    {
        this.thrower = thrower;
        state = State.GoingOut;
        //  _velocity = velocity.magnitude * initialVelocityMultiplier;
        //direction = velocity.normalized;
        Velocity = velocity * initialVelocityMultiplier;
        rotationSpeed = 720;
        initialRot = transform.rotation;
        launchPoint = velocity;
        trail.enabled = true;
        maxDistToPlayer = 0;
    }

    public void Catch()
    {
        trail.enabled = false;
        state = State.Held;
    }

    public float gravity=1;
    private TrailRenderer trail;

    float maxDistToPlayer;

    void Update()
    {
        if (state != State.Held)
        {
            transform.rotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);

            Vector3 directionToPlayer =thrower.position- transform.position ;

            //var distance = directionToPlayer.sqrMagnitude;
            Velocity += gravity * Time.deltaTime * directionToPlayer.normalized;

            var curDist=directionToPlayer.magnitude;

            if (state == State.GoingOut)
            {
                if (curDist < maxDistToPlayer-0.1)
                    state = State.Returning;
                maxDistToPlayer = Mathf.Max(maxDistToPlayer, curDist);
            }
            transform.position += Velocity * Time.deltaTime;
            // else
            //{
            // transform.position += directionToPlayer.normalized * Time.deltaTime * _velocity;
            //}
        }
    }
}
