using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsStatsController : MonoBehaviour
{
    //ice
    public float slowAmount = 0.3f;
    public float slowDuration = 1f;
    public bool isSlowHpEffect = false;
    public bool slowDamageUpEffect;
    //burn
    public float burnDiraction = 2f;
    public float burnDamage = 5f;
    public bool burnDieAura = false;
    public bool fireboll = false;
    public bool fireAuraPlayer = false;
    public GameObject fireBollPrefab;
    public GameObject fireAuraPlayerPrefab;
    //shrapnel
    public int shrapnelCount;
    public float spreadAngle;
    public float shrapnelSpeed;
    public float shrapnelBulletDamageInPecrent;
    public int shrapnelPenetration;
    //lighting
    public float lightingDamage;
    public float lightingRadius;
    public bool allowSecondLightning;
    public float lightningChance;
    public float secondLightningChance;
    //venom
    public float venomDamage;
    public float venomSizeMultiple;
    public bool venomEnemySlownes;
}
