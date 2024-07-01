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
        bgmSlider.value = 1f;
        sfxSlider.value = 1f;
    }

    public void setBgmVolume(float volume)
    {
        
    }

    public void setSfxVolume(float volume)
    {

    }
}
