using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {

    }
    public void LoadSceneWithData(string sceneName, string dialogueType)
    {
        PlayerPrefs.SetString("DialogueType", dialogueType);
        PlayerPrefs.Save();

        SceneManager.LoadScene(sceneName);
        BGMManager.Instance.ChangeBGM(dialogueType);
    }
}
