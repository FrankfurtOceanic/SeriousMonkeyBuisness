using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Round[] Rounds;
    
    [SerializeField] Transform startTransform;
    [SerializeField] Transform[] path;


    private LineRenderer lr;
    
    float roundTime;
    float waveTime;

    int roundIndex = 0;
    int waveIndex = 0;

    int waveCount = 0;
    int prefabIndex = 0;

    bool isStandby = false;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        SetUpLine();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isStandby)
            return;

        roundTime += Time.deltaTime;
        if(roundTime > Rounds[roundIndex].Waves[waveIndex].Duration)
        {
            roundTime = 0;
            waveIndex++;
            waveCount = 0;
            if(waveIndex > Rounds[roundIndex].Waves.Length-1)
            {
                waveIndex = 0;
                prefabIndex = 0;
                roundIndex++;
                if (roundIndex > Rounds.Length - 1)
                {
                    isStandby = true;
                    return;
                }
                SetUpLine();
            }
        }
        
        waveTime += Time.deltaTime;
        if (waveTime >= Rounds[roundIndex].Waves[waveIndex].Frequency && waveCount < Rounds[roundIndex].Waves[waveIndex].Count)
        {
            switch (Rounds[roundIndex].Waves[waveIndex].WaveType)
            {
                case WaveType.Simultaneous:
                    foreach(var enemyPrefab in Rounds[roundIndex].Waves[waveIndex].EnemyPrefabs)
                    {
                        var newEnemyS = Instantiate(enemyPrefab, startTransform.position, Quaternion.identity, transform);
                        var behaviorS = newEnemyS.GetComponent<EnemyBehaviour>();
                        behaviorS.path = path;
                        behaviorS.playerController = playerController;
                    }
                    break;
                case WaveType.Alternating:
                    var newEnemyA = Instantiate(Rounds[roundIndex].Waves[waveIndex].EnemyPrefabs[prefabIndex], startTransform.position, Quaternion.identity, transform);
                    var behaviorA = newEnemyA.GetComponent<EnemyBehaviour>();
                    behaviorA.path = path;
                    behaviorA.playerController = playerController;
                    prefabIndex++;
                    if(prefabIndex >= Rounds[roundIndex].Waves[waveIndex].EnemyPrefabs.Length)
                        prefabIndex = 0;
                    break;
                default:
                    break;
            }
            waveTime = 0;
            waveCount++;
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

[Serializable]
public struct Round
{
    public Wave[] Waves;
}

[Serializable]
public struct Wave
{
    public GameObject[] EnemyPrefabs;
    public float Frequency;
    public float Count;
    public float Duration;
    public WaveType WaveType;
}

[Serializable]
public enum WaveType
{
    Simultaneous = 0,
    Alternating
}
