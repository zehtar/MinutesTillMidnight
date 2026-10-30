using UnityEngine;

public class ButtonEffectSpawner : MonoBehaviour
{
    [Header("Текст эффекта при нажатии")]
    public string effectText = "+1 к силе";
    public ButtonStatsAnim buttonStatsAnim;
    public Vector3 offset;
    public void OnClick()
    {
        //buttonStatsAnim = FindObjectOfType<ButtonStatsAnim>();
        //buttonStatsAnim.SummonEffect(effectText, transform.position + offset);
        AudioManager.Instance.PlayClick();
        //Destroy(gameObject);
    }
}