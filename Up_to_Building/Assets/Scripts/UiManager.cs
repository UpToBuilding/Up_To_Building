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

    public void OnMainSence()
    {
        SceneManager.LoadScene(1);
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
}
