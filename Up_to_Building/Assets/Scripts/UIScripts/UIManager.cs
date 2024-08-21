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
    [SerializeField]
    private Transform pauseUis;
    public GameObject[] StateButton; // 0 : PauseButton, 1 : PausePanel, 2 : SettingPanel
    [SerializeField] private Transform progressBar;
    [SerializeField] private TextMeshProUGUI stageText;
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

    public void closePanel()
    {
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
        clickedObject.transform.parent.gameObject.SetActive(false);
    }

    public void pause()
    {
        temp = Time.deltaTime;
        
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0f;
        StateButton[0].gameObject.SetActive(true);
        //pauseUis.GetChild(0).gameObject.SetActive(false); // PauseButton
        //sta.gameObject.SetActive(true); // PausePanel
    }

    public void Resume()
    {
        
        
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        StateButton[0].gameObject.SetActive(false);

        //pauseUis.GetChild(0).gameObject.SetActive(true); // PauseButton
        //pauseUis.GetChild(1).gameObject.SetActive(false); // PausePanel
    }

    public void OpenHomeMenu()
    {
        //StateButton[0].gameObject.SetActive(false);
        StateButton[2].gameObject.SetActive(true);
        //pauseUis.GetChild(3).gameObject.SetActive(true);
    }

    public void closeHomeMenu()
    {
        StateButton[2].gameObject.SetActive(false) ;
        //pauseUis.GetChild(3).gameObject.SetActive(false);
    }

    public void goHome()
    {

        SceneManager.LoadScene("Start_Scene");
    }

    public void setting()
    {
        StateButton[3].gameObject.SetActive(false);
        StateButton[1].gameObject.SetActive(true);
        //pauseUis.GetChild(1).gameObject.SetActive(false); // PausePanel
        //pauseUis.GetChild(2).gameObject.SetActive(true); // SettingPanel
    }

    public void closeSetting()
    {

        StateButton[3].gameObject.SetActive(true);
        StateButton[1].gameObject.SetActive(false);
        // pauseUis.GetChild(1).gameObject.SetActive(true); // PausePanel
        //pauseUis.GetChild(2).gameObject.SetActive(false); // SettingPanel
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
        progressBar.gameObject.SetActive(true);
    }

    public void setStageText()
    {
        stageText.text = "스테이지 1-" + process;
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
