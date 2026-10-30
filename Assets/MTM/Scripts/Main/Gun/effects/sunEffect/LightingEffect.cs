using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEffect : MonoBehaviour
{
    public float duration = 0.2f; // Продолжительность молнии
    // public float damage = 10f; // Урон молнии
    public float effectRadius = 1f; // Радиус воздействия молнии (взрыв)

    private void Start()
    {
        // Удалим молнию через 'duration' секунд
        StartCoroutine(DestroyAfterDuration());
    }

    // Метод для нанесения урона и паралича врагам в области молнии
    public void ApplyEffects(Enemy target, float damage, float effectRadius)
    {
        // Находим всех врагов в радиусе молнии
      
        Vector2 offset = new Vector2(0f, -4.5f);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll((Vector2)transform.position + offset, effectRadius);
        
        List<Enemy> nearbyEnemies = new List<Enemy>();
        
        foreach (Collider2D col in hitEnemies)
        {
            if (col.CompareTag("Enemy"))
            {
                // Проверяем оба коллайдера на враге (если их два)
                Enemy enemy = col.GetComponentInParent<Enemy>();  // Получаем ссылку на врага через родительский объект, если коллайдеры на дочерних объектах
                if (enemy != null && !nearbyEnemies.Contains(enemy))
                {
                    nearbyEnemies.Add(enemy);
                }
            }
        }
        
        foreach (var enemys in nearbyEnemies)
        {
            enemys.TakeDamage(damage);
        }
    }
    
    IEnumerator DestroyAfterDuration()
    {
        // Удаляем молнию через определённое время
        yield return new WaitForSeconds(duration);
        Destroy(gameObject); // Удаляем молнию после времени
    }
}