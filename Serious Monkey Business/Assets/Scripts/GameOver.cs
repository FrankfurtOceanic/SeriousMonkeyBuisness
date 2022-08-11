using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] PlayerController m_PlayerController;
    [SerializeField] Volume postProcessing;
    [SerializeField] VolumeProfile gameOverProfile;
    [SerializeField] PostProcessingAdjuster PPA;

    // Start is called before the first frame update
    void Awake()
    {
        m_PlayerController.HealthChanged += PlayerController_HealthChanged;
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;
        }
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
        //postProcessing.profile = gameOverProfile;
        PPA.setIsDead();
    }
}
