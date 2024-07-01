using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintTexts : MonoBehaviour
{
    private string text;
    private Text textUi;
    private float delayTime;
    private bool isCoroutineRunning;

    // Start is called before the first frame update
    void Awake()
    {
        delayTime = 0.2f;
        isCoroutineRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void printText(Text text)
    {
        this.text = text.text.ToString();
        textUi = text;
        textUi.text = "";

        StartCoroutine("Print");
    }

    IEnumerator Print()
    {
        isCoroutineRunning = true;
        int count = 0;

        while (count < text.Length)
        {
            textUi.text += text[count++];
            yield return new WaitForSeconds(delayTime);
        }
        isCoroutineRunning = false;
    }

    public bool checkCoroutine()
    {
        if (isCoroutineRunning)
        {
            textUi.text = text;
            isCoroutineRunning = false;
            StopCoroutine("Print");

            return true;
        }

        return false;
    }
}
