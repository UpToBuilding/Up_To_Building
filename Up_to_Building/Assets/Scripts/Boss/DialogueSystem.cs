using UnityEngine;
using UnityEngine.UI;
using TMPro;


[System.Serializable]
public struct Speaker
{
    public Image dialogueImage;             // 대화창 이미지
    public TextMeshProUGUI textName;        // 대사 출력중인 대상의 이름
    public TextMeshProUGUI textDialogue;    // 출력중인 대사
    public GameObject objectArrow;          // 대사 완료되면 출력될 화살표
}

[System.Serializable]
public struct DialogueData
{
    public int speakerIndex;                // 캐릭터 배열 순서
    public string speakerName;              // 캐릭터 이름
    public string dialogue;                 // 캐릭터 대사
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
