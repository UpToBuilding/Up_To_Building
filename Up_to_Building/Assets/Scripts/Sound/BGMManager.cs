using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip openingBGM;
    private AudioClip endingBGM;
    private AudioClip companyBGM;
    private AudioClip dungeonBGM;
    private string bgmAddr = "Audio/BGM/";

    private static BGMManager instance;
    public static BGMManager Instance { get => instance; }


    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        openingBGM = Resources.Load<AudioClip>(bgmAddr + "OpeningBGM");
        endingBGM = Resources.Load<AudioClip>(bgmAddr + "EndingBGM");
        companyBGM = Resources.Load<AudioClip>(bgmAddr + "CompanyBGM");
        dungeonBGM = Resources.Load<AudioClip>(bgmAddr + "DungeonBGM");
    }

    public void ChangeBGM(string name)
    {
        AudioClip clip = GetComponent<AudioClip>();
        if (name == "Opening") clip = openingBGM;
        else if (name == "Ending") clip = endingBGM;
        else if (name == "Company") clip = companyBGM;
        else if (name == "Dungeon") clip = dungeonBGM;
        else clip = null;
        if (clip == null)
        {
            audioSource.Stop();
            return;
        }
        audioSource.clip = clip;
        audioSource.Play();
    }
}
