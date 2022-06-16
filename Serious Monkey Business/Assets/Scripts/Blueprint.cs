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
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.1)
        {
            building.SetActive(true);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 50000f, targetLayer))
            {
                building.transform.position = hit.point;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                {
                    Instantiate(turret, hit.point, Quaternion.identity);
                }
            }
        }
        else
        {
            building.SetActive(false);
        }
        
    }
}
