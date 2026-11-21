
using UnityEngine;

public class MouseParallax : MonoBehaviour
{
    [Header("Parallax Settings")]
    [SerializeField] private float strengthX = 0.2f;
    [SerializeField] private float strengthY = 0.1f;
    [SerializeField] private float smoothSpeed = 5f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.localPosition;
    }

    private void LateUpdate()
    {
        Vector2 mousePosition = Input.mousePosition;

        Vector2 viewportPosition = new Vector2(
            mousePosition.x / Screen.width,
            mousePosition.y / Screen.height
        );

        viewportPosition = new Vector2(
            Mathf.Clamp01(viewportPosition.x),
            Mathf.Clamp01(viewportPosition.y)
        );

        // Смещение относительно центра экрана
        Vector2 offset = (viewportPosition - Vector2.one * 0.5f) * 2f;

        // Движение фона в противоположную сторону
        Vector3 targetPosition = startPosition + new Vector3(
            -offset.x * strengthX,
            -offset.y * strengthY,
            0f
        );

        // Плавное перемещение
        float smooth = 1f - Mathf.Exp(-smoothSpeed * Time.deltaTime);

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPosition,
            smooth
        );
    }
}
