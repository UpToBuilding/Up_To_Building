using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private PrintTexts printTexts;
    [SerializeField] private Transform tutorialTexts;
    private int textIdx;
    private int maxIdx;
    // Start is called before the first frame update
    void Awake()
    {
        textIdx = 0;
        maxIdx = 2;
        printTexts.printText(tutorialTexts.GetChild(0).GetChild(0).GetComponent<Text>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showNextText()
    {
        if (printTexts.checkCoroutine()) return; // 텍스트 출력중이면 코루틴 중단 및 텍스트를 바로 다 채움

        tutorialTexts.GetChild(textIdx++).gameObject.SetActive(false);
        if (textIdx == maxIdx) SceneManager.LoadScene("MainScence");
        else
        {
            tutorialTexts.GetChild(textIdx).gameObject.SetActive(true);
            printTexts.printText(tutorialTexts.GetChild(textIdx).GetChild(0).GetComponent<Text>());
        }
    }
}
