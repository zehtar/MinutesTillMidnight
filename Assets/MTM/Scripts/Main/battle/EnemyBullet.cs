using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 6f;
    public float lifetime = 5f;
    public int damage = 1;

    [Header("Physics")]
    public Rigidbody2D rb;
    
    public LayerMask solidLayer;

    private void Start()
    {
        // удаляем пулю через время
        Destroy(gameObject, lifetime);
    }

    public void SetDirection(Vector2 dir)
    {
        rb.velocity  = dir.normalized * speed;

        // поворот пули по направлению полета
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            player p = other.GetComponent<player>();

            if (p != null)
            {
                p.TakeDamage(damage);
            }

            Destroy(gameObject);
            return;
        }

        if ((solidLayer.value & (1 << other.gameObject.layer)) != 0 && other.GetComponent<Enemy>() == null)
        {
            Destroy(gameObject);
        }
    }
}