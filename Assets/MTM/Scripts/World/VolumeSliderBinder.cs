using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderBinder : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        // Подключаем методы
        if (musicSlider != null)
            musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);

        if (sfxSlider != null)
            sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);

        // Загружаем сохранённые значения
        float savedMusic = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        musicSlider.value = savedMusic;
        sfxSlider.value = savedSFX;
    }
}
