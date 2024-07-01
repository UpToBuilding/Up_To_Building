using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        maxIdx = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showNextText()
    {
        if (printText.checkCoroutine()) return;
        if (textIdx >= 0) tutorialTexts.GetChild(textIdx).gameObject.SetActive(false);
        if (++textIdx == maxIdx) SceneManager.LoadScene("MainScence");
        else
        {
            tutorialTexts.GetChild(textIdx).gameObject.SetActive(true);
            printText.printText(tutorialTexts.GetChild(textIdx).GetChild(0).GetComponent<Text>());
        }
    }
}
