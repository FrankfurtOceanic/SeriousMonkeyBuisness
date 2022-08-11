using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform[] path;
    public float speed = 3;

    public float HP = 100;
    public float MaxHP=100;

    public float damage = 100;
    public float money = 100;
    public PlayerController playerController;


    int index = 0;
    Vector3 startPosition;
    float startTime;
    Flash flash;

    //public Color liveColor = new Color(0.8f,0.8f,0.8f), deadColor=new Color(1f, 1f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startTime = Time.time;
        flash = GetComponentInChildren<Flash>();
        transform.LookAt(path[index].position);
    }
    

    public void TakeDamage(float dmg)
    {
        HP -= dmg;

        //flash.flashColor = Color.Lerp(liveColor, deadColor, 1-Mathf.Pow(HP/MaxHP, 0.9f));
        flash.FlashMe();
        if(HP < 0)
        {
            playerController.Money += money;
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if((transform.position - path[index].position).magnitude < 0.1f)
        {
            var targettedTower = path[index].GetComponentInChildren<EnemyExit>();
            targettedTower?.TakeDmg(damage);

            startPosition = path[index].position;
            startTime = Time.time;
            index++;
            if(index < path.Length)
                transform.LookAt(path[index].position);
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
        else
        {
            Destroy(gameObject);
        }
    }
}
