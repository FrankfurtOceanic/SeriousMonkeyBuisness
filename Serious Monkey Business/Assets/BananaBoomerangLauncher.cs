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
    public float maxDistance=20;
    // Update is called once per frame
    void Update()
    {
        var curTipPos = transform.position;
        Vector3 delta = (curTipPos - lastTipPos)/Time.deltaTime;
        lastTipPos = curTipPos;
        avgVel = Vector3.Lerp(avgVel, delta, 0.1f);

        var curRot = transform.localRotation.eulerAngles.y;
        var deltaRot = Mathf.DeltaAngle(lastRot, curRot)/Time.deltaTime;
        lastRot = curRot;
        avgRot = Mathf.Lerp(avgRot, deltaRot, 0.2f);

        if (isHolding)
        {
            if (InputManager.GetDown(MonkeyKey.Fire))
                vibration.enabled = true;
            if (InputManager.GetUp(MonkeyKey.Fire))
            {
                banana.transform.parent = null;
                isHolding = false;
                banana.Throw(avgVel, avgRot, this.transform);
                vibration.enabled = false;
            }
        }
        else
        {
            Vector3 bananaDistance = transform.position - banana.transform.position;
            var velocityPercent = bananaDistance.magnitude/maxDistance;
            OVRInput.SetControllerVibration(velocityPercent * 10, velocityPercent * 10, OVRInput.Controller.RHand);
            if (banana.isReturning)
            {
                if (Vector3.Angle(banana.Velocity, bananaDistance) >= 90)
                {
                    OVRInput.SetControllerVibration(0, 0);
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
