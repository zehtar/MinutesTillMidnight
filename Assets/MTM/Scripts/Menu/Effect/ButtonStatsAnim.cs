using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStatsAnim : MonoBehaviour
{
    public static ButtonStatsAnim Instance;

    public Text textEffectPrefab;
    public Image circleEffectPrefab;

    private Canvas canvas;
    private RectTransform canvasRect;
    private Camera cam;

    private void Start()
    {
        Instance = this;

        canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();

        if (canvas.renderMode == RenderMode.ScreenSpaceCamera || canvas.renderMode == RenderMode.WorldSpace)
        {
            cam = canvas.worldCamera;
        }
        else
        {
            cam = null;
        }
    }

    public void SummonEffect(string _text, Vector3 worldPos)
    {
        // Конвертация мировых координат в локальные координаты для Canvas
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(cam, worldPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, cam, out Vector2 localPoint);

        // Спавн текста
        var text = Instantiate(textEffectPrefab, canvas.transform, false);
        text.rectTransform.anchoredPosition = localPoint;
        text.text = _text;

        // Спавн кружка
        var circle = Instantiate(circleEffectPrefab, canvas.transform, false);
        circle.rectTransform.anchoredPosition = localPoint;
        StartCoroutine(AnimateCircle(circle));
    }

    private IEnumerator AnimateCircle(Image circle)
    {
        float duration = 0.5f;
        float time = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one * 4f;

        Color startColor = circle.color;
        Color endColor = startColor;
        endColor.a = 0f;

        while (time < duration)
        {
            float t = time / duration;
            circle.rectTransform.localScale = Vector3.Lerp(startScale, endScale, t);
            circle.color = Color.Lerp(startColor, endColor, t);

            time += Time.deltaTime;
            yield return null;
        }

        Destroy(circle.gameObject);
    }
}
