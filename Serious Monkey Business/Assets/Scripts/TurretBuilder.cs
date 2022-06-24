using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TurretBuilder : MonoBehaviour
{
    RaycastHit hit;

    //these two are temporary and will be gone when the player turret selection is in place
    public GameObject blueprintPrefab;
    public GameObject turretPrefab;

    [SerializeField] LayerMask targetLayer;
    [Range(0f, 1f)]
    public float TriggerThreshold = 0.1f;
    [Range(0f, 1f)]
    public float getDownThreshhold = 0.7f;

    //these store the actual current turret type to build
    private GameObject currentTurret;
    private GameObject instantiatedBlueprint;
    private TurretInfo currentTurretInfo;

    bool Building = false;

    LineRenderer line;
    PlayerController player;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        player = GetComponentInParent<PlayerController>(true);
    }


    // Start is called before the first frame update
    void Start()
    {
        SetCurrentTurret(blueprintPrefab, turretPrefab);
    }

    public void SetCurrentTurret(GameObject blueprint, GameObject actual)
    {
        currentTurretInfo = actual.GetComponent<TurretInfo>();
        instantiatedBlueprint = Instantiate(blueprint);
        currentTurret = actual;
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 50000f, targetLayer))
        {
            instantiatedBlueprint.transform.position = hit.point;
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
                instantiatedBlueprint.SetActive(false);
            }
            else 
            {
                Building = true;
                 instantiatedBlueprint.SetActive(true);
            }


        }
        
        if (Building)
        {
            line.SetPositions(new Vector3[]{transform.position, transform.position});
           
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, float.MaxValue, targetLayer))
            {
                instantiatedBlueprint.transform.position = hit.point;
                line.SetPosition(1, hit.point);

                if(currentTurretInfo.cost > player.Money)
                {
                    if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
                    {
                        player.Money -= currentTurretInfo.cost;
                        Instantiate(currentTurret, hit.point, Quaternion.identity);
                    }
                }
                else
                {
                    //TODO make blueprint red if not enuf money
                }
                
            }
        }
        

    }
}
