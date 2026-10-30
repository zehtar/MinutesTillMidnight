using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTextEffectCorutine : MonoBehaviour
{
    private RectTransform rectTransform;
    private Text text;
    private float rotationDirection;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponent<Text>();

        // Рандомное направление вращения
        rotationDirection = Random.Range(-30f, 30f);
    }

    private void OnEnable()
    {
        StartCoroutine(PlayEffect());
    }

    private IEnumerator PlayEffect()
    {
        float duration = 1f;
        float time = 0f;

        
        Vector3 startScale = Vector3.one * 0.8f;
        Vector3 endScale = Vector3.one * 1.2f;

        Color startColor = text.color;
        Color endColor = startColor;
        endColor.a = 0;

        while (time < duration)
        {
            float t = time / duration;
            
            // Масштабируем
            rectTransform.localScale = Vector3.Lerp(startScale, endScale, t);
            // Вращаем
            rectTransform.localRotation = Quaternion.Euler(0, 0, rotationDirection * t * 2f);
            // Затухание
            text.color = Color.Lerp(startColor, endColor, t);

            time += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

}
