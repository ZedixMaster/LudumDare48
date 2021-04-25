using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider effectsSlider;
    public Slider musicSlider;
    public Toggle fullscreenToggle;

    private float effectsVolume;
    private float musicVolume;
    private bool isFullscreen;

    private void Start()
    {
        LoadSettings();
    }

    public void ChangeEffectVolume(float volume)
    {
        Debug.Log("Changing effects to: " + volume);
        effectsVolume = volume;
        mixer.SetFloat("effects", volume);
    }
    public void ChangeMusicVolume(float volume)
    {
        Debug.Log("Changing music to: " + volume);
        musicVolume = volume;
        mixer.SetFloat("music", volume);
    }

    public void ChangeFullscreenMode(bool isOn) 
    {
        Debug.Log("Changing fullscreen mode to: " + isOn);
        isFullscreen = isOn;
        Screen.fullScreenMode = isOn ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;
    }

    public void LoadSettings()
    {
        effectsVolume = PlayerPrefs.GetFloat("effects");
        musicVolume = PlayerPrefs.GetFloat("music");
        isFullscreen = PlayerPrefs.GetInt("fullscreen") == 1;

        effectsSlider.value = effectsVolume;
        musicSlider.value = musicVolume;
        fullscreenToggle.isOn = isFullscreen;
    }

    public void SaveChanges()
    {
        Debug.Log("Saving");
        PlayerPrefs.SetInt("fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.SetFloat("effects", effectsVolume);
        PlayerPrefs.SetFloat("music", musicVolume);
        PlayerPrefs.Save();
    }
}
