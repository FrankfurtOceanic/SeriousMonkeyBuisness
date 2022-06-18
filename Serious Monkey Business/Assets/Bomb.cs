using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float DamageAmt;

    public GameObject Explosion;

    public GameObject ToDestroy;

    private void OnTriggerEnter(Collider other)
    {
        var enemy=other.GetComponent<EnemyBehaviour>();
        if (enemy != null)
        {
            enemy.TakeDamage(DamageAmt);
        }

        Instantiate(Explosion, transform.position, transform.rotation, null);

        Destroy(ToDestroy);
    }
}
