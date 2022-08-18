
using UnityEngine;

public class ControllerShakeBySpeed : MonoBehaviour
{
    public float maxVelocity = 500;
    public Transform swordTip;
    public OVRInput.Controller hand=OVRInput.Controller.RHand;
    Vector3 lastTipPos;
    // Update is called once per frame
    void Update()
    {
        var curTipPos = swordTip.position;
        Vector3 delta = curTipPos - lastTipPos;
        var velocity = delta.magnitude / Time.deltaTime;
        var velocityPercent = Mathf.Min(1, velocity/maxVelocity);
        OVRInput.SetControllerVibration(velocityPercent * 10, velocityPercent * 10, hand);
        lastTipPos = curTipPos;
    }

    private void OnDisable()
    {
        OVRInput.SetControllerVibration(0, 0, hand);
    }
}
