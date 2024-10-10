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
    private int process;

    public Action JsonSaveinfo;

    public UnityEvent GameStop;
    public UnityEvent GameStart;
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
        
    }

    public void pause()
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

    public void closeHomeMenu()
    {
        StateObject[4].SetActive(false);
    }

    public void goHome()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        GameManager.Instance.SaveGameinfo();
        JsonSaveinfo();
        SaveManager.Instance.SaveJson();
        SceneManager.LoadScene("Start_Scene");
    }

    public void setting()
    {
        StateObject[1].SetActive(false);
        StateObject[2].SetActive(true);
    }

    public void closeSetting()
    {

        StateObject[1].gameObject.SetActive(true);
        StateObject[2].gameObject.SetActive(false);
    }


    public void fail() // 실패 후 진행바 표시
    {
        foreach (Transform processUi in progressBar.GetComponentInChildren<Transform>()) // 진행도 초기화
        {
            processUi.GetChild(0).gameObject.SetActive(false);
        }
        for (int i = 0; i < process; i++) // 현재 진행도까지 불 밝히기
        {
            progressBar.GetChild(i).GetChild(0).gameObject.SetActive(true);
        }
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

    public void setStageText()
    {
        stageText.text = "스테이지 1-" + process;
    }

    public void setStageTextTMP()
    {
        stageTextTMP.text = "스테이지 1-" + process;
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
