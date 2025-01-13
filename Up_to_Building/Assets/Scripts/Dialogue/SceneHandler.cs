using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] private Transform cutsceneImages;
    private string dialogueType;
    private int idx;
    // Start is called before the first frame update
    void Awake()
    {
        dialogueType = PlayerPrefs.GetString("DialogueType", "Opening");
    }

    void Start()
    {
        BGMManager.Instance.ChangeBGM(dialogueType);
        idx = 0;
        StartCoroutine(ActivateImagesAndShowDialogue());
    }

    IEnumerator ActivateImagesAndShowDialogue()
    {
        Transform img;
        while (img = cutsceneImages.Find(dialogueType + (++idx)))
        {
            img.gameObject.SetActive(true);
            dialogueManager.showDialogue(dialogueType + idx);

            yield return new WaitUntil(() => !dialogueManager.IsDialogueRunning());
        }

        if (dialogueType == "Opening")
        {
            PlayerPrefs.SetInt("IsPlayed", 1);
            SceneManager.LoadScene(2);
        }
        else if (dialogueType == "Ending")
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            SceneManager.LoadScene(0);
        }
    }
}
