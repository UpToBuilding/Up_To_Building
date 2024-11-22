using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private GameObject[] StateObject; // 0 : BackgroundPanel, 1 : PausePanel, 2 : SettingPanel, 3 : FailPanel, 4 : HomePanel, 5 : QuitPanel
    [SerializeField] private Transform progressBar;
    [SerializeField] private Text stageText;
    [SerializeField] private TextMeshProUGUI stageTextTMP;

    public Action JsonSaveinfo;

    public UnityEvent GameStop;
    public UnityEvent GameStart;

    public GameObject[] LodingImage;

    [SerializeField]
    private float temp;
    private void Awake()
    {
        //fail();
        //setStageText();
        //pauseUIs = GameObject.Find("PauseUIs").transform;
    }
    private void Start()
    {
        Player player = FindObjectOfType<Player>();
       
        JsonSaveinfo += player.SavePlayerInfo;

       // DontDestroyOnLoad(this.gameObject);
    }

    IEnumerator LoadingEffect()
    {
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < LodingImage.Length; i++)
            {
                if (LodingImage[i].activeSelf)
                LodingImage[i].SetActive(false);
                else LodingImage[i].SetActive(true);
                yield return new WaitForSeconds(0.33f);
            }
        }

        yield return null;
     
    }
    public void LoadingEffection()
    {
  
            StartCoroutine(LoadingEffect());
        
     
    }


    public void Pause()
    {
        temp = Time.deltaTime;
        
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0f;
        StateObject[0].SetActive(true);
        StateObject[1].SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        StateObject[0].SetActive(false);
        StateObject[1].SetActive(false);
    }

    public void OpenHomeMenu()
    {
        StateObject[4].SetActive(true);
    }

    public void CloseHomeMenu()
    {
        StateObject[4].SetActive(false);
    }

    public void GoHome()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        GameManager.Instance.SaveGameinfo();
        JsonSaveinfo();
        SaveManager.Instance.SaveJson();
        SceneManager.LoadScene("Start_Scene");
        BGMManager.Instance.ChangeBGM(null);
    }

    public void Setting()
    {
        StateObject[1].SetActive(false);
        StateObject[2].SetActive(true);
        GameManager.Instance.SaveGameinfo();
        JsonSaveinfo();
        SaveManager.Instance.SaveJson();
    }

    public void CloseSetting()
    {

        StateObject[1].gameObject.SetActive(true);
        StateObject[2].gameObject.SetActive(false);
    }


    public void Fail() // 실패 후 진행바 표시
    {
        foreach (Transform processUi in progressBar.GetComponentInChildren<Transform>()) // 진행도 초기화
        {
            processUi.GetChild(0).gameObject.SetActive(false);
        }
        for (int i = 0; i < GameManager.Instance.process; i++) // 현재 진행도까지 불 밝히기
        {
            progressBar.GetChild(i).GetChild(0).gameObject.SetActive(true);
        }
        SetStageTextTMP();
        StateObject[0].SetActive(true);
        StateObject[3].SetActive(true);
    }

    public void RetryGame()
    {
        StateObject[3].SetActive(false);
        StateObject[0].SetActive(false);
        playerUI.HealHp();
    }

    public void OpenQuitPanel()
    {
        StateObject[5].SetActive(true);
    }

    public void CloseQuitPanel()
    {
        StateObject[5].SetActive(false);
    }

    private string GetStageText()
    {
        return "스테이지 " + (GameManager.Instance.stageNum + 1) + "-" + (GameManager.Instance.process + 1);
    }

    public void SetStageText()
    {
        stageText.text = GetStageText();
    }

    public void SetStageTextTMP()
    {
        stageTextTMP.text = GetStageText();
    }

    public void gameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
