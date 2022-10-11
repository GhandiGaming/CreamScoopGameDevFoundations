using UnityEngine;

public class MaintainAudioSettings : MonoBehaviour
{
    private static readonly string MusicPref = "MusicPref";
    private static readonly string SFXPref = "SFXPref";

    private float SFXVolumeFloat, MusicVolumeFloat;

    public AudioSource[] MusicAudio;
    public AudioSource[] SFXAudio;

    void Awake()
    {
        ContinueSettings();
    }

    private void ContinueSettings()
    {
        MusicVolumeFloat = PlayerPrefs.GetFloat(MusicPref);
        SFXVolumeFloat = PlayerPrefs.GetFloat(SFXPref);

        for (int i = 0; i < MusicAudio.Length; i++)
        {
            MusicAudio[i].volume = MusicVolumeFloat;
        }

        for (int i = 0; i < SFXAudio.Length; i++)
        {
            SFXAudio[i].volume = SFXVolumeFloat;
        }
    }
}

