using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Audio_MainMenu : MonoBehaviour
{
    public static Audio_MainMenu instance = null; // Singleton instance

    public AudioSource mainMenuBGMAudioSource; // Main menu BGM AudioSource
    public AudioSource bgmAudioSource1; // Reference to the 1st in-game BGM AudioSource
    public AudioSource bgmAudioSource2; // Reference to the 2nd in-game BGM AudioSource
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
            bgmSlider.value = mainMenuBGMAudioSource.volume;
            bgmSlider.onValueChanged.AddListener(OnBgmSliderValueChanged);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = sfxAudioSources.Length > 0 ? sfxAudioSources[0].volume : 1.0f;
            sfxSlider.onValueChanged.AddListener(OnSfxSliderValueChanged);
        }

        PlayMainMenuBGM();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckAndPlayBGMs();
    }

    public void OnBgmSliderValueChanged(float value)
    {
        mainMenuBGMAudioSource.volume = value;
        bgmAudioSource1.volume = value;
        bgmAudioSource2.volume = value;
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
            mainMenuBGMAudioSource.volume = bgmVolume;
            bgmAudioSource1.volume = bgmVolume;
            bgmAudioSource2.volume = bgmVolume;
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

    // Just call this method to play the particular sound effect 
    //How: Audio_MainMenu.instance.PlaySFX(1);
    public void PlaySFX(int index)
    {
        if (index >= 0 && index < sfxAudioSources.Length)
        {
            sfxAudioSources[index].Play();
        }
        else
        {
            Debug.LogWarning("SFX index out of range.");
        }
    }

    // Method to play the main menu BGM
    private void PlayMainMenuBGM()
    {
        if (!mainMenuBGMAudioSource.isPlaying)
        {
            mainMenuBGMAudioSource.loop = true;
            mainMenuBGMAudioSource.Play();
        }

       
    }

    private void CheckAndPlayBGMs()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "KW_testing")
        {
            // Stop main menu BGM if it's playing
            if (mainMenuBGMAudioSource.isPlaying)
            {
                mainMenuBGMAudioSource.Stop();
            }

            // Play in-game BGMs
            if (!bgmAudioSource1.isPlaying)
            {
                bgmAudioSource1.loop = false;
                bgmAudioSource1.Play();
            }
            if (!bgmAudioSource2.isPlaying)
            {
                bgmAudioSource2.loop = true;
                bgmAudioSource2.Play();
            }
        }
        else
        {
            // Play main menu BGM if in a different scene
            PlayMainMenuBGM();

            // Stop in-game BGMs if they are playing
            if (bgmAudioSource1.isPlaying)
            {
                bgmAudioSource1.Stop();
            }
            if (bgmAudioSource2.isPlaying)
            {
                bgmAudioSource2.Stop();
            }
        }
    }


}
