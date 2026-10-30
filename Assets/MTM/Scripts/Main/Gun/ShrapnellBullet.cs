using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShrapnellBullet : MonoBehaviour
{
    public float speed;
    public float timer, lifetime = 0.5f;
    public float damage;
    public int penetration;
    public int penetrationNow;
    public float baseDamage;

    public LayerMask whatIsSolid;
    public GameObject deathBulletEffect;
    public StatsSaved statsSaved;
    
    private bool _started = false;
    public HashSet<Enemy> _penetratedEnemies = new HashSet<Enemy>();

    public EffectsStatsController effStController;
    public TrailRenderer trailRenderer;

    public void Start()
    {
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        statsSaved = FindObjectOfType<StatsSaved>();
        effStController = FindObjectOfType<EffectsStatsController>();
        baseDamage = statsSaved.bulletdamage/2;
        penetration = effStController.shrapnelPenetration + 1;
    }

    void OnEnable()
    {
        timer = 0;
        if (!_started)
        {
            Start();
            _started = true;
        }

        BuffsCheck();
        _penetratedEnemies.Clear();
        penetrationNow = penetration;
        trailRenderer.Clear();
    }

    private void BuffsCheck()
    {
        damage = baseDamage * effStController.shrapnelBulletDamageInPecrent;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * (speed * Time.deltaTime));

        timer += Time.deltaTime;
        if (timer >= lifetime) 
        {
            //Instantiate(deathBulletEffect, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (!_penetratedEnemies.Contains(enemy))
            {
                Vector2 hitPoint = other.ClosestPoint(transform.position);
                
                enemy.TakeDamage(damage, hitPoint);
                _penetratedEnemies.Add(enemy);

                //Instantiate(deathBulletEffect, transform.position, transform.rotation);
                
                penetrationNow--;
                if (penetrationNow < 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
        else if (whatIsSolid == (whatIsSolid | (1 << other.gameObject.layer)))
        {
            //Instantiate(deathBulletEffect, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
