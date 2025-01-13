using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Transform tutorialTexts;
    [SerializeField] private PrintText printText;
    private int textIdx;
    private int maxIdx;
    // Start is called before the first frame update
    void Start()
    {
        textIdx = -1;
        maxIdx = tutorialTexts.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTutorial()
    {
        tutorialTexts.gameObject.SetActive(true);
        ShowNextText();
    }

    public void ShowNextText()
    {
        if (printText.checkCoroutine()) return;
        if (textIdx >= 0) tutorialTexts.GetChild(textIdx).gameObject.SetActive(false);
        if (++textIdx == maxIdx) SceneLoader.LoadSceneWithData("CutScene", "Opening");
        else
        {
            Transform targetText = tutorialTexts.GetChild(textIdx);
            TextMeshProUGUI targetTmp = targetText.GetChild(0).GetComponent<TextMeshProUGUI>();

            targetText.gameObject.SetActive(true);
            printText.printText(targetTmp, targetTmp.text);
        }
    }
}
