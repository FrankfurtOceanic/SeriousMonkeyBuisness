using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TurretBuilder : MonoBehaviour
{
    RaycastHit hit;

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

    [SerializeField] SelectionCircle turretSelector;



    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        player = GetComponentInParent<PlayerController>(true);
    }

    [SerializeField] Material blueprintMaterial;

    void MakeBlueprint(GameObject actual)
    {
        foreach (Renderer r in actual.GetComponentsInChildren<Renderer>())
            r.material = blueprintMaterial;

        DisableFunctionality(actual);
    }

    void DisableFunctionality(GameObject turret)
    {
        foreach (MonoBehaviour r in turret.GetComponentsInChildren<ITurretComponent>())
        {
            r.enabled = false;
        }
    }

    public List<GameObject> turrets;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var turret in turrets)
        {
            var copy = Instantiate(turret);
            DisableFunctionality(copy);
            turretSelector.AddChild(copy.transform);
        }
    }

    public void SetCurrentTurret( GameObject actual)
    {
        currentTurretInfo = actual.GetComponent<TurretInfo>();
        instantiatedBlueprint = Instantiate(currentTurretInfo.blueprint);
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

            turretSelector.gameObject.SetActive(Building);
        }
        
        if (Building)
        {
            if (OVRInput.GetDown(OVRInput.Button.Left))
            {
                turretSelector.ScrollLeft();
                SetCurrentTurret(turrets[turretSelector.SelectionIndex]);
            }else if (OVRInput.GetDown(OVRInput.Button.Right))
            {
                turretSelector.ScrollRight();
                SetCurrentTurret(turrets[turretSelector.SelectionIndex]);
            }

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
