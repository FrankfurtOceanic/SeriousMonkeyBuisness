using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Gradient burnGradient;
    public float cooldownRate = 1;
    public float heatupRatio = 1;
    
    [SerializeField]
    Transform swordTip;
    
    TrailRenderer trailRenderer;
    Material swordMaterial;

    Vector3 lastTipPos;

    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        swordMaterial = GetComponent<Renderer>().material;
        
    }

    public float temperature = 0;
    
    // Update is called once per frame
    void Update()
    {
        trailRenderer.startWidth = transform.localScale.y * 2;
        var curTipPos = swordTip.position;
        var velocity = (curTipPos - lastTipPos).magnitude / Time.deltaTime;
        temperature = Mathf.Max(0, temperature - cooldownRate * temperature * Time.deltaTime);
        temperature = Mathf.Min(1, temperature + velocity * heatupRatio * Time.deltaTime);
        swordMaterial.SetColor("_EmissiveColor", burnGradient.Evaluate(temperature));
        lastTipPos = curTipPos;
    }
}
