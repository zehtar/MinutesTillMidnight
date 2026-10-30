using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [Header("Настройки спавна")]
    public int minAliSpawn;
    public int maxAliSpawn;
    public int enemyToSpawn   = 1;
    public int enemySpawnLevel;

    public bool canSpawn            = true;
    public bool enemyWillBeSummoned = false;
    public bool rangedEnemyCanSummon;
    public bool colision            = true;
    
    [Range(0, 100)]
    public int rangedSpawnChance = 15;
    
    public List<Enemy> enemySpawnList;   // префабы
    public Enemy rangedEnemyPrefab;
    public List<Enemy> enemyAtGameList;  // живые враги, если нужно

    [Header("Движение по кругу вокруг игрока")]
    public float      rotateSpeed;
    public float      radius = 2f;
    public Rigidbody2D rb;

    private Spawner   _spawner;
    private Transform _player;
    private Transform _damageCanvas;

    private float  angle;
    private Vector3 randomPosSpawn;

    public void Init(Spawner spawner)
    {
        _spawner      = spawner;
        _player       = spawner.player.transform;
        _damageCanvas = spawner.damageCanvas;

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_player != null)
        {
            angle += rotateSpeed * Mathf.Deg2Rad;
            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            Vector2 newPos = (Vector2)_player.position + offset;
            rb.MovePosition(newPos);
        }

        if (enemyWillBeSummoned)
            enemySummon();
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("wall"))
            colision = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("wall"))
            colision = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("wall"))
            colision = true;
    }

  

    public void enemySummon()
    {
        if (!colision || !canSpawn) return;

        enemyWillBeSummoned = false;

        for (int i = 0; i < enemyToSpawn; i++)
        {
            randomPosSpawn.x = Random.Range(-5f, 5f);
            randomPosSpawn.y = Random.Range(-5f, 5f);

            Enemy prefab;

            bool spawnRanged =
                rangedEnemyPrefab != null &&
                rangedEnemyCanSummon &&
                Random.Range(0, 100) < rangedSpawnChance;

            if (spawnRanged)
            {
                prefab = rangedEnemyPrefab;
            }
            else
            {
                int random = Random.Range(0, 100);
                int index =  Random.Range(minAliSpawn, maxAliSpawn);;
                if (index > minAliSpawn && random > 90)
                {
                    index = minAliSpawn + 1;
                }
                else
                {
                    index = minAliSpawn;
                }
                
                prefab = enemySpawnList[index];
            }

            Enemy enemy = Instantiate(
                prefab,
                transform.position + randomPosSpawn,
                Quaternion.identity
            );

            enemy.Init(
                _spawner.player,
                _spawner.scoreManager,
                _spawner,
                _spawner.statsSaved,
                _damageCanvas,
                enemySpawnLevel
            );

            enemyAtGameList.Add(enemy);
        }
    }

    public void miniBossSummon()
    {
        if (!canSpawn) return;

        randomPosSpawn.x = Random.Range(-5f, 5f);
        randomPosSpawn.y = Random.Range(-5f, 5f);

        Enemy prefab = enemySpawnList[3];
        Enemy enemy  = Instantiate(prefab, transform.position + randomPosSpawn, Quaternion.identity);

        enemy.Init(
            _spawner.player,
            _spawner.scoreManager,
            _spawner,
            _spawner.statsSaved,
            _damageCanvas,
            enemySpawnLevel
        );

        enemyAtGameList.Add(enemy);
    }

    public void FinalBossSummon()
    {
        if (!canSpawn) return;

        randomPosSpawn.x = Random.Range(-5f, 5f);
        randomPosSpawn.y = Random.Range(-5f, 5f);

        Enemy prefab = enemySpawnList[0];
        Enemy enemy  = Instantiate(prefab, transform.position + randomPosSpawn, Quaternion.identity);

        int bossLevel = enemySpawnLevel * 5;
        enemy.transform.localScale = new Vector3(2, 2, 2);

        enemy.Init(
            _spawner.player,
            _spawner.scoreManager,
            _spawner,
            _spawner.statsSaved,
            _damageCanvas,
            bossLevel
        );

        enemyAtGameList.Add(enemy);
    }
}