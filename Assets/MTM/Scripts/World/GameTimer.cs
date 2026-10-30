using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Ссылка на UI-текст для отображения таймера
    private float elapsedTime = 360f; // Время с начала игры
    public bool isRunning = true; // Флаг для остановки таймера
    public bool gameIsWinOver;
    
    public float timer;
    public int second;

    public GameObject winPanel;
    public Point gameEnemySpawner;
    public player player;

    void Start()
    {
        player = FindObjectOfType<player>();
    }

    void Update()
    {
        if(elapsedTime > 0)
        {
            if (isRunning)
            {
                timer += Time.deltaTime;
                if(timer >= 1)
                {
                   timer = 0;
                   second++;
                }          
        
                // Увеличиваем время
                elapsedTime -= Time.deltaTime;
                // Форматируем время в формат "мм:сс"
                int minutes = Mathf.FloorToInt(elapsedTime / 60f);
                int seconds = Mathf.FloorToInt(elapsedTime % 60f);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            } 
        }
        else
        {
            gameIsWinOver = true;
            timerText.text = "0:00";
            StopTimer();
            winPanel.SetActive(true);
            DeathPanelButtonAnim panel = winPanel.GetComponentInChildren<DeathPanelButtonAnim>();
            panel.gameEnded = true;
            gameEnemySpawner.canSpawn = false;
            player.GetComponent<GameStats>().CoutingAwards();
        }
    }  
    // Метод для остановки таймера
    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        isRunning = true;
    }

    // Метод для сброса таймера
    public void ResetTimer()
    {
        elapsedTime = 0f;
        isRunning = true;
    }
}
