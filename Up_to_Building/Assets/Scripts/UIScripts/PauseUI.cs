using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseUI : MonoBehaviour
{
    private Transform pauseUIs; // 0 : PauseButton, 1 : PausePanel, 2 : SettingPanel

    private void Awake()
    {
        pauseUIs = GameObject.Find("PauseUIs").transform;
    }

    public void pause()
    {
        Time.timeScale = 0;
        pauseUIs.GetChild(0).gameObject.SetActive(false); // PauseButton
        pauseUIs.GetChild(1).gameObject.SetActive(true); // PausePanel
    }

    public void home()
    {
        pauseUIs.GetChild(3).gameObject.SetActive(true);
    }

    public void cancelHome()
    {
        pauseUIs.GetChild(3).gameObject.SetActive(false);
    }

    public void goHome()
    {
        SceneManager.LoadScene("Start_Scene");
    }

    public void setting()
    {
        pauseUIs.GetChild(1).gameObject.SetActive(false); // PausePanel
        pauseUIs.GetChild(2).gameObject.SetActive(true); // SettingPanel
    }

    public void closeSetting()
    {
        pauseUIs.GetChild(1).gameObject.SetActive(true); // PausePanel
        pauseUIs.GetChild(2).gameObject.SetActive(false); // SettingPanel
    }

    public void resume()
    {
        Time.timeScale = 1;
        pauseUIs.GetChild(0).gameObject.SetActive(true); // PauseButton
        pauseUIs.GetChild(1).gameObject.SetActive(false); // PausePanel
    }
}
