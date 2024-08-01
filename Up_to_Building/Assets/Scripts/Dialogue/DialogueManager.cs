using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private PrintText printText;
    [SerializeField] private TextMeshProUGUI textFrame;
    private string dialogueDataName;
    private DialogueDataSO dialogueData;
    private int currentLineIndex;
    private bool isDialogueRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void showDialogue(string sceneName)
    {
        isDialogueRunning = true;
        dialogueDataName = sceneName + "Dialogue";
        currentLineIndex = 0;
        LoadDialogueData();
        DisplayCurrentLine();
    }

    void LoadDialogueData()
    {
        dialogueData = Resources.Load<DialogueDataSO>("Dialogue/" + dialogueDataName);
        if (dialogueData == null)
        {
            dialogueData = ScriptableObject.CreateInstance<DialogueDataSO>();
            dialogueData.dialogueLines = new DialogueLine[1];
            dialogueData.dialogueLines[0] = new DialogueLine { line = "" };
        }
    }

    public void DisplayNextLine()
    {
        if (printText.checkCoroutine()) return;
        currentLineIndex++;
        if (currentLineIndex < dialogueData.dialogueLines.Length)
        {
            DisplayCurrentLine();
        }
        else
        {
            isDialogueRunning = false;
            Debug.Log("End of dialogue.");
        }
    }

    void DisplayCurrentLine()
    {
        DialogueLine line = dialogueData.dialogueLines[currentLineIndex];
        printText.printText(textFrame, line.line);
    }

    public bool IsDialogueRunning()
    {
        return isDialogueRunning;
    }
}
