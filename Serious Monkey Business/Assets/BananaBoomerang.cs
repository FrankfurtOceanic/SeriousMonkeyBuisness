using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaBoomerang : MonoBehaviour
{
    public float initialVelocityMultiplier=2;
    public float initialRotMultiplier=2;

    float rotationSpeed;
    public Vector3 Velocity => _velocity * direction;
    public bool isReturning => state == State.Returning;

    float _velocity;
    Vector3 direction;

    Transform thrower;

    enum State
    {
        Held,
        GoingOut,
        Returning
    }

    State state=State.Held;
    Vector3 launchPoint;

    public void Throw(Vector3 velocity, float rotSpeed, Transform thrower)
    {
        this.thrower = thrower;
        state = State.GoingOut;
        _velocity = velocity.magnitude * initialVelocityMultiplier;
        direction = velocity.normalized;
        rotationSpeed = rotSpeed * initialRotMultiplier;
        launchPoint = velocity;
    }

    public void Catch()
    {
        state = State.Held;
    }

    public float gravity=1;

    void Update()
    {
        if (state != State.Held)
        {

            Vector3 directionToPlayer = transform.position - thrower.position;

            var distance = directionToPlayer.sqrMagnitude;
            _velocity -= gravity * Time.deltaTime / distance;

            if (_velocity <= 0)
                state = State.Returning;

            if (state == State.GoingOut)
            {
                transform.position += Velocity * Time.deltaTime;
            }
            else
            {
                transform.position += directionToPlayer.normalized * Time.deltaTime * _velocity;
            }
        }
    }
}
