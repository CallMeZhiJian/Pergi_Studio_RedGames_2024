using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null; // Singleton instance

    public AudioSource bgmAudioSource; // Reference to the BGM AudioSource
    public AudioSource[] sfxAudioSources; // References to all SFX AudioSources

    public Slider bgmSlider; // Reference to the BGM volume slider
    public Slider sfxSlider; // Reference to the SFX volume slider

    private void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Load volume settings from PlayerPrefs
        LoadVolumeSettings();
    }

    private void Start()
    {
        // Initialize slider values with the loaded volume settings
        if (bgmSlider != null)
        {
            bgmSlider.value = bgmAudioSource.volume;
            bgmSlider.onValueChanged.AddListener(OnBgmSliderValueChanged);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = sfxAudioSources[0].volume;
            sfxSlider.onValueChanged.AddListener(OnSfxSliderValueChanged);
        }
    }

    public void OnBgmSliderValueChanged(float value)
    {
        bgmAudioSource.volume = value;
        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    public void OnSfxSliderValueChanged(float value)
    {
        foreach (AudioSource sfxAudioSource in sfxAudioSources)
        {
            sfxAudioSource.volume = value;
        }
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            float bgmVolume = PlayerPrefs.GetFloat("BGMVolume");
            bgmAudioSource.volume = bgmVolume;
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
            foreach (AudioSource sfxAudioSource in sfxAudioSources)
            {
                sfxAudioSource.volume = sfxVolume;
            }
        }
    }
}
