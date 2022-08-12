using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWave : MonoBehaviour
{
    [SerializeField] float speed = 2.0f;

    public bool goForward = false;
    public Vector3 Forward;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(goForward)
            transform.Translate(Forward*speed*Time.deltaTime);
    }

    public void SetWave(Vector3 waveStart, Vector3 waveEnd)
    {        
        var line = GetComponent<LineRenderer>();
        waveStart = transform.InverseTransformPoint(waveStart);
        waveEnd = transform.InverseTransformPoint(waveEnd);
        line.SetPositions(new Vector3[]{ waveStart, waveEnd });
    }
}
