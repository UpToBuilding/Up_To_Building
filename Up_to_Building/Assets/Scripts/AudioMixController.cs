using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMixController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    // Start is called before the first frame update
    void Awake()
    {
        bgmSlider.onValueChanged.AddListener(setBgmVolume);
        sfxSlider.onValueChanged.AddListener(setSfxVolume);
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("BGMVolume")) bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        else bgmSlider.value = 1f;

        if (PlayerPrefs.HasKey("SFXVolume")) sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        else sfxSlider.value = 1f;

        audioMixer.SetFloat("BGM", Mathf.Log10(bgmSlider.value) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxSlider.value) * 20);
    }

    public void setBgmVolume(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void setSfxVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}
