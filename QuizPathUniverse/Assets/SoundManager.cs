using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private AudioSource effectsAudioSource;

    [SerializeField] private Button backgroundToggleButton;
    [SerializeField] private Button effectsToggleButton;

    [SerializeField] private Sprite backgroundOnIcon;
    [SerializeField] private Sprite backgroundOffIcon;

    
    [SerializeField] private Sprite effectsOnIcon;
    [SerializeField] private Sprite effectsOffIcon;
  

    private void Start()
    {
        LoadAudioSettings();           
        UpdateBackgroundButtonIcon();
        UpdateEffectsButtonIcon();
        

        if (!PlayerPrefs.HasKey("BackgroundVolume") && !PlayerPrefs.HasKey("EffectsVolume"))
        {
            backgroundAudioSource.volume = 0;
            effectsAudioSource.volume = 0;
          
            SaveAudioSettings();
                     
            backgroundToggleButton.image.sprite = backgroundOffIcon;
            effectsToggleButton.image.sprite = effectsOffIcon;
           
        }
    }

    public void ToggleBackgroundVolume()
    {
        backgroundAudioSource.volume = backgroundAudioSource.volume == 1 ? 0 : 1;
        SaveAudioSettings();
        UpdateBackgroundButtonIcon();
    }

    public void ToggleEffectsVolume()
    {
        effectsAudioSource.volume = effectsAudioSource.volume == 1 ? 0 : 1;
        SaveAudioSettings();
        UpdateEffectsButtonIcon();
    }
    private void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat("BackgroundVolume", backgroundAudioSource.volume);
        PlayerPrefs.SetFloat("EffectsVolume", effectsAudioSource.volume);
        PlayerPrefs.Save();
    }

    private void LoadAudioSettings()
    {
        backgroundAudioSource.volume = PlayerPrefs.HasKey("BackgroundVolume")
            ? PlayerPrefs.GetFloat("BackgroundVolume")
            : 1;

        effectsAudioSource.volume = PlayerPrefs.HasKey("EffectsVolume")
            ? PlayerPrefs.GetFloat("EffectsVolume")
            : 1;
    }

   

    private void UpdateBackgroundButtonIcon()
    {
        backgroundToggleButton.image.sprite = backgroundAudioSource.volume == 1 ? backgroundOnIcon : backgroundOffIcon;
    }

    private void UpdateEffectsButtonIcon()
    {
        effectsToggleButton.image.sprite = effectsAudioSource.volume == 1 ? effectsOnIcon : effectsOffIcon;
    }  
    public void PlayClickSound()
    {
        effectsAudioSource.Play();
    }
}
