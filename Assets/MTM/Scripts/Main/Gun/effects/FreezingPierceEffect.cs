using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingPierceEffect : MonoBehaviour, IBulletEffect
{
    public string EffectID => "IcePenetration";
    
    [Range(0f, 1f)] public float slowAmount = 0.5f; 
    public float slowDuration = 1f;
    public bool reduceMaxHP = false;
    public float maxHpReductionPercent = 0.15f;
    public bool damageBoostToSlowed = false;
    public float extraDamagePercent = 0.5f;
    public EffectsStatsController effStControleller;

    public void Start()
    {
        effStControleller = FindAnyObjectByType<EffectsStatsController>();
    }

    public void OnHit(Enemy enemy, Vector2 hitPoint,Bullet orgBullet)
    {
        slowDuration = effStControleller.slowDuration;
        slowAmount = effStControleller.slowAmount;
        reduceMaxHP = effStControleller.isSlowHpEffect;
        damageBoostToSlowed = effStControleller.slowDamageUpEffect;
      
        enemy.StartCoroutine(enemy.ApplySlow(slowAmount, slowDuration)); 
        
        if (reduceMaxHP)
        {
            enemy.ReduceMaxHealth(maxHpReductionPercent);
        }

        if (damageBoostToSlowed)
        {
            enemy.extraSlowDamagePercent = extraDamagePercent;
        }
    }
}
