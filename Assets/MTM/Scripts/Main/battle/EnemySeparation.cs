using UnityEngine;

public class EnemySeparation : MonoBehaviour
{
    [Header("Размер врага")]
    [Tooltip("Примерно радиус спрайта/коллайдера врага")]
    [SerializeField] private float bodyRadius = 0.35f;  // подгони под размер врага

    [Header("Радиус поиска соседей")]
    [SerializeField] private float separationRadius = 1.0f;   // чуть больше 2 * bodyRadius

    [Header("Сила раздвигания")]
    [SerializeField] private float pushStrength  = 5f;        // сила толчка при наложении
    [SerializeField] private float maxPush      = 1.0f;       // лимит длины вектора
    [SerializeField] private float checkInterval = 0.05f;     // как часто считать

    [SerializeField] private LayerMask enemyLayer;

    private static readonly Collider2D[] _results = new Collider2D[32];

    private Transform _tr;
    private Vector2   _pushDir;
    private float     _timer;

    public Vector2 PushDir => _pushDir;   // это подмешиваем к движению

    private void Awake()
    {
        _tr = transform;

        // если radius не задан руками — посчитаем автоматически
        if (bodyRadius <= 0f)
        {
            var col = GetComponent<Collider2D>();
            if (col != null)
            {
                var bounds = col.bounds; // в мировых координатах
                bodyRadius = Mathf.Max(bounds.extents.x, bounds.extents.y);
            }
            else
            {
                bodyRadius = 0.5f; // запасной вариант
            }
        }

        // separationRadius лучше сделать чуть больше диаметра
        if (separationRadius <= 0f)
        {
            separationRadius = bodyRadius * 2.5f; // можно поправить потом
        }
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer > 0f) return;
        _timer = checkInterval;

        ComputePush();
    }

    private void ComputePush()
    {
        Vector2 pos = _tr.position;
        int count = Physics2D.OverlapCircleNonAlloc(
            pos,
            separationRadius,
            _results,
            enemyLayer
        );

        Vector2 total = Vector2.zero;
        float   myDiameter = bodyRadius * 2f;

        for (int i = 0; i < count; i++)
        {
            var col = _results[i];
            if (!col || col.transform == _tr) continue;

            Vector2 otherPos = col.transform.position;
            Vector2 diff     = pos - otherPos;
            float   dist     = diff.magnitude;
            if (dist <= 0.0001f) continue;

            // цель: держать дистанцию не меньше, чем диаметр двух тел
            float targetDist = myDiameter;

            if (dist < targetDist)
            {
                float penetration = targetDist - dist;       // насколько залезли друг в друга
                float weight      = penetration / targetDist; // 0..1

                total += diff.normalized * weight;
            }
        }

        if (total.sqrMagnitude > 0.0001f)
        {
            total = total.normalized * pushStrength;
            total = Vector2.ClampMagnitude(total, maxPush);
        }
        else
        {
            total = Vector2.zero;
        }

        _pushDir = total;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, separationRadius);
    }
#endif
}