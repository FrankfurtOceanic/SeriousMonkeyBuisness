using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Round[] Rounds;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float freq;
    [SerializeField] Transform startTransform;
    [SerializeField] Transform[] path;


    private LineRenderer lr;
    
    float roundTime;
    float waveTime;

    int roundIndex = 0;
    int waveIndex = 0;

    int waveCount = 0;
    int prefabIndex = 0;

    bool isStandy = false;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        SetUpLine();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isStandy)
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
                roundIndex++;
                if (roundIndex > Rounds.Length - 1)
                {
                    isStandy = true;
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
                    var xPosition = 0;
                    foreach(var enemyPrefab in Rounds[roundIndex].Waves[waveIndex].EnemyPrefabs)
                    {
                        var newEnemyS = Instantiate(enemyPrefab, Rounds[roundIndex].StartTransform.position + new Vector3(0, 0, prefabIndex), Quaternion.identity, transform);
                        var behaviorS = newEnemyS.GetComponent<EnemyBehaviour>();
                        behaviorS.path = Rounds[roundIndex].Path;
                        xPosition++;
                    }
                    break;
                case WaveType.Alternating:
                    var newEnemyA = Instantiate(Rounds[roundIndex].Waves[waveIndex].EnemyPrefabs[prefabIndex], Rounds[roundIndex].StartTransform.position, Quaternion.identity, transform);
                    var behaviorA = newEnemyA.GetComponent<EnemyBehaviour>();
                    behaviorA.path = Rounds[roundIndex].Path;
                    prefabIndex = prefabIndex >= Rounds[roundIndex].Waves[waveIndex].EnemyPrefabs.Length - 1 ? 1 : prefabIndex + 1;
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
        lr.positionCount = 1 + Rounds[roundIndex].Path.Length;
        lr.SetPosition(0, Rounds[roundIndex].StartTransform.position);
        for (int i = 0; i<Rounds[roundIndex].Path.Length; i++)
        {
            lr.SetPosition(i + 1, Rounds[roundIndex].Path[i].position);
        }
    }
}

[Serializable]
public struct Round
{
    public Transform StartTransform;
    public Transform[] Path;
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
