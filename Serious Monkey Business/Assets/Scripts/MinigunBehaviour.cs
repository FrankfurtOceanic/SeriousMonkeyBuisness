using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunBehaviour : MonoBehaviour
{
    [SerializeField] Transform minigunTransform;
    [SerializeField] Transform leftHandTransform;
    [SerializeField] Transform rightHandTransform;
    [SerializeField] ParticleSystem muzzleFlash;

    // Update is called once per frame
    void Update()
    {
        minigunTransform.position = ((leftHandTransform.position + rightHandTransform.position) / 2) - 0.02f*Vector3.one;
        var leftToRight = (rightHandTransform.position - leftHandTransform.position).normalized;
        minigunTransform.LookAt(rightHandTransform.position + 10*leftToRight - 0.02f*Vector3.one);
        
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            Fire();
            OVRInput.SetControllerVibration(0.05f, 0.1f, OVRInput.Controller.LTouch);
        }

    }

    void Fire()
    {
        muzzleFlash.Play();
    }
}
