using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEffect : MonoBehaviour, IBulletEffect
{
    public string EffectID => "Explode";
    public GameObject boom;
    
    public EffectsStatsController effStControleller;
    public void Start()
    {
        effStControleller = FindAnyObjectByType<EffectsStatsController>();
    }
    public void OnHit(Enemy enemy, Vector2 hitPoint, Bullet orgBullet)
    {
        Vector3 spawnPosition = new Vector3(hitPoint.x, hitPoint.y, 0f);
    
        if (boom != null)
        {
            Instantiate(boom, spawnPosition, Quaternion.identity);
        }
    
        orgBullet.penetrationNow = -999;
        Debug.Log("boom");
    }

}

