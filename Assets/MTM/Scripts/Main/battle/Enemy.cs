using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("HP")]
    public float baseHealt;
    public bool isBoss = false;
    public bool isDie = false;

    public float maxHP;
    public float health;

    [Header("Движение")]
    public float baseSpeed;
    public float speed;
    public float maxSpeed;
    public bool canMove = true;

    [Header("Атака")]
    public int damage;
    public float startTimeBtwAttack = 1f;
    public float attackRadius = 0.9f;

    [Header("Награда / визуал смерти")]
    public int expGain = 1;
    public GameObject deathEffect;
    public GameObject deathCoinDrop;
    public GameObject fireArea;
    public bool isEvil = true;

    [Header("Урон по врагу")]
    public GameObject floatingDamage;
    public Transform damageCanvas;
    public float extraSlowDamagePercent;

    [Header("Уровень")]
    public int enemyLevel;

    // --- ЗАВИСИМОСТИ ---
    private Vector3 _initialScale;
    public player _player;
    [HideInInspector] public ScoreManager sm;
    [HideInInspector] public StatsSaved st;

    // --- СПАВНЕРЫ ---
    // Храним ссылку либо на обычный, либо на бесконечный спавнер
    [HideInInspector] public Spawner sp;         // Обычный
    [HideInInspector] public InfSpawner infSp;   // Бесконечный (НОВОЕ ПОЛЕ)

    // внутренние поля
    private SpriteRenderer _spriteRenderer;
    private Transform _tr;
    private Vector2 _baseScale;

    private float _timebBtwAttack;

    private Coroutine burnCoroutine;
    private bool burnDeathCircleSpawns;

    private EnemySeparation _separation;

    private Vector2 _currentMoveDir;

    [SerializeField] private float turnSpeed = 10f;   // сглаживание
    [SerializeField] private float sepWeight = 1.0f;  // насколько сильно учитывать раздвигание

    private HitFlash hitEffect;

    private void Awake()
    {
        _tr = transform;
        _separation = GetComponent<EnemySeparation>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        hitEffect = GetComponent<HitFlash>();
        _initialScale = transform.localScale;
    }

    /// <summary>
    /// Инициализация для ОБЫЧНОГО режима
    /// </summary>
    public void Init(
        player playerRef,
        ScoreManager scoreManager,
        Spawner spawner,
        StatsSaved stats,
        Transform dmgCanvas,
        int level
    )
    {
        _player = playerRef;
        sm = scoreManager;
        sp = spawner;     // Сохраняем обычный спавнер
        infSp = null;     // Обнуляем бесконечный
        st = stats;
        damageCanvas = dmgCanvas;
        enemyLevel = level;

        SetupStats();
    }

    /// <summary>
    /// Инициализация для БЕСКОНЕЧНОГО режима (InfSpawner)
    /// </summary>
    public void Init(
        player playerRef,
        ScoreManager scoreManager,
        InfSpawner spawner, // Принимаем InfSpawner
        StatsSaved stats,
        Transform dmgCanvas,
        int level
    )
    {
        _player = playerRef;
        sm = scoreManager;
        sp = null;        // Обнуляем обычный
        infSp = spawner;  // Сохраняем бесконечный
        st = stats;
        damageCanvas = dmgCanvas;
        enemyLevel = level;

        SetupStats();
    }

    // Вынес настройку статов в отдельный метод, чтобы не дублировать код
    private void SetupStats()
    {
        if (isEvil)
        {
            float hp = baseHealt;
            if (isBoss && st != null) // Проверка на null
                hp *= st.eneeyBossHpMyltiply;

            maxHP = hp * (1f + (enemyLevel - 1) * 0.3f);
            
            float bonusSpeed = (st != null) ? st.enemySpeed : 0;
            speed = baseSpeed + bonusSpeed;
            maxSpeed = speed;
            
            damage = (st != null) ? st.enemyDamage : 1;
        }
        else
        {
            maxHP = baseHealt;
            speed = baseSpeed;
            maxSpeed = speed;
        }

        health = maxHP;
        _timebBtwAttack = 0f;
        burnDeathCircleSpawns = false;
    }

    private void Update()
    {
        if (_player == null) return;

        var s = _initialScale;

        // Поворот спрайта
        if (_player.transform.position.x < transform.position.x)
            s.x = -Mathf.Abs(_initialScale.x);  // смотрим влево
        else
            s.x = Mathf.Abs(_initialScale.x);   // смотрим вправо

        transform.localScale = s;

        // Атака
        float sqrDist = (_player.transform.position - _tr.position).sqrMagnitude;
        float attackRadiusSq = attackRadius * attackRadius;

        if (sqrDist <= attackRadiusSq && _timebBtwAttack <= 0f)
        {
            OnEnemyAttack();
            _timebBtwAttack = startTimeBtwAttack;
        }

        if (_timebBtwAttack > 0f)
            _timebBtwAttack -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (_player == null || speed <= 0f || !canMove) return;

        Vector2 pos = _tr.position;
        Vector2 toPlayer = (Vector2)_player.transform.position - pos;
        if (toPlayer.sqrMagnitude < 0.0001f) return;

        Vector2 dirToPlayer = toPlayer.normalized;
        Vector2 push = _separation != null ? _separation.PushDir * sepWeight : Vector2.zero;

        // Комбинируем движение к игроку и раздвигание
        Vector2 desiredDir = dirToPlayer + push;

        if (desiredDir.sqrMagnitude < 0.0001f)
            desiredDir = dirToPlayer;
        else
            desiredDir.Normalize();

        // Плавно меняем направление, чтобы не дёргались
        _currentMoveDir = Vector2.MoveTowards(
            _currentMoveDir,
            desiredDir,
            turnSpeed * Time.fixedDeltaTime
        );
        if (_currentMoveDir.sqrMagnitude < 0.0001f)
            _currentMoveDir = desiredDir;

        _tr.position = pos + _currentMoveDir * speed * Time.fixedDeltaTime;
    }

    public void TakeDamage(float damage)
    {
        TakeDamage(damage, _tr.position, false);
    }

    public void TakeDamage(float damage, Vector2 hitPoint)
    {
        TakeDamage(damage, hitPoint, false);
    }

    public void TakeDamage(float damage, Vector2 hitPoint, bool isCrit)
    {
        float finalDamage = damage + (damage * extraSlowDamagePercent);
        health -= finalDamage;

        // текст урона (позже тоже можно запулить)
        if (damageCanvas != null && floatingDamage != null)
        {
            // Здесь лучше использовать Instantiate как есть, либо пул
            // Для упрощения оставляю Instantiate, но привожу к мировым координатам аккуратно
            GameObject obj = Instantiate(floatingDamage, damageCanvas);
            obj.transform.position = hitPoint + new Vector2(0, 0.5f); 
            // Или если у тебя UI DamageCanvas в ScreenSpace, то логика другая, но оставим как было у тебя
            // obj.GetComponent<RectTransform>().localPosition = ... (твоя старая логика)
            
            obj.GetComponent<DamageTextAnimationScript>().Init(finalDamage, isCrit);
        }
        
        hitEffect.Flash();

        if (health <= 0f && !isDie)
            Die();
    }

    private void Die()
    {
        isDie = true;
        if (!isEvil)
        {
            if (deathCoinDrop != null)
                Instantiate(deathCoinDrop, _tr.position, _tr.rotation);

            if (deathEffect != null)
                Instantiate(deathEffect, _tr.position, _tr.rotation);

            Destroy(gameObject);
            return;
        }

        sm?.kill();

        if (deathCoinDrop != null)
        {
            var coinObj = Instantiate(deathCoinDrop, _tr.position, _tr.rotation);
            var coin = coinObj.GetComponent<ExpCoin>();
            if (coin != null) coin.expNum = expGain;
        }

        if (deathEffect != null)
            Instantiate(deathEffect, _tr.position, _tr.rotation);

        if (burnDeathCircleSpawns && fireArea != null)
            Instantiate(fireArea, _tr.position, _tr.rotation);

        // --- ГЛАВНОЕ ИЗМЕНЕНИЕ: ПРОВЕРКА ОБОИХ СПАВНЕРОВ ---
        if (sp != null)
        {
            sp.enemyOnLevelDied(); // Сообщаем обычному спавнеру
        }
        else if (infSp != null)
        {
            infSp.enemyOnLevelDied(); // Сообщаем бесконечному спавнеру
        }

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayBoom();

        Destroy(gameObject);
    }

    public void OnEnemyAttack()
    {
        _player?.TakeDamage(damage);
    }

    public IEnumerator ApplySlow(float amount, float duration)
    {
        float factor = 1f - amount;
        speed *= factor;
        if (_spriteRenderer != null)
            _spriteRenderer.color = new Color(0.5f, 0.8f, 1f);

        yield return new WaitForSeconds(duration);

        if (_spriteRenderer != null)
            _spriteRenderer.color = Color.white;
        speed /= factor;
    }

    public void ReduceMaxHealth(float percent)
    {
        float reduction = maxHP * percent;
        maxHP -= reduction;
        if (health > maxHP)
            health = maxHP;
    }

    public void ApplyBurn(float burnDuration, float burnDamagePerSecond, bool burnDieAura)
    {
        burnDeathCircleSpawns = burnDieAura;
        if (burnCoroutine != null)
            StopCoroutine(burnCoroutine);

        burnCoroutine = StartCoroutine(BurnCoroutine(burnDuration, burnDamagePerSecond));
    }

    private IEnumerator BurnCoroutine(float burnDuration, float burnDamagePerSecond)
    {
        float timer = 0f;

        while (timer < burnDuration)
        {
            timer += 1f;
            yield return new WaitForSeconds(1f);
            
            // Здесь важно не убить врага внутри корутины без проверок,
            // поэтому вызываем TakeDamage
            if (this != null) // Проверка, жив ли объект
            {
                TakeDamage(burnDamagePerSecond);
            }
            else
            {
                yield break;
            }
        }

        burnCoroutine = null;
    }
}