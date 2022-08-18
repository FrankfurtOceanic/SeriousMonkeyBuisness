using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaBoomerang : MonoBehaviour
{
    public float initialVelocityMultiplier;
    public float initialRotMultiplier;

    float rotationSpeed;
    public Vector3 Velocity;
    public bool isReturning=>state==State.Returning;

    enum State
    {
        Held,
        GoingOut,
        Returning
    }

    State state=State.Held;

    public void Throw(Vector3 velocity, float rotSpeed){
        state = State.GoingOut;
        Velocity = velocity * initialVelocityMultiplier;
        rotationSpeed = rotSpeed * initialRotMultiplier;
    }

    public void Catch(){
        state = State.Held;
    }

    // Update is called once per frame
    void Update()
    {
        if (state != State.Held)
        {
        }
    }
}
