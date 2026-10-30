using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrapnelEffect : MonoBehaviour , IBulletEffect
{
    public string EffectID => "Sharpnel";
    public int shrapnelCount;
    public float spreadAngle;
    public float shrapnelSpeed;
    public EffectsStatsController effStControleller;
    public void Start()
    {
        effStControleller = FindAnyObjectByType<EffectsStatsController>();
    }
    public void OnHit(Enemy enemy, Vector2 hitPoint, Bullet orgBullet)
    {
        shrapnelCount = effStControleller.shrapnelCount;
        spreadAngle = effStControleller.spreadAngle;
        shrapnelSpeed = effStControleller.shrapnelSpeed;
        ExplodeIntoShrapnel(orgBullet);
        //спавн пули и шп пули и передача ей списка пробитых
    }
    public void ExplodeIntoShrapnel(Bullet orgBullet)
    {
        float angleStep = spreadAngle / (shrapnelCount - 1);
        float startAngle = -spreadAngle / 2f;

        for (int i = 0; i < shrapnelCount; i++)
        {
            float angle = startAngle + angleStep * i;
            Quaternion rotation = Quaternion.Euler(0, 0, angle + orgBullet.transform.eulerAngles.z);
            
            //GameObject shrapnel = Instantiate(shrapnelPrefab, orgBullet.transform.position, rotation);
            GameObject shrapnel = ShrapnelBullletPoulManager.SharedInstance.GetPooledObject(null); 
            if (shrapnel != null) 
            {
                shrapnel.transform.position = orgBullet.transform.position;
                shrapnel.transform.rotation = rotation;
                shrapnel.SetActive(true);
            }
            ShrapnellBullet shBullet = shrapnel.GetComponent<ShrapnellBullet>();
            shBullet._penetratedEnemies = orgBullet._penetratedEnemies;
            Rigidbody2D rb = shrapnel.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.velocity = rotation * Vector2.right * shrapnelSpeed;
        }
    }
}
