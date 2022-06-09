using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float freq;
    [SerializeField] Transform startTransform;
    [SerializeField] Transform[] path;

    float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= freq)
        {
            var newEnemy = Instantiate(enemyPrefab, startTransform.position, Quaternion.identity);
            var behavior = newEnemy.AddComponent<EnemyBehaviour>();
            behavior.path = path;
            time = 0;
        }
    }
}
