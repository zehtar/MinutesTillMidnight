using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnEffect : MonoBehaviour, IBulletEffect
{
    public string EffectID => "Burn";

    public float burnDuration = 3f;
    public float burnDamagePerSecond = 1f;
    public bool burnDieAura = true;
    public bool fireBoll; 
    
    public EffectsStatsController effStControleller;
    public void Start()
    {
        effStControleller = FindAnyObjectByType<EffectsStatsController>();
    }
    public void OnHit(Enemy enemy, Vector2 hitPoint,Bullet orgBullet)
    {
        burnDuration = effStControleller.burnDiraction;
        burnDamagePerSecond = effStControleller.burnDamage;
        burnDieAura = effStControleller.burnDieAura;
        enemy.ApplyBurn(burnDuration, burnDamagePerSecond, burnDieAura);
    }
}
