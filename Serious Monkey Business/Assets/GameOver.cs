using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameOver : MonoBehaviour
{
    [SerializeField] PlayerController m_PlayerController;
    [SerializeField] Volume postProcessing;
    [SerializeField] VolumeProfile gameOverProfile;

    // Start is called before the first frame update
    void Awake()
    {
        m_PlayerController.HealthChanged += PlayerController_HealthChanged;
    }

    private void PlayerController_HealthChanged(float health)
    {
        if(health < 0)
        {
            GameIsOver();
        }
    }
    
    [ContextMenu("GameOver")]
    void GameIsOver()
    {
        Time.timeScale = 0.1f;
        postProcessing.profile = gameOverProfile;
    }
}
