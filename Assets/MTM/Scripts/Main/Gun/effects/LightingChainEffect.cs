using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingChainEffect : MonoBehaviour , IBulletEffect
{
    public string EffectID => "LightingChain";
    public GameObject LightingPrefab;
    public float lightningDelay;
    
    public float lightingDamage;
    public float lightingRadius;
    public bool allowSecondLightning;
    
    public float lightningChance;
    public float secondLightningChance;
    
    public EffectsStatsController effStControleller;
    public void Start()
    {
        effStControleller = FindAnyObjectByType<EffectsStatsController>();
    }
    public void OnHit(Enemy enem, Vector2 hitPoint, Bullet orgBullet)
    {
        lightingDamage = effStControleller.lightingDamage;
        lightingRadius = effStControleller.lightingRadius;
        lightningChance = effStControleller.lightningChance;
        allowSecondLightning = effStControleller.allowSecondLightning;
        secondLightningChance = effStControleller.secondLightningChance;
        // шанс спавна первой молнии
        if (Random.value > lightningChance) return;

        // находим всех врагов в радиусе
        List<Enemy> nearby = FindNearbyEnemies(enem.transform.position);
        if (nearby.Count == 0) return;

        // первая молния
        Enemy first = nearby[Random.Range(0, nearby.Count)];
        Vector2 pos1 = new Vector2(first.transform.position.x, first.transform.position.y + 4.5f);
        LightningEffect fx1 = Instantiate(LightingPrefab, pos1, Quaternion.identity)
            .GetComponent<LightningEffect>();
        fx1.ApplyEffects(first, lightingDamage, lightingRadius);

        // вторая молния, если есть апгрейд и прошёл шанс
        nearby.Remove(first);
        if (allowSecondLightning && nearby.Count > 0 && Random.value <= secondLightningChance)
        {
            Enemy second = nearby[Random.Range(0, nearby.Count)];
            Vector2 pos2 = new Vector2(second.transform.position.x, second.transform.position.y + 4.5f);
            LightningEffect fx2 = Instantiate(LightingPrefab, pos2, Quaternion.identity)
                .GetComponent<LightningEffect>();
            fx2.ApplyEffects(second, lightingDamage, lightingRadius);
        }
    }

    private List<Enemy> FindNearbyEnemies(Vector2 hitPoint)
    {
        List<Enemy> nearbyEnemies = new List<Enemy>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(hitPoint, lightingRadius);  // Используем 2D метод

        foreach (Collider2D col in colliders)
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

        return nearbyEnemies;
    }

}
