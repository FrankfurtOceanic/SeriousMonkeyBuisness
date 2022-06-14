using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float freq;
    [SerializeField] Transform startTransform;
    [SerializeField] Transform[] path;
    private LineRenderer lr;

    float time;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        SetUpLine();
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= freq)
        {
            var newEnemy = Instantiate(enemyPrefab, startTransform.position, Quaternion.identity, transform);
            var behavior = newEnemy.AddComponent<EnemyBehaviour>();
            behavior.path = path;
            time = 0;
        }
    }
    public void SetUpLine()
    {
        lr.positionCount = 1 + path.Length;
        lr.SetPosition(0, startTransform.position);
        for (int i = 0; i<path.Length; i++)
        {
            lr.SetPosition(i + 1, path[i].position);
        }
    }
}
