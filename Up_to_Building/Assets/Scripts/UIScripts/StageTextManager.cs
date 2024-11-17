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

    public void SetStageTextTMP() // todo : GameManager를 Start씬에서도 가동할 수 있도록 해야함
    {
        stageTextTMP.text = "스테이지 " + (GameManager.Instance.stageNum + 1) + "-" + (GameManager.Instance.process + 1);
    }
}
