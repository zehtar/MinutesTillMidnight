using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBollScript : MonoBehaviour
{
    public float speed;
    public float timer, lifetime = 2.5f;
    public float damage;
    public int penetration;
    public int penetrationNow;
    public LayerMask whatIsSolid;
    public GameObject deathBulletEffect;
    public float burnEffectTimer;
    
    private bool _started = false;
    public HashSet<Enemy> _penetratedEnemies = new HashSet<Enemy>();
    
    public TrailRenderer trail;

    public GameObject burnAuraEffectPrefab;
    void OnEnable()
    {
        timer = 0;
        trail = GetComponentInChildren<TrailRenderer>();
        trail.Clear();
        _penetratedEnemies.Clear();
        penetrationNow = penetration;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * (speed * Time.deltaTime));

        timer += Time.deltaTime;
        burnEffectTimer += Time.deltaTime;
        if (timer >= lifetime) 
        {
            Instantiate(deathBulletEffect, transform.position, transform.rotation);
            GameObject fireAura = Instantiate(burnAuraEffectPrefab, transform.position, transform.rotation);
            fireAura.transform.localScale = new Vector3(2, 2, 1);
            fireAura.GetComponent<FireArea>().damagePerTick = 10;
            Destroy(gameObject);
        }

        if (burnEffectTimer >= 0.03f)
        {
            Instantiate(deathBulletEffect, transform.position, transform.rotation);
            burnEffectTimer = 0;
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

                Instantiate(deathBulletEffect, transform.position, transform.rotation);
                
                penetrationNow--;
                if (penetrationNow < 0)
                {
                    GameObject fireAura = Instantiate(burnAuraEffectPrefab, transform.position, transform.rotation);
                    fireAura.transform.localScale = new Vector3(3, 3, 3);
                    fireAura.GetComponent<FireArea>().damagePerTick = 10;
                    Destroy(gameObject);
                }
            }
        }
        else if (whatIsSolid == (whatIsSolid | (1 << other.gameObject.layer)))
        {
            Instantiate(deathBulletEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            GameObject fireAura = Instantiate(burnAuraEffectPrefab, transform.position, transform.rotation);
            fireAura.transform.localScale = new Vector3(2, 2, 1);
            fireAura.GetComponent<FireArea>().damagePerTick = 10;
        }
    }
}
