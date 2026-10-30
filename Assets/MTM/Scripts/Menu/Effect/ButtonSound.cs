using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(PlayClickSound);
    }

    void PlayClickSound()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayClick();
    }
    
    public void PlayClickSFX()
    {
        AudioManager.Instance.PlayClick2();
    }
}