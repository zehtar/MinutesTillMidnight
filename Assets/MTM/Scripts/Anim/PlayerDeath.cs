using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
    public GameObject deathPanel;
    private bool isFading = false;
    
    public InfGameTimer infGameTimer;
    public GameTimer gameTimer;
    public GameStats gameStats;
    public player player;

    public void TriggerDeathScreen()
    {
        gameTimer = FindObjectOfType<GameTimer>();
        gameStats = FindObjectOfType<GameStats>();
        if (gameTimer == null)
        {
            infGameTimer = FindAnyObjectByType<InfGameTimer>();
            infGameTimer.StopTimer();
        }
        else
        {
            gameTimer.StopTimer();
        }

        gameStats.isRunning = false;
        player = FindObjectOfType<player>();
        this.transform.position = player.transform.position;
    }

    public void TriggerReviveScreen()
    {
        gameTimer = FindObjectOfType<GameTimer>();
        gameStats = FindObjectOfType<GameStats>();
        gameTimer.ResumeTimer();
        gameStats.isRunning = true;
        deathPanel.SetActive(false);
    }

    public void EffectOff()
    {
        deathPanel.SetActive(true);
        gameStats.CoutingAwards();
    }
}
