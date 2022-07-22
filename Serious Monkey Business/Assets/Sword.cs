using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [GradientUsage(hdr:true)]
    public Gradient burnGradient;
    public float cooldownRate = 1;
    public float heatupRatio = 1;
    
    [SerializeField]
    Transform swordTip;

    [SerializeField]
    Transform swordMesh;
    
    TrailRenderer trailRenderer;
    Material swordMaterial;
    Renderer swordRenderer;

    Vector3 lastTipPos;

    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        swordRenderer = swordMesh.GetComponent<Renderer>();
        //swordMaterial = GetComponent<Renderer>().material;
        
        trailRenderer.startWidth = swordMesh.GetComponent<Renderer>().bounds.size.y * 2;
    }

    public float temperature = 0;
    
    // Update is called once per frame
    void Update()
    {
        var curTipPos = swordTip.position;
        var velocity = (curTipPos - lastTipPos).magnitude / Time.deltaTime;
        temperature = Mathf.Max(0, temperature - cooldownRate * temperature * Time.deltaTime);
        temperature = Mathf.Min(1, temperature + velocity * heatupRatio * Time.deltaTime);
        swordRenderer.material.SetColor("_Emission", burnGradient.Evaluate(temperature));
        lastTipPos = curTipPos;
    }
}
