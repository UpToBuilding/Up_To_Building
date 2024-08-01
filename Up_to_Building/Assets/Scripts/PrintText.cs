using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrintText : MonoBehaviour
{
    private string text;
    private TextMeshProUGUI targetText;
    private float delayTime;
    private Coroutine printingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        //isRunning = false;
        delayTime = 0.1f;
    }

    public void printText(TextMeshProUGUI t, string txt)
    {
        text = txt;
        targetText = t;
        targetText.text = "";
        printingCoroutine = StartCoroutine(Print());
    }
    
    IEnumerator Print()
    {
        int count = 0;
        while (count < text.Length)
        {
            targetText.text += text[count++];
            yield return new WaitForSeconds(delayTime);
        }
        printingCoroutine = null;
    }

    public bool checkCoroutine()
    {
        if (printingCoroutine != null)
        {
            targetText.text = text;
            StopCoroutine(printingCoroutine);
            printingCoroutine = null;
            return true;
        }
        return false;
    }
}