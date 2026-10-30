using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float timer, lifetime = 0.5f;
    public float damage;
    public int penetration;
    public int penetrationNow;
    public bool _isCrit;

    public float bigbaff = 0;
    public float baseSpeed, baseDamage, baseSize;

    public LayerMask whatIsSolid;
    public GameObject deathBulletEffect;
    public StatsSaved statsSaved;
    public BulletPoulManager manager;
    public WeaponPassivesActive passives;
    public FireScript fireScript;

    public int rotateDown;
    public int rotateUp;

    private bool _started = false;
    public HashSet<Enemy> _penetratedEnemies = new HashSet<Enemy>();

    public BulletEffectController effectController; // Новый контроллер эффектов
    
    public TrailRenderer trail;

    public void Start()
    {
        passives = FindObjectOfType<WeaponPassivesActive>();
        effectController = this.GetComponent<BulletEffectController>();
        
        statsSaved = FindObjectOfType<StatsSaved>();
        fireScript = FindObjectOfType<FireScript>();
        
        baseDamage = statsSaved.bulletdamage;
        baseSpeed = statsSaved.bulletspeed;
        penetration = statsSaved.penetration;
        baseSize = 0.3f;
    }

    public void FindAgain()
    {
        passives = FindObjectOfType<WeaponPassivesActive>();
        effectController = this.GetComponent<BulletEffectController>();
        
        statsSaved = FindObjectOfType<StatsSaved>();
        fireScript = FindObjectOfType<FireScript>();
        
        baseDamage = statsSaved.bulletdamage;
        baseSpeed = statsSaved.bulletspeed;
        penetration = statsSaved.penetration;
        baseSize = 0.3f;
    }

    void OnEnable()
    {
        
        timer = 0;
        if (passives == null)
        {
            FindAgain();
        }
        
        BuffsCheck();
        _penetratedEnemies.Clear();
        penetrationNow = penetration + (int)bigbaff * 2;
        trail = GetComponentInChildren<TrailRenderer>();
        trail.enabled = true;
        trail.Clear();
    }

    void OnDisable()
    {
        trail.Clear();
        trail.enabled = false;
    }

    private void BuffsCheck()
    {
        manager = BulletPoulManager.SharedInstance;
        damage = baseDamage * manager.globalDamageBuff + bigbaff * 2;
        speed = baseSpeed * manager.globalSpeedBuff + bigbaff * 3;
        penetration = statsSaved.penetration + manager.globalPenetrationBuff;

        float newScale = baseSize + manager.globalScaleBuff + bigbaff / 2;
        if (newScale <= 0f)
        {
            newScale = 0.1f;
        }
        transform.localScale = new Vector3(newScale, newScale, 1f);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * (speed * Time.deltaTime));

        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            //Instantiate(deathBulletEffect, transform.position, transform.rotation);
            trail.enabled = false;
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
                
                effectController.ApplyEffects(enemy, hitPoint , this);
                
                float finalDamage = passives.critChanceActive ? CritDamagion((int)damage) : (int)damage;
                finalDamage = CalculateDamage(finalDamage,statsSaved.critChance,statsSaved.critDamage);
                
                enemy.TakeDamage(finalDamage, hitPoint, _isCrit);
                _penetratedEnemies.Add(enemy);

                //Instantiate(deathBulletEffect, transform.position, transform.rotation);
                
                penetrationNow--;
                if (penetrationNow < 0)
                {
                    trail.enabled = false;
                    gameObject.SetActive(false);
                }
            }
        }
        else if (whatIsSolid == (whatIsSolid | (1 << other.gameObject.layer)))
        {
            //Instantiate(deathBulletEffect, transform.position, transform.rotation);
            trail.enabled = false;
            gameObject.SetActive(false);
        }
    }

    public int CritDamagion(int damage)
    {
        int randomChance = Random.Range(1, 4);
        if (randomChance >= 3)
        {
            return damage += damage / 2;
        }
        else
        {
            return damage;
        }
    }
    
    public float CalculateDamage(float baseDamage, float critChance, float critMultiplier)
    {
        bool isCrit = Random.Range(0,100) < critChance;
        if (isCrit)
        {
            float critDamage = (baseDamage * critMultiplier);
            _isCrit = true;
            return critDamage;
        }
        else
        {
            _isCrit = false;
            return baseDamage;
        }
    }
}
