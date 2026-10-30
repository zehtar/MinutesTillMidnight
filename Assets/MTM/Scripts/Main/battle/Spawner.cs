using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Точка спавна врагов")]
    public Point spawnPoint;
    public PointHeal spawnHealPoint;

    [Header("Параметры спавна")]
    public float startTimeBtwSpawn = 0.5f;
    private float _timeBtwSpawn;

    public int   enemySpawnLevel;
    public int   enemyAllKilled;
    private float _enemyOnGame;

    public float timer;

    [Header("Глобальные ссылки (авто‑поиск)")]
    public player       player;
    public GameTimer    gameTimer;
    public StatsSaved   statsSaved;
    public ScoreManager scoreManager;
    public Transform    damageCanvas;

    [Header("Флаги боссов")]
    public bool miniBossSpawned  = false;
    public bool finalBossSpawned = false;

    private void Awake()
    {
        // Ищем всё ОДИН РАЗ. В инспекторе можно не заполнять.
        if (player == null)
            player = FindAnyObjectByType<player>();

        if (statsSaved == null)
            statsSaved = FindAnyObjectByType<StatsSaved>();

        if (scoreManager == null)
            scoreManager = FindAnyObjectByType<ScoreManager>();

        if (gameTimer == null)
            gameTimer = FindAnyObjectByType<GameTimer>();

        if (spawnPoint == null)
            spawnPoint = FindAnyObjectByType<Point>();

        if (spawnHealPoint == null)
            spawnHealPoint = FindAnyObjectByType<PointHeal>();

        if (damageCanvas == null)
        {
            var dc = GameObject.FindWithTag("DamageCanvas");
            if (dc != null)
                damageCanvas = dc.transform;
        }

        // Передаём себя в Point / PointHeal
        if (spawnPoint != null)
            spawnPoint.Init(this);

        // Крутим Spawner вокруг игрока
        if (player != null)
            transform.SetParent(player.transform);

        _timeBtwSpawn = startTimeBtwSpawn;
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        // ==== ЛОГИКА ВОЛН (оставляю как у тебя, только без Find ====
        if (timer < 60f)
        {
            spawnPoint.minAliSpawn      = 1;
            spawnPoint.maxAliSpawn      = 1;
            startTimeBtwSpawn           = Mathf.Lerp(1f, 0.7f, timer / 100f);
        }
        else if (timer < 120f)
        {
            spawnPoint.enemySpawnLevel  = statsSaved.enemyLevel + 1;
            spawnPoint.minAliSpawn      = 1;
            spawnPoint.maxAliSpawn      = 3;
            startTimeBtwSpawn           = Mathf.Lerp(0.5f, 0.15f, (timer - 100f) / 100f);
            
            if (timer >= 100f && timer < 105f && !miniBossSpawned)
            {
                enemyOnLevel();
                spawnPoint.miniBossSummon();
                _timeBtwSpawn   = startTimeBtwSpawn;
                miniBossSpawned = true;
            }
        }
        else if (timer < 180f)
        {
            spawnPoint.enemySpawnLevel  = statsSaved.enemyLevel + 1;
            spawnPoint.minAliSpawn      = 1;
            spawnPoint.maxAliSpawn      = 3;
            spawnPoint.enemyToSpawn     = 1;
            startTimeBtwSpawn           = 0.3f;
            spawnPoint.rangedEnemyCanSummon = true;
        }
        else if (timer < 240f)
        {
            spawnPoint.enemySpawnLevel  = statsSaved.enemyLevel + 2;
            spawnPoint.minAliSpawn      = 4;
            spawnPoint.maxAliSpawn      = 6;
            spawnPoint.enemyToSpawn     = 2;
            startTimeBtwSpawn           = Mathf.Lerp(0.4f, 0.3f, (timer - 300f) / 100f);
        }
        else if (timer < 300f)
        {
            spawnPoint.enemySpawnLevel  = statsSaved.enemyLevel + 3;
            spawnPoint.minAliSpawn      = 6;
            spawnPoint.maxAliSpawn      = 8;
            startTimeBtwSpawn           = Mathf.Lerp(0.1f, 0.03f, (timer - 400f) / 120f);
        }
        else if (timer < 360f)
        {
            spawnPoint.enemyToSpawn     = 2;
            spawnPoint.enemySpawnLevel  = statsSaved.enemyLevel + 3;
            spawnPoint.minAliSpawn      = 6;
            spawnPoint.maxAliSpawn      = 8;
            startTimeBtwSpawn           = Mathf.Lerp(0.18f, 0.06f, (timer - 520f) / 80f);

            if (timer >= 330f && !finalBossSpawned)
            {
                enemyOnLevel();
                spawnPoint.FinalBossSummon();
                _timeBtwSpawn    = startTimeBtwSpawn;
                finalBossSpawned = true;
            }
        }

        // таймер спавна
        if (_timeBtwSpawn <= 0f)
        {
            pointIdent();
        }
        else
        {
            _timeBtwSpawn -= Time.deltaTime;
        }
    }

    // Счётчик живых врагов (если нужен)
    public void enemyOnLevel()
    {
        _enemyOnGame++;
    }

    public void enemyOnLevelDied()
    {
        _enemyOnGame--;
        enemyAllKilled++;
        spawnHealPoint?.HealSummonCheck();
    }

    public void pointIdent()
    {
        enemyOnLevel();
        spawnPoint.enemyWillBeSummoned = true;
        _timeBtwSpawn = startTimeBtwSpawn;
    }
}