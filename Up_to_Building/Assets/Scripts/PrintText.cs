using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintText : MonoBehaviour
{
    private string text;
    private Text targetText;
    private float delayTime;
    private bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        isRunning = false;
        delayTime = 0.1f;
    }

    public void printText(Text t)
    {
        text = t.text.ToString();
        targetText = t;
        targetText.text = "";
        StartCoroutine("Print");
    }
    
    IEnumerator Print()
    {
        isRunning = true;
        int count = 0;

        while (count < text.Length)
        {
            targetText.text += text[count++];
            yield return new WaitForSeconds(delayTime);
        }
        isRunning = false;
    }

    public bool checkCoroutine()
    {
        if (isRunning)
        {
            isRunning = false;
            targetText.text = text;
            StopCoroutine("Print");
            return true;
        }
        return false;
    }
}