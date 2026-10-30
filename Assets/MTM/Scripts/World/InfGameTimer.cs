using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfGameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText; // SerializeField позволяет видеть поле в инспекторе, но оставляет его приватным
    
    private bool isRunning = true;
    private float elapsedTime = 0f; // Храним общее точное время
    private int lastDisplayedSecond = -1; // Для проверки: изменилась ли секунда
    
    public int second;
    public float timer;

    void Update()
    {
        if (timer < 1)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            second++;
        }
        
        if (isRunning)
        {
            // 1. Просто накапливаем время (Time.deltaTime)
            // Это намного точнее, чем сбрасывать счетчик каждую секунду
            elapsedTime += Time.deltaTime;

            // 2. Получаем текущее количество полных секунд
            int currentSecond = Mathf.FloorToInt(elapsedTime);

            // 3. ОПТИМИЗАЦИЯ: Обновляем текст ТОЛЬКО если секунда изменилась
            // Нет смысла обновлять текст 60 раз в секунду, если цифры те же
            if (currentSecond != lastDisplayedSecond)
            {
                lastDisplayedSecond = currentSecond;
                UpdateTimerText(currentSecond);
            }
        }
    }

    // Вынесли обновление текста в отдельный метод для удобства
    private void UpdateTimerText(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        
        // string.Format создает "мусор" в памяти. 
        // Теперь мы вызываем его только 1 раз в секунду, а не каждый кадр.
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        isRunning = true;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        lastDisplayedSecond = -1; // Сбрасываем проверку, чтобы текст обновился принудительно
        isRunning = true;
        
        // Сразу обновляем UI на 00:00, чтобы не ждать 1 секунду до обновления
        UpdateTimerText(0); 
    }
}
