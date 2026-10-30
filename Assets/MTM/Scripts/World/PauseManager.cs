using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public FireEnableDisable fireStop;
    public GameObject pausePanel;
    
    public void Start()
    {
        fireStop = GetComponent<FireEnableDisable>();
        pausePanel = GameObject.Find("PausePanel");
    }

    public void PauseGame() // остановка всех врагов, движение игрока, пуль и тд
    {
        Time.timeScale = 0;
        if (fireStop == null)
        {
            fireStop = GetComponent<FireEnableDisable>();
        }
        fireStop.DisableFire();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        if (fireStop == null)
        {
            fireStop = GetComponent<FireEnableDisable>();
        }
        fireStop.EnableFire();
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel == null)
            {
                pausePanel = GameObject.Find("PausePanel");
            }

            if (Time.timeScale == 1f)
            {
                PauseGame();
                pausePanel.SetActive(true);
            }
            else
            {
                ResumeGame();
                pausePanel.SetActive(false);
            }
        }
    }
}
    
