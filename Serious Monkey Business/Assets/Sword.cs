using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [GradientUsage(hdr:true)]
    public Gradient burnGradient;
    public float cooldownRate = 1;
    public float heatupRatio = 1;
    public float maxVelocity = 2;
    public float rotSpeed = 1;

    public float DPS = 1;

    [SerializeField]
    Transform swordTip;

    [SerializeField]
    Transform glowPart;

    [SerializeField]
    Transform swordParent;

    TrailRenderer trailRenderer;
    Material swordMaterial;
    Renderer swordRenderer;

    Vector3 lastTipPos;

    [SerializeField] ParticleSystem hitSpark;

    List<ParticleSystem> hitSparkCache = new(); //reuse previously instatiated hit sparks


    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        swordRenderer = glowPart.GetComponent<Renderer>();
        swordMaterial = GetComponent<Renderer>().material;

        trailRenderer.startWidth = swordRenderer.bounds.size.y * 2;

        foreach (var collider in GetComponentsInChildren<Collider2Event>())
        {
            collider.EventCollisionStay += Collider_EventCollisionStay;
        }
    }

    private void OnDestroy()
    {
        foreach (var collider in GetComponentsInChildren<Collider2Event>())
        {
            collider.EventCollisionStay -= Collider_EventCollisionStay;
        }
    }

    private void Collider_EventCollisionStay(Collision collision)
    {
        var obj = collision.gameObject;
        var enemy = obj.GetComponent<EnemyBehaviour>(); //TODO performance concerns?
        if (enemy != null)
        {
            enemy.TakeDamage(DPS * Time.deltaTime);

            for (int i = 0; i < collision.contactCount - hitSparkCache.Count; i++)
                hitSparkCache.Add(Instantiate(hitSpark, swordParent));

            for (int i = 0; i < collision.contactCount; i++)
            {
                hitSparkCache[i].transform.position = collision.GetContact(i).point;
                hitSparkCache[i].Play();
            }
        }
    }

    public float temperature = 0;
    public float rotThres = 5;

    // Update is called once per frame
    void Update()
    {
        var curTipPos = swordTip.position;
        Vector3 delta = curTipPos - lastTipPos;
        var velocity = delta.magnitude / Time.deltaTime;
        var velocityPercent = Mathf.Min(1, velocity/maxVelocity);
        if (velocity > rotThres)
        {
            var direction = transform.InverseTransformDirection(delta);
            direction.y = 0;

            swordParent.localRotation.SetLookRotation(direction, Vector3.up);
        }
        OVRInput.SetControllerVibration(velocityPercent * 10, velocityPercent * 10, OVRInput.Controller.RHand);
        temperature = Mathf.Max(0, temperature - cooldownRate * temperature * Time.deltaTime);
        temperature = Mathf.Min(1, temperature + velocity * heatupRatio * Time.deltaTime);
        swordRenderer.material.SetColor("_Emission", burnGradient.Evaluate(temperature));
        lastTipPos = curTipPos;
    }
}
