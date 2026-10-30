using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolierPower : MonoBehaviour
{
    public float duration = 2f;

    private HashSet<Enemy> enemiesInZone = new HashSet<Enemy>();
    private Dictionary<Enemy, float> originalSpeeds = new Dictionary<Enemy, float>();

    public player player;

    private void Start()
    {
        player = FindAnyObjectByType<player>();
        transform.SetParent(player.transform);

        // Автоматически уничтожаем объект через заданное время
        Destroy(gameObject, duration);
    }

    private void FixedUpdate()
    {
        // Следуем за игроком
        transform.position = player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && !originalSpeeds.ContainsKey(enemy))
            {
                // Сохраняем оригинальную скорость и останавливаем врага
                originalSpeeds[enemy] = enemy.speed;
                enemy.speed = 0f;

                enemiesInZone.Add(enemy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && originalSpeeds.ContainsKey(enemy))
            {
                // Восстанавливаем скорость и удаляем врага из списков
                enemy.speed = originalSpeeds[enemy];
                originalSpeeds.Remove(enemy);
                enemiesInZone.Remove(enemy);
            }
        }
    }

    private void OnDestroy()
    {
        // Если зона исчезает, восстанавливаем скорость всем врагам внутри
        foreach (var enemy in enemiesInZone)
        {
            if (enemy != null && originalSpeeds.ContainsKey(enemy))
            {
                enemy.speed = originalSpeeds[enemy];
            }
        }

        enemiesInZone.Clear();
        originalSpeeds.Clear();
    }
}
