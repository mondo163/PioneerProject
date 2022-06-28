using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class SceneAudioControl : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer soundFXMixer;
    public Slider musicSlider = null;
    public Slider soundFXSlider = null;

    private void Start()
    {
        musicMixer.SetFloat("MusicVol", Mathf.Log10(.25f) * 20);
        soundFXMixer.SetFloat("SoundFXVol", Mathf.Log10(.25f) * 20);

    }
    public void SetMusicLevel(float sliderValue)
    {
        musicMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSoundFXVolume(float sliderValue)
    {
        soundFXMixer.SetFloat("SoundFXVol", Mathf.Log10(sliderValue) * 20);
    }
}
