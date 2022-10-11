using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";

    private static readonly string MusicPref = "MusicPref";
    private static readonly string SFXPref = "SFXPref";
    
    private int firstPlayInt;
    
    public Slider SFXVolumeSlider, MusicVolumeSlider;
    private float SFXVolumeFloat, MusicVolumeFloat;

    public AudioSource[] MusicAudio;
    public AudioSource[] SFXAudio;

    // Start is called before the first frame update
    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if (firstPlayInt == 0)
        {
            SFXVolumeFloat = .25f;
            MusicVolumeFloat = .75f;

            MusicVolumeSlider.value = MusicVolumeFloat;
            SFXVolumeSlider.value = SFXVolumeFloat;

            PlayerPrefs.SetFloat(MusicPref, MusicVolumeFloat);
            PlayerPrefs.SetFloat(SFXPref, SFXVolumeFloat);

            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            MusicVolumeFloat = PlayerPrefs.GetFloat(MusicPref);
            MusicVolumeSlider.value = MusicVolumeFloat;

            SFXVolumeFloat = PlayerPrefs.GetFloat(SFXPref);
            SFXVolumeSlider.value = SFXVolumeFloat;
        }
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(MusicPref, MusicVolumeSlider.value);
        PlayerPrefs.SetFloat(SFXPref, SFXVolumeSlider.value);
    }

    private void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        { 
            SaveSoundSettings();
        }

    }

    public void UpdateSound()
    {
        for (int i = 0; i < MusicAudio.Length; i++)
        {
            MusicAudio[i].volume = MusicVolumeSlider.value;
        }

        for (int i = 0; i < SFXAudio.Length; i++)
        {
            SFXAudio[i].volume = SFXVolumeSlider.value;
        }
    }
}
