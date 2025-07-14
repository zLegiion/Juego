using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    [SerializeField]private Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer;

    private const string VolumePrefKey = "VolumeLevel";


    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 0.75f);

        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);
        volumeSlider.onValueChanged.AddListener(SetVolume);

    }

    public void SetVolume(float volume)
    {

        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("MasterVolume", dB);

        PlayerPrefs.SetFloat(VolumePrefKey, volume);
        PlayerPrefs.Save();
    }
}
