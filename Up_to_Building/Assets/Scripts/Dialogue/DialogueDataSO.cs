using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speaker;
    public string line;
}

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/DialogueData")]
public class DialogueDataSO : ScriptableObject
{
    public DialogueLine[] dialogueLines;
}
