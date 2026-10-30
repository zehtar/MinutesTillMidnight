using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class LamiPower : MonoBehaviour
{
    public float damagePerTick = 2f;
    public float duration = 2f;
    public float tickRate = 1f;

    private HashSet<Enemy> enemiesInZone = new HashSet<Enemy>();
    
    public player player;
    public StatsSaved statsSaved;
    
    private void Start()
    {
        player = FindAnyObjectByType<player>();
        statsSaved = FindObjectOfType<StatsSaved>();
        transform.SetParent(player.transform);
        damagePerTick = statsSaved.powersDamage;
        duration = statsSaved.powersDuration;
        transform.localScale = new Vector3(statsSaved.powersSize, statsSaved.powersSize, 1);
        
        StartCoroutine(DamageTick());
        Destroy(gameObject, duration);
    }

    public void FixedUpdate()
    {
        transform.position = player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemiesInZone.Add(enemy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemiesInZone.Remove(enemy);
            }
        }
    }

    private IEnumerator DamageTick()
    {
        while (true)
        {
            // ИСПРАВЛЕНИЕ: Создаем временный список (копию) врагов, находящихся в зоне.
            // Мы будем перебирать 'targets', поэтому удаление из оригинального 
            // 'enemiesInZone' (через OnTriggerExit) не сломает этот цикл.
            List<Enemy> targets = new List<Enemy>(enemiesInZone);

            foreach (Enemy enemy in targets)
            {
                // Всегда проверяем на null, так как враг мог быть уничтожен
                // в предыдущем кадре или ранее в этом цикле
                if (enemy != null)
                {
                    enemy.TakeDamage(damagePerTick);
                }
            }
        
            // Дополнительно: Очищаем основной HashSet от "мертвых" ссылок, 
            // если OnTriggerExit вдруг не сработал для уничтоженного объекта
            enemiesInZone.RemoveWhere(e => e == null);

            yield return new WaitForSeconds(tickRate);
        }
    }
}
