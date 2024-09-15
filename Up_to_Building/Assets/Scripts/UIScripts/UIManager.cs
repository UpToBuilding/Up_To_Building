using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private GameObject[] StateObject; // 0 : BackgroundPanel, 1 : PausePanel, 2 : SettingPanel, 3 : FailPanel, 4 : HomePanel, 5 : QuitPanel
    [SerializeField] private Transform progressBar;
    [SerializeField] private TextMeshProUGUI[] stageText;
    private int process = 2;

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


    public void fail() // ���� �� ����� ǥ��
    {
        foreach (Transform processUi in progressBar.GetComponentInChildren<Transform>()) // ���൵ �ʱ�ȭ
        {
            processUi.GetChild(0).gameObject.SetActive(false);
        }
        for (int i = 0; i < process; i++) // ���� ���൵���� �� ������
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
        foreach (TextMeshProUGUI txt in stageText)
        {
            if (txt == null) break;
            txt.text = "�������� 1-" + process;
        }
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
