using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    [SerializeField]
    private GameObject SettingPanel;
    [SerializeField]
    private GameObject CreditPanel;
    [SerializeField] private Tutorial tutorial;

    private string isPlayed = "IsPlayed";


    public void OnMainSence()
    {
        SaveManager.Instance.newGame_DeleteJson();
        if (PlayerPrefs.GetInt(isPlayed, 0) == 1)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            tutorial.StartTutorial();
        }
    }

    public void OnReLoad()
    {
        SaveManager.Instance.LoadData();
        if (SaveManager.Instance.GameData.isSaveExist)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void OnSettingPanel()
    {
        SettingPanel.SetActive(true);
    }
    public void OffSettingPanel()
    {
        SettingPanel.SetActive(false);
    }

    public void OnCreditPanel()
    {
        CreditPanel.SetActive(true);
    }

    public void OffCreditPanel()
    {
        CreditPanel.SetActive(false);
    }

    [ContextMenu("Reset IsPlayed")]
    public void ResetIsPlayed()
    {
        PlayerPrefs.DeleteKey(isPlayed);
    }
}
