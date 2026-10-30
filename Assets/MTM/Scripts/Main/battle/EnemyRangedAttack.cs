using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    public Enemy enemy;

    public GameObject bulletPrefab;
    public Transform shootPoint;

    public float shootRadius = 6f;
    public float shootCooldown = 2f;

    private player target;

    private float timer;

    private void Start()
    {
        target = enemy._player;
    }

    private void Update()
    {
        if (target == null)
        {
            target = enemy._player;
        }

        float sqrDist =
            (target.transform.position - transform.position).sqrMagnitude;

        float shootRadiusSq = shootRadius * shootRadius;

        if(sqrDist > shootRadiusSq)
        {
            enemy.canMove = true;
            return;
        }

        timer -= Time.deltaTime;

        // двигается пока почти не выстрелил
        enemy.canMove = timer > 1f;

        if(timer <= 0f)
        {
            Shoot();

            timer = shootCooldown;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            shootPoint.position,
            Quaternion.identity
        );

        Vector2 dir =
            (target.transform.position - shootPoint.position).normalized;

        bullet.GetComponent<EnemyBullet>()
            .SetDirection(dir);
    }
}