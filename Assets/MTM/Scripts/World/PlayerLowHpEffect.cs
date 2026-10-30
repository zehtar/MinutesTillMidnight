using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerLowHpEffect : MonoBehaviour
{
    public PostProcessVolume globalVolume;
    private Vignette vignette;

    void Start()
    {
        if (globalVolume.profile.TryGetSettings(out vignette))
        {
            vignette.intensity.overrideState = true;
        }
        
        // Настраиваем AudioSource заранее
       
    }

    public void UpdateVignette(float healthPercent, bool music = true)
    {
        UpdateVignette(healthPercent);
        if (music)
        {
            AudioManager.Instance.ToggleHeartbeat(true);
        }
        else
        {
            AudioManager.Instance.ToggleHeartbeat(false);
        }
    }

    public void UpdateVignette(float healthPercent)
    {
        if (vignette == null) return;

        float threshold = 0.3f;

        if (healthPercent <= threshold)
        {
            // Визуал
            vignette.color.value = Color.red;
            float dangerLevel = healthPercent / threshold;
            float pulse = Mathf.Sin(Time.time * 6f) * 0.1f;
            vignette.intensity.value = Mathf.Lerp(0.8f, 0f, dangerLevel) + pulse;
        }
        else
        {
            // Возвращаем виньетку в норму
            vignette.color.value = Color.black;
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0.45f, Time.deltaTime * 2f);

            // Просим затушить звук
            AudioManager.Instance.ToggleHeartbeat(false);
        }
    }
}
