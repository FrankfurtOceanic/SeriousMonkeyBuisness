using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Blueprint : MonoBehaviour
{
    RaycastHit hit;
    public GameObject prefab;
    public GameObject turret;
    private GameObject blueprintPrefab;
    [SerializeField] LayerMask targetLayer;
    [Range(0f, 1f)]
    public float TriggerThreshold = 0.1f;
    [Range(0f, 1f)]
    public float getDownThreshhold = 0.7f;
    bool Building = false;

    LineRenderer line;


    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        blueprintPrefab = Instantiate(prefab);

        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 50000f, targetLayer))
        {
            blueprintPrefab.transform.position = hit.point;
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        /*if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) < getDownThreshhold)
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
        }*/

        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            if (Building)
            {
                Building = false; 
                blueprintPrefab.SetActive(false);
            }
            else 
            {
                Building = true;
                 blueprintPrefab.SetActive(true);
            }


        }
        
        if (Building)
        {
            line.SetPositions(new Vector3[]{transform.position, transform.position});
           
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, float.MaxValue, targetLayer))
            {
                blueprintPrefab.transform.position = hit.point;
                line.SetPosition(1, hit.point);

                if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
                {
                    Instantiate(turret, hit.point, Quaternion.identity);
                }
                
            }
        }
        

    }
}
