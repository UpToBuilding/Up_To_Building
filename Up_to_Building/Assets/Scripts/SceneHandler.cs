using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
