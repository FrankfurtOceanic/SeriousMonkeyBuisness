using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    Renderer renderer;
    Color originalColor;


    public float flashCooldown=1f, flashDecreaseRate=1f;
    float flash = 0;
    float currentFlashCooldown=0;

    public void FlashMe()
    {
        if (currentFlashCooldown > 0)
            return;

        currentFlashCooldown = flashCooldown;
        flash = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        currentFlashCooldown -= Time.deltaTime;
        var color=originalColor;
        color.r = Mathf.Max(color.r, flash);
        color.g = Mathf.Max(color.g, flash);
        color.b = Mathf.Max(color.b, flash);
        renderer.material.color = color;
        flash = Mathf.Max(0, flash - flashDecreaseRate * Time.deltaTime);
    }
}
