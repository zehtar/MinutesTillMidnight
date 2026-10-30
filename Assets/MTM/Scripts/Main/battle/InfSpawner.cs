using UnityEngine;

public class InfSpawner : MonoBehaviour
{
    [Header("Точка спавна врагов")]
    public InfPoint spawnPoint;
    public PointHeal spawnHealPoint;

    [Header("Параметры спавна")]
    public float startTimeBtwSpawn = 0.5f;
    private float _timeBtwSpawn;

    public int enemySpawnLevel;
    public int enemyAllKilled;
    private float _enemyOnGame;

    [Header("Таймер волны (0 - 600 сек)")]
    public float timer; 

    [Header("Глобальные параметры цикла")]
    public int cycleCount = 0; // Какой по счету круг (0, 1, 2...)
    public int currentPhase = 0; // Текущая фаза (0, 1 или 2) для выбора пула врагов

    [Header("Глобальные ссылки")]
    public player player;
    public GameTimer gameTimer;
    public StatsSaved statsSaved;
    public ScoreManager scoreManager;
    public Transform damageCanvas;

    [Header("Флаги боссов")]
    public bool miniBossSpawned = false;
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
            spawnPoint = FindAnyObjectByType<InfPoint>();

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

    private void Update()
    {
        timer += Time.deltaTime;

        // --- ЛОГИКА СБРОСА ТАЙМЕРА (ЦИКЛ) ---
        // Если прошло 10 минут (600 секунд)
        if (timer >= 600f)
        {
            timer = 0f;          // Сбрасываем таймер
            cycleCount++;        // Увеличиваем сложность
            miniBossSpawned = false;  // Разрешаем спавн минибосса в новом круге
            finalBossSpawned = false; // Разрешаем спавн финал босса в новом круге
            
            // Определяем фазу (0, 1, 2, потом снова 0, 1, 2...)
            currentPhase = cycleCount % 3; 
            
            Debug.Log($"<color=red>НАЧАЛСЯ НОВЫЙ ЦИКЛ: {cycleCount}, ФАЗА: {currentPhase}</color>");
        }

        // --- НАСТРОЙКА ВРАГОВ ДЛЯ ТЕКУЩЕЙ ФАЗЫ ---
        // Базовые границы врагов в зависимости от фазы (меняются каждые 10 мин)
        int phaseMinIndex = 4;
        int phaseMaxIndex = 9;

        if (currentPhase == 1) // 2-е 10 минут
        {
            phaseMinIndex = 10;
            phaseMaxIndex = 15;
        }
        else if (currentPhase == 2) // 3-е 10 минут
        {
            phaseMinIndex = 15; // Чуть пересекаем или ставим 16
            phaseMaxIndex = 20; // До последнего врага
        }

        // Базовое усиление уровня от количества кругов
        // Например: 0 круг = +0 ур, 1 круг = +5 ур, 2 круг = +10 ур.
        int difficultyBonus = cycleCount * 5; 

        // --- СЦЕНАРИЙ ВОЛНЫ (Повторяется каждые 10 мин) ---
        
        if (timer < 100f)
        {
            // Легкое начало
            spawnPoint.enemySpawnLevel = statsSaved.enemyLevel + difficultyBonus;
            spawnPoint.minAliSpawn = phaseMinIndex;
            spawnPoint.maxAliSpawn = phaseMinIndex + 2; // Берем первых 2-х из пака
            
            // Чем больше циклов, тем быстрее спавн даже в начале
            startTimeBtwSpawn = Mathf.Lerp(1f, 0.7f, timer / 100f) / (1 + cycleCount * 0.2f);
        }
        else if (timer < 200f)
        {
            spawnPoint.enemySpawnLevel = statsSaved.enemyLevel + 1 + difficultyBonus;
            spawnPoint.minAliSpawn = phaseMinIndex;
            spawnPoint.maxAliSpawn = phaseMaxIndex - 2;
            
            startTimeBtwSpawn = Mathf.Lerp(0.5f, 0.15f, (timer - 100f) / 100f) / (1 + cycleCount * 0.2f);
            
            // --- СПАВН МИНИБОССА (180 сек) ---
            if (timer >= 180f && timer < 185f && !miniBossSpawned)
            {
                enemyOnLevel();

                // Выбираем минибосса в зависимости от фазы:
                // Фаза 0 -> Босс 1, Фаза 1 -> Босс 2, Фаза 2 -> Босс 3
                int bossIndexToSpawn = 1 + currentPhase; 
                
                // Чтобы это сработало, в Point.cs методе miniBossSummon() 
                // нужно уметь принимать индекс или выбирать его логикой.
                // Если у тебя там жестко задано, можно временно подменить min/maxAliSpawn:
                
                // ХАК ДЛЯ СПАВНА НУЖНОГО МИНИБОССА:
                spawnPoint.minAliSpawn = bossIndexToSpawn; 
                spawnPoint.maxAliSpawn = bossIndexToSpawn;
                
                // БАФФ БОССА (Если круг > 0, босс получает +15 уровней за каждый круг)
                spawnPoint.enemySpawnLevel += (cycleCount * 15); 
                
                spawnPoint.miniBossSummon();
                
                _timeBtwSpawn = startTimeBtwSpawn;
                miniBossSpawned = true;
            }
        }
        else if (timer < 300f)
        {
            spawnPoint.enemySpawnLevel = statsSaved.enemyLevel + 1 + difficultyBonus;
            spawnPoint.minAliSpawn = phaseMinIndex + 2;
            spawnPoint.maxAliSpawn = phaseMaxIndex;
            
            // Увеличиваем кол-во врагов за раз с каждым циклом
            spawnPoint.enemyToSpawn = 2 + cycleCount; 
            
            startTimeBtwSpawn = 0.4f / (1 + cycleCount * 0.1f);
        }
        else if (timer < 400f)
        {
            spawnPoint.enemySpawnLevel = statsSaved.enemyLevel + 2 + difficultyBonus;
            spawnPoint.minAliSpawn = phaseMinIndex;
            spawnPoint.maxAliSpawn = phaseMaxIndex;
            
            startTimeBtwSpawn = Mathf.Lerp(0.4f, 0.3f, (timer - 300f) / 100f) / (1 + cycleCount * 0.1f);
        }
        else if (timer < 520f)
        {
            spawnPoint.enemySpawnLevel = statsSaved.enemyLevel + 3 + difficultyBonus;
            spawnPoint.minAliSpawn = phaseMinIndex + 1; // Убираем самых слабых
            spawnPoint.maxAliSpawn = phaseMaxIndex;
            
            startTimeBtwSpawn = Mathf.Lerp(0.1f, 0.03f, (timer - 400f) / 120f) / (1 + cycleCount * 0.1f);
        }
        else // От 520 до 600 (ФИНАЛ ЦИКЛА)
        {
            spawnPoint.enemyToSpawn = 2 + cycleCount;
            spawnPoint.enemySpawnLevel = statsSaved.enemyLevel + 3 + difficultyBonus;
            spawnPoint.minAliSpawn = phaseMinIndex + 2;
            spawnPoint.maxAliSpawn = phaseMaxIndex;
            
            startTimeBtwSpawn = Mathf.Lerp(0.18f, 0.06f, (timer - 520f) / 80f) / (1 + cycleCount * 0.1f);

            // --- СПАВН ФИНАЛЬНОГО БОССА (550 сек) ---
            if (timer >= 550f && !finalBossSpawned)
            {
                enemyOnLevel();
                
                // Финальный босс всегда индекс 0 (как ты писал)
                spawnPoint.minAliSpawn = 0; 
                spawnPoint.maxAliSpawn = 0;

                // БАФФ ФИНАЛ БОССА (очень сильный на повторных кругах)
                spawnPoint.enemySpawnLevel += (cycleCount * 25); 

                spawnPoint.FinalBossSummon();
                
                _timeBtwSpawn = startTimeBtwSpawn;
                finalBossSpawned = true;
            }
        }

        // Таймер самого спавна
        if (_timeBtwSpawn <= 0f)
        {
            pointIdent();
        }
        else
        {
            _timeBtwSpawn -= Time.deltaTime;
        }
    }

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