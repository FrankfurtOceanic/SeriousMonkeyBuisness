using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ControllerShakeBySpeed))]
public class BananaBoomerangLauncher : MonoBehaviour
{
    ControllerShakeBySpeed vibration;

    bool isHolding;
    BananaBoomerang banana;
    Vector3 initialLocalPos;
    Quaternion initialLocalRot;
    // Start is called before the first frame update
    void Start()
    {
        vibration = GetComponent<ControllerShakeBySpeed>();
        banana = GetComponentInChildren<BananaBoomerang>();
        initialLocalPos = banana.transform.localPosition;
        initialLocalRot = banana.transform.localRotation;
        isHolding = true;
    }

    Vector3 avgVel;
    float avgRot;

    Vector3 lastTipPos;
    float lastRot;
    // Update is called once per frame
    void Update()
    {
        var curTipPos = transform.position;
        Vector3 delta = (curTipPos - lastTipPos)/Time.deltaTime;
        lastTipPos = curTipPos;
        avgVel = Vector3.Lerp(avgVel, delta, 0.5f);

        var curRot = transform.localRotation.eulerAngles.y;
        var deltaRot = Mathf.DeltaAngle(lastRot, curRot)/Time.deltaTime;
        lastRot = curRot;
        avgRot = Mathf.Lerp(avgRot, deltaRot, 0.5f);

        if (isHolding)
        {
            vibration.enabled = InputManager.Get(MonkeyKey.Fire);
            if (InputManager.GetUp(MonkeyKey.Fire))
            {
                banana.transform.parent = null;
                isHolding = false;
                banana.Throw(avgVel, avgRot, this.transform);
            }
        }
        else
        {
            vibration.enabled = true;
            if (banana.isReturning)
            {
                if (Vector3.Angle(banana.Velocity, transform.position - banana.transform.position) >= 90)
                {
                    isHolding = true;
                    banana.Catch();
                    banana.transform.parent = transform;
                    banana.transform.localPosition = initialLocalPos;
                    banana.transform.localRotation = initialLocalRot;
                }
            }
        }
    }
}
