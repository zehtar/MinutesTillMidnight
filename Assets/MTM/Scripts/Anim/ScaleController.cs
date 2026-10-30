using UnityEngine;

public class ScaleController : MonoBehaviour
{
    private Vector3 baseScale; // Базовый размер объекта.
    private Vector3 targetScale; // Целевой размер для анимации.
    private float scaleSpeed = 12f; // Скорость изменения размера.
    private bool isScaling = false; // Флаг для запуска анимации.

    void Start()
    {
        baseScale = transform.localScale; // Сохраняем изначальный размер.
        targetScale = baseScale; // Устанавливаем целевой размер равным базовому.
    }

    void Update()
    {
        if (isScaling)
        {
            // Плавное изменение размера с использованием Lerp.
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);

            // Останавливаем анимацию, когда объект достигает целевого размера.
            if (Vector3.Distance(transform.localScale, targetScale) < 0.1f)
            {
                transform.localScale = targetScale;
                isScaling = false;
            }
        }
    }

    public void IncreaseScale(float amount)
    {
        // Увеличиваем целевой размер.
        targetScale = baseScale * (1 + amount);
        isScaling = true;
    }

    public void DecreaseScale(float amount)
    {
        // Уменьшаем целевой размер.
        targetScale = baseScale * (1 - amount);
        isScaling = true;
    }
}
