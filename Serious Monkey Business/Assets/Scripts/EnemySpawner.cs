using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Round[] m_Rounds;

    [SerializeField] Transform m_StartTransform;
    [SerializeField] Transform[] m_Path;


    private LineRenderer m_LineRenderer;
    
    float m_RoundTime;
    float m_WaveTime;

    int m_RoundIndex = 0;
    int m_WaveIndex = 0;

    int m_WaveCount = 0;
    int m_PrefabIndex = 0;

    bool m_IsStandby = false;

    // Start is called before the first frame update
    void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        SetUpLine();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_IsStandby)
            return;

        m_RoundTime += Time.deltaTime;
        if(m_RoundTime > m_Rounds[m_RoundIndex].Waves[m_WaveIndex].Duration)
        {
            m_RoundTime = 0;
            m_WaveIndex++;
            m_WaveCount = 0;
            m_PrefabIndex = 0;
            if(m_WaveIndex >= m_Rounds[m_RoundIndex].Waves.Length)
            {
                m_WaveIndex = 0;
                m_RoundIndex++;
                if (m_RoundIndex >= m_Rounds.Length)
                {
                    m_IsStandby = true;
                    return;
                }
                SetUpLine();
            }
        }
        
        m_WaveTime += Time.deltaTime;
        if (m_WaveTime >= m_Rounds[m_RoundIndex].Waves[m_WaveIndex].Frequency && m_WaveCount < m_Rounds[m_RoundIndex].Waves[m_WaveIndex].Count)
        {
            switch (m_Rounds[m_RoundIndex].Waves[m_WaveIndex].WaveType)
            {
                case WaveType.Simultaneous:
                    foreach(var enemyPrefab in m_Rounds[m_RoundIndex].Waves[m_WaveIndex].EnemyPrefabs)
                    {
                        var newEnemyS = Instantiate(enemyPrefab, m_StartTransform.position, Quaternion.identity, transform);
                        var behaviorS = newEnemyS.GetComponent<EnemyBehaviour>();
                        behaviorS.path = m_Path;
                    }
                    break;
                case WaveType.Alternating:
                    var newEnemyA = Instantiate(m_Rounds[m_RoundIndex].Waves[m_WaveIndex].EnemyPrefabs[m_PrefabIndex], m_StartTransform.position, Quaternion.identity, transform);
                    var behaviorA = newEnemyA.GetComponent<EnemyBehaviour>();
                    behaviorA.path = m_Path;
                    m_PrefabIndex++;
                    if(m_PrefabIndex >= m_Rounds[m_RoundIndex].Waves[m_WaveIndex].EnemyPrefabs.Length)
                        m_PrefabIndex = 0;
                    break;
                default:
                    break;
            }
            m_WaveTime = 0;
            m_WaveCount++;
        }
    }

    void SetUpLine()
    {
        m_LineRenderer.positionCount = 1 + m_Path.Length;
        m_LineRenderer.SetPosition(0, m_StartTransform.position);
        for (int i = 0; i<m_Path.Length; i++)
        {
            m_LineRenderer.SetPosition(i + 1, m_Path[i].position);
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
