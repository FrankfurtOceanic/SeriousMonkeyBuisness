using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunBehaviour : MonoBehaviour, Gun
{
    Transform leftHandTransform;
    Transform rightHandTransform;
    [SerializeField] ParticleSystem muzzleFlash;

    // temp hack so that player has minigun equiped at start
    private void Start()
    {
        var player=FindObjectOfType<PlayerController>();
        player.Equip(this);
    }

    void Update()
    {
        transform.position = ((leftHandTransform.position + rightHandTransform.position) / 2) - 0.02f*Vector3.one;
        var leftToRight = (rightHandTransform.position - leftHandTransform.position).normalized;
        transform.LookAt(rightHandTransform.position + 10*leftToRight - 0.02f*Vector3.one);
        
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            Fire();
            OVRInput.SetControllerVibration(0.05f, 0.1f, OVRInput.Controller.LTouch);
        }
    }

    public void Fire()
    {
        muzzleFlash.Play();
    }

    public void EquipTo(PlayerController player)
    {
        leftHandTransform = player.leftHand;
        rightHandTransform = player.rightHand;
    }
}
