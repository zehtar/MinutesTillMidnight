using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public UImanager UImanager;
    
    public AudioSource sfxSource;
    public AudioSource HearthSource;
    public AudioSource musicSource;

    public AudioClip shootClip;
    public AudioClip hitClip;
    public AudioClip clickClip;
    public AudioClip click2Clip;
    public AudioClip powerUpClip;
    public AudioClip BoomClip;
    public AudioClip HearthClip;
    public AudioClip[] bgmBattleClips;
    public AudioClip[] bgmMenuClips;

    private int lastIndex = -1;
    private int index;

    public Slider musicSlider;
    public Slider sfxSlider;

    public bool loadMusic = false;
    
    void Start()
    {
        float savedMusic = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        musicSource.volume = savedMusic/3;
        sfxSource.volume = savedSFX/3;
        HearthSource.volume = savedSFX / 3;

        if (musicSlider != null)
            musicSlider.value = savedMusic;

        if (sfxSlider != null)
        {
            sfxSlider.value = savedSFX;
        }
    }
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume/3;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume/3;
        HearthSource.volume = volume / 3;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }


    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void PlayClick() => PlaySFX(clickClip);
    public void PlayClick2() => PlaySFX(click2Clip);
    public void PlayPowerUp() => PlaySFX(powerUpClip);
    public void PlayShoot() => PlaySFX(shootClip);
    public void PlayHit() => PlaySFX(hitClip);
    public void PlayBoom() => PlaySFX(BoomClip);
    
    public void PlayHearth() => PlaySFX(HearthClip);

    public void Update()
    {
        if (UImanager == null)
        {
            UImanager = FindObjectOfType<UImanager>();
        }

        if (loadMusic)
        {
            if (!musicSource.isPlaying)
            {
                if (UImanager.indexScene == 0)
                {
                    PlayRandoomMenuMusic();
                }
                else
                {
                    PlayRandoomBattleMusic();
                }
            }
        }
    }

    public void PlayRandoomBattleMusic()
    {
        do
        {
            index = Random.Range(1, bgmBattleClips.Length);
        }while(index == lastIndex);
        
        lastIndex = index;
        musicSource.clip = bgmBattleClips[index];
        musicSource.Play();
    }
    public void PlayRandoomMenuMusic()
    {
        do
        {
            index = Random.Range(0, bgmMenuClips.Length);
        }while(index == lastIndex);
        
        lastIndex = index;
        musicSource.clip = bgmMenuClips[index];
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
    
    // Метод для плавного включения/выключения сердца
    public void ToggleHeartbeat(bool play)
    {
        if (play)
        {
            if (!HearthSource.isPlaying || HearthSource.clip != HearthClip)
            {
                HearthSource.clip = HearthClip;
                HearthSource.loop = true; 
                HearthSource.Play();
            }
            float targetVol = PlayerPrefs.GetFloat("SFXVolume", 0.5f) / 3f;
            HearthSource.volume = Mathf.MoveTowards(HearthSource.volume, targetVol, Time.deltaTime * 2f);
        }
        else
        {
            if (HearthSource.clip == HearthClip)
            {
                HearthSource.volume = Mathf.MoveTowards(HearthSource.volume, 0f, Time.deltaTime * 2f);
                if (HearthSource.volume <= 0.01f)
                {
                    HearthSource.Stop();
                    HearthSource.loop = false;
                }
            }
        }
    }
}