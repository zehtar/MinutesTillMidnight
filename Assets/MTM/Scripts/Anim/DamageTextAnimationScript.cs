using UnityEngine;
using TMPro;

public class DamageTextAnimationScript : MonoBehaviour
{
    public float damage;
    public bool isCrit;

    [Header("TMP Text")]
    public TextMeshProUGUI text;

    [Header("Настройки Outline")]
    public float outlineSize = 0.2f;
    public Color outlineColorNormal = Color.black;
    public Color outlineColorCrit = new Color(1f, 0.3f, 0f);

    public void Init(float damage, bool isCrit)
    {
        this.damage = damage;
        this.isCrit = isCrit;

        // Текст цифры
        string damageStr = Mathf.RoundToInt(damage).ToString();
        text.text = damageStr;

        // Основной цвет текста
        text.color = GetColorByDamage(damage);

        // Outline
        text.outlineWidth = outlineSize;
        text.outlineColor = isCrit ? outlineColorCrit : outlineColorNormal;
    }

    private Color GetColorByDamage(float damage)
    {
        if (damage < 10) return Color.white;
        if (damage < 25) return Color.yellow;
        if (damage < 50) return new Color(1f, 0.5f, 0f);
        if (damage < 100) return Color.red;
        return new Color(1f, 0f, 1f);
    }

    void RemoveObjectAndParent(GameObject obj)
    {
        if (obj != null)
        {
            Destroy(obj);
        }
    }
    // Вызывается анимацией в конце
    public void OnAnimationOver()
    {
        RemoveObjectAndParent(gameObject);
    }
}