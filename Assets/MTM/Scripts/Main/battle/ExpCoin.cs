using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCoin : MonoBehaviour
{
    public int expNum = 1;                  // Стоимость монеты
    public float flightDuration = 1f;      // Длительность полета по дуге
    public float maxHeight = 2f;            // Максимальная высота подъема
           
    public Transform player;               // Ссылка на игрока
    private Vector2 startPoint;            // Начальная позиция монеты
    private float flightTime = 0f;         // Время полета
    private bool shouldFlyToPlayer = false; // Флаг для начала полета

    void Start()
    {
        // Находим игрока по тегу (убедитесь, что у игрока стоит тег "Player")
        player = GameObject.FindGameObjectWithTag("player").transform;
    }

    void Update()
    {
        if (shouldFlyToPlayer)
        {
            flightTime += Time.deltaTime;
            float t = flightTime / flightDuration; // Нормализованное время (от 0 до 1)

            // Обновляем текущую позицию игрока в процессе полета
            Vector2 endPoint = player.position;

            // Вычисляем позицию по оси X (линейное движение к игроку)
            float newX = Mathf.Lerp(startPoint.x, endPoint.x, t);

            // Вычисляем позицию по оси Y (параболическая траектория)
            float newY = Mathf.Lerp(startPoint.y, endPoint.y, t) + (maxHeight * Mathf.Sin(Mathf.PI * t));

            // Устанавливаем новую позицию монеты
            transform.position = new Vector2(newX, newY);

            // Завершаем полет, когда время полета превысит длительность
            if (flightTime >= flightDuration)
            {
                shouldFlyToPlayer = false;
                transform.position = endPoint; // Точно установить монету в конце полета
            }
        }
    }

    // Этот метод вызывается, когда монета сталкивается с триггером (вторым кольцом)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CoinCollector"))
        {
            // Устанавливаем стартовую точку и запускаем полет
            startPoint = transform.position;

            // Сбрасываем время полета и активируем полет
            flightTime = 0f;
            shouldFlyToPlayer = true;
        }
    } 
}

