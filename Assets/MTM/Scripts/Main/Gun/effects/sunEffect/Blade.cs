using UnityEngine;

public class Blade : MonoBehaviour
{
    public Transform player;
    public float radius = 1.5f;
    public float rotationSpeed = 120f;
    public int damage = 20;

    private int index;
    private int totalBlades;
    private float baseAngle;

    // Для шторма
    public bool isStormBlade = false;
    private Vector2 stormDirection;
    private float currentAngle;

    void Update()
    {
        if (isStormBlade)
        {
            // Перемещение шторма
            transform.Translate(stormDirection * 10f * Time.deltaTime, Space.World);

            // Вращение вокруг собственной оси
            transform.Rotate(0, 0, 360f * Time.deltaTime);
            return;
        }

        if (player == null) return;

        // Обновляем угол вращения вокруг игрока
        currentAngle += rotationSpeed * Time.deltaTime;
        if (currentAngle >= 360f) currentAngle -= 360f;

        // Вычисляем текущую позицию по кругу
        Vector3 dir = Quaternion.Euler(0, 0, baseAngle + currentAngle) * Vector3.right;
        Vector3 targetPos = player.position + dir * radius;
        transform.position = Vector3.Lerp(transform.position, targetPos, 5f * Time.deltaTime);

        // Вращение вокруг собственной оси
        transform.Rotate(0, 0, 360f * Time.deltaTime);
    }


    public void UpdateBladePosition(int newIndex, int total)
    {
        index = newIndex;
        totalBlades = total;
        baseAngle = 360f / totalBlades * index; // Угол начальной позиции
    }

    public void SetStormDirection(Vector2 dir)
    {
        stormDirection = dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}