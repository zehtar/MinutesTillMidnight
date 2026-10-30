using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfPoint : MonoBehaviour
{
    [Header("Настройки спавна")]
    public int minAliSpawn; // Нижняя граница индекса врага
    public int maxAliSpawn; // Верхняя граница (включительно)
    public int enemyToSpawn = 1; // Сколько врагов за раз
    public int enemySpawnLevel;  // Уровень врагов

    public bool canSpawn = true;
    public bool enemyWillBeSummoned = false;
    public bool colision = true;

    [Header("Списки")]
    public List<Enemy> enemySpawnList;   // Сюда перетащи всех 21 врагов (0 - Финал, 1-3 Мини, 4-20 Обычные)
    public List<Enemy> enemyAtGameList;  // Список живых врагов

    [Header("Движение")]
    public float rotateSpeed;
    public float radius = 2f;
    public Rigidbody2D rb;

    private InfSpawner _spawner; // Ссылка на наш новый спавнер
    private Transform _player;
    private Transform _damageCanvas;

    private float angle;

    public void Init(InfSpawner spawner)
    {
        _spawner = spawner;
        _player = spawner.player.transform;
        _damageCanvas = spawner.damageCanvas;

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_player != null)
        {
            // Вращение точки спавна вокруг игрока
            angle += rotateSpeed * Mathf.Deg2Rad;
            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            Vector2 newPos = (Vector2)_player.position + offset;
            rb.MovePosition(newPos);
        }

        // Если спавнер дал команду спавнить
        if (enemyWillBeSummoned)
            enemySummon();
    }

    #region Коллизия (проверка стен)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("wall")) colision = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("wall")) colision = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("wall")) colision = true;
    }
    #endregion

    // === ОБЫЧНЫЙ СПАВН ===
    public void enemySummon()
    {
        if (!colision || !canSpawn) return;

        enemyWillBeSummoned = false; // Сбрасываем флаг

        // Защита от дурака: проверяем, чтобы индексы не вышли за пределы списка
        if (enemySpawnList == null || enemySpawnList.Count == 0) return;
        
        // Корректируем границы, чтобы не вылететь с ошибкой
        int safeMin = Mathf.Clamp(minAliSpawn, 0, enemySpawnList.Count - 1);
        int safeMax = Mathf.Clamp(maxAliSpawn, safeMin, enemySpawnList.Count - 1);

        for (int i = 0; i < enemyToSpawn; i++)
        {
            // Случайный сдвиг позиции
            Vector3 spawnOffset = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);

            // Выбираем случайного врага (max + 1, чтобы включить последнее число)
            int index = Random.Range(safeMin, safeMax + 1); 
            
            // Проверка на всякий случай
            if (index >= enemySpawnList.Count) index = enemySpawnList.Count - 1;

            Enemy prefab = enemySpawnList[index];

            Enemy enemy = Instantiate(
                prefab,
                transform.position + spawnOffset,
                Quaternion.identity
            );

            // Инициализация врага
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

    // === СПАВН МИНИ-БОССА ===
    public void miniBossSummon()
    {
        if (!canSpawn) return;

        Vector3 spawnOffset = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);

        // ВАЖНО: Мы берем индекс из minAliSpawn!
        // В InfSpawner мы написали: spawnPoint.minAliSpawn = bossIndexToSpawn;
        // Это позволяет спавнить разных боссов (1, 2 или 3)
        int bossIndex = minAliSpawn;

        // Защита: если индекс кривой, берем стандартного минибосса (индекс 3)
        if (bossIndex < 1 || bossIndex > 3) bossIndex = 3;
        // Защита от выхода за пределы списка
        if (bossIndex >= enemySpawnList.Count) bossIndex = enemySpawnList.Count - 1;

        Enemy prefab = enemySpawnList[bossIndex];
        
        Enemy enemy = Instantiate(prefab, transform.position + spawnOffset, Quaternion.identity);

        enemy.Init(
            _spawner.player,
            _spawner.scoreManager,
            _spawner,
            _spawner.statsSaved,
            _damageCanvas,
            enemySpawnLevel // Уровень уже усилен в Spawner
        );

        enemyAtGameList.Add(enemy);
    }

    // === СПАВН ФИНАЛЬНОГО БОССА ===
    public void FinalBossSummon()
    {
        if (!canSpawn) return;

        Vector3 spawnOffset = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);

        // Финальный босс всегда под индексом 0
        if (enemySpawnList.Count == 0) return;
        Enemy prefab = enemySpawnList[0];

        Enemy enemy = Instantiate(prefab, transform.position + spawnOffset, Quaternion.identity);

        // Финальный босс всегда больше и жирнее
        enemy.transform.localScale = new Vector3(2f, 2f, 2f);

        // Уровень передаем как есть (он уже усилен в Spawner строкой enemySpawnLevel += cycle * 25)
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