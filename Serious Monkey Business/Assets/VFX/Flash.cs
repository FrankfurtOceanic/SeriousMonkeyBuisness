using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    Renderer myRenderer;
    Color originalColor;

    public Color flashColor=Color.white;


    public float flashCooldown=1f, flashDecreaseRate=1f;
    float flash = 0;
    float currentFlashCooldown=0;

    public void FlashMe()
    {
        if (currentFlashCooldown > 0)
            return;

        currentFlashCooldown = flashCooldown;
        flash = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        originalColor = myRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        currentFlashCooldown -= Time.deltaTime;
        var color=originalColor;
        color.r = Mathf.Max(color.r, flash*flashColor.r);
        color.g = Mathf.Max(color.g, flash*flashColor.g);
        color.b = Mathf.Max(color.b, flash*flashColor.b);
        myRenderer.material.color = color;
        flash = Mathf.Max(0, flash - flashDecreaseRate * Time.deltaTime);
    }
}
