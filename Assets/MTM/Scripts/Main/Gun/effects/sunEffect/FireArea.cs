using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireArea : MonoBehaviour 
{
    public float damagePerTick = 2f;
    public float duration = 2f;
    public float tickRate = 0.5f;
    public bool enemySlownes;

    private readonly HashSet<Enemy> enemiesInZone = new HashSet<Enemy>();
    // Хранит время, когда конкретный враг сможет получить следующий периодический урон
    private readonly Dictionary<Enemy, float> nextDamageTime = new Dictionary<Enemy, float>();

    private void Start() 
    {
        StartCoroutine(DamageTick());
        Destroy(gameObject, duration);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Enemy")) 
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && enemiesInZone.Add(enemy)) 
            {
                ApplyEffects(enemy);
                
                // Наносим урон мгновенно при входе
                enemy.TakeDamage(damagePerTick);
                
                // Задаем задержку перед следующим тиком урона для этого врага
                nextDamageTime[enemy] = Time.time + tickRate;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Enemy")) 
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && enemiesInZone.Remove(enemy)) 
            {
                RemoveEffects(enemy);
                nextDamageTime.Remove(enemy);
            }
        }
    }

    private void OnDestroy()
    {
        foreach (Enemy enemy in enemiesInZone)
        {
            if (enemy != null)
            {
                RemoveEffects(enemy);
            }
        }
    }

    private void ApplyEffects(Enemy enemy)
    {
        if (enemySlownes) 
        {
            if (enemy.TryGetComponent<SpriteRenderer>(out var sr))
            {
                sr.color = new Color(0.4f, 0.2f, 1f);
            }
            enemy.speed *= 0.7f;
        }
    }

    private void RemoveEffects(Enemy enemy)
    {
        if (enemySlownes) 
        {
            if (enemy.TryGetComponent<SpriteRenderer>(out var sr))
            {
                sr.color = Color.white;
            }
            enemy.speed /= 0.7f;
        }
    }

    private IEnumerator DamageTick() 
    {
        var wait = new WaitForSeconds(tickRate);
        while (true) 
        {
            yield return wait;

            // Очищаем уничтоженных врагов из словаря и списка
            enemiesInZone.RemoveWhere(enemy => enemy == null);
            var deadEnemies = nextDamageTime.Keys.Where(enemy => enemy == null).ToList();
            foreach (var dead in deadEnemies) nextDamageTime.Remove(dead);

            foreach (Enemy enemy in enemiesInZone.ToArray()) 
            {
                // Проверяем, прошло ли достаточно времени с момента последнего урона
                if (enemy != null && Time.time >= nextDamageTime[enemy])
                {
                    enemy.TakeDamage(damagePerTick);
                    nextDamageTime[enemy] = Time.time + tickRate;
                }
            }
        }
    }
}
