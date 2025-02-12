using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageTextManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI stageTextTMP;

    // Start is called before the first frame update
    void Start()
    {
        SetStageTextTMP();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStageTextTMP()
    {
        if (!SaveManager.Instance.GameData.isSaveExist)
        {
            stageTextTMP.text = "";

        }
        else if (SaveManager.Instance.GameData.Stage == 2)
        {
            stageTextTMP.text = "Boss";
        }
        else
        {
            stageTextTMP.text = "스테이지 " + (SaveManager.Instance.GameData.Stage + 1) + "-" + (SaveManager.Instance.GameData.process + 1);
        }
    }
}
