using UnityEngine;
using UnityEngine.UI;
using TMPro;


[System.Serializable]
public struct Speaker
{
    public Image dialogueImage;             // ��ȭâ �̹���
    public TextMeshProUGUI textName;        // ��� ������� ����� �̸�
    public TextMeshProUGUI textDialogue;    // ������� ���
    public GameObject objectArrow;          // ��� �Ϸ�Ǹ� ��µ� ȭ��ǥ
}

[System.Serializable]
public struct DialogueData
{
    public int speakerIndex;                // ĳ���� �迭 ����
    public string speakerName;              // ĳ���� �̸�
    public string dialogue;                 // ĳ���� ���
}

public class DialogueSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
