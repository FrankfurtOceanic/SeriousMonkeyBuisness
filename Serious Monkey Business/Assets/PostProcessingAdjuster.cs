using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingAdjuster : MonoBehaviour
{
    [SerializeField] private VolumeProfile gameVolume;
    private ColorAdjustments colorAdjustments;
    private bool isDead;
    private float lerp;
    [SerializeField]  private float lerpSpeed = 1f;
    [Range(-100f, 100f)]
    [SerializeField] private float deadSaturation = 0f;

    private void Awake()
    {
        isDead = false;
        lerp = 0f;
        ColorAdjustments cA;
        if (gameVolume.TryGet<ColorAdjustments>(out cA))
        {
            colorAdjustments = cA;
            colorAdjustments.saturation.overrideState = true;
            colorAdjustments.saturation.value = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead && lerp<=1f)
        {
            lerp += Time.deltaTime*lerpSpeed;
            colorAdjustments.saturation.value = Mathf.Lerp(0f, deadSaturation, lerp);
        }
    }

    public void setIsDead()
    {
        isDead = true;
    }
}
