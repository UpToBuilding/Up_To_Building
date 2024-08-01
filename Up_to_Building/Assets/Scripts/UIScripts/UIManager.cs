using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform pauseUis; // 0 : PauseButton, 1 : PausePanel, 2 : SettingPanel
    [SerializeField] private Transform progressBar;
    [SerializeField] private TextMeshProUGUI stageText;
    private int process = 2;

    private void Awake()
    {
        fail();
        setStageText();
        //pauseUIs = GameObject.Find("PauseUIs").transform;
    }

    public void closePanel()
    {
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
        clickedObject.transform.parent.gameObject.SetActive(false);
    }

    public void pause()
    {
        Time.timeScale = 0;
        pauseUis.GetChild(0).gameObject.SetActive(false); // PauseButton
        pauseUis.GetChild(1).gameObject.SetActive(true); // PausePanel
    }

    public void openHomeMenu()
    {
        pauseUis.GetChild(3).gameObject.SetActive(true);
    }

    public void closeHomeMenu()
    {
        pauseUis.GetChild(3).gameObject.SetActive(false);
    }

    public void goHome()
    {

        SceneManager.LoadScene("Start_Scene");
    }

    public void setting()
    {
        pauseUis.GetChild(1).gameObject.SetActive(false); // PausePanel
        pauseUis.GetChild(2).gameObject.SetActive(true); // SettingPanel
    }

    public void closeSetting()
    {
        pauseUis.GetChild(1).gameObject.SetActive(true); // PausePanel
        pauseUis.GetChild(2).gameObject.SetActive(false); // SettingPanel
    }

    public void resume()
    {
        Time.timeScale = 1;
        pauseUis.GetChild(0).gameObject.SetActive(true); // PauseButton
        pauseUis.GetChild(1).gameObject.SetActive(false); // PausePanel
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
