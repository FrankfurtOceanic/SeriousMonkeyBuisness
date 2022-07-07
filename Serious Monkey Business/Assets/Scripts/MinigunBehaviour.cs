using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunBehaviour : MonoBehaviour, Gun
{
    [SerializeField] ParticleSystem m_MuzzleFlash;

    Transform m_LeftHandTransform;
    Transform m_RightHandTransform;

    // temp hack so that player has minigun equiped at start
    private void Start()
    {
        var player=FindObjectOfType<PlayerController>();
        player.Equip(this);
    }

    void Update()
    {
        transform.position = ((m_LeftHandTransform.position + m_RightHandTransform.position) / 2) - 0.02f*Vector3.one;
        var leftToRight = (m_RightHandTransform.position - m_LeftHandTransform.position).normalized;
        transform.LookAt(m_RightHandTransform.position + 10*leftToRight - 0.02f*Vector3.one);
        
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            Fire();
            OVRInput.SetControllerVibration(0.05f, 0.1f, OVRInput.Controller.LTouch);
        }
    }

    public void Fire()
    {
        m_MuzzleFlash.Play();
    }

    public void EquipTo(PlayerController player)
    {
        m_LeftHandTransform = player.leftHand;
        m_RightHandTransform = player.rightHand;
    }
}
