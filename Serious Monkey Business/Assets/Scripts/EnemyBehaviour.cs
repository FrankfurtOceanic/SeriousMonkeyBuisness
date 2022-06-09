using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform[] path;
    public float speed = 0.1f;

    int index = 0;
    Vector3 startPosition;
    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if((transform.position - path[index].position).magnitude < 0.1f)
        {
            startPosition = path[index].position;
            index++;
        }
        if(index < path.Length)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / (path[index].position - startPosition).magnitude;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startPosition, path[index].position, fractionOfJourney);
        }
    }
}
