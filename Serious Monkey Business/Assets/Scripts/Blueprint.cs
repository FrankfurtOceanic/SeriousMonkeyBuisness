using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour
{
    RaycastHit hit;
    public GameObject prefab;
    public GameObject turret;
    private GameObject building;
    [SerializeField] LayerMask targetLayer;
    [Range(0f, 1f)]
    public float TriggerThreshold = 0.1f;
    [Range(0f, 1f)]
    public float getDownThreshhold = 0.7f;
    bool released;
    // Start is called before the first frame update
    void Start()
    {
        building = Instantiate(prefab);

        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 50000f, targetLayer))
        {
            transform.position = hit.point;
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) < getDownThreshhold)
        {
            released = true;
        }
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > TriggerThreshold)
        {
            building.SetActive(true);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 50000f, targetLayer))
            {
                building.transform.position = hit.point;

                if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > getDownThreshhold && released)
                {
                    Instantiate(turret, hit.point, Quaternion.identity);
                    released = false;
                }
            }
        }
        else
        {
            building.SetActive(false);
        }
        
    }
}
