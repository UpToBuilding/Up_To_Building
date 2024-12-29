//using static UnityEditor.Progress;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;




public class GameManager : MonoBehaviour
{
    [SerializeField] UIManager uiManager;

    [SerializeField]
    private int maxfloor;

    private const float STAGE1_OFFSET = 4.1f;
    private const float STAGE2_OFFSET = 4.5f;
    public bool ischangeMap;

    public GameObject[] Stage;

    public GameObject[] BossStage;

    public Transform BossPos;
    [SerializeField]
    private int currentfloor;

    public int currentFloor
    {
        get { return currentfloor; }
        set
        {
            currentfloor = value;
            if (currentfloor % 2 != 0&&stageNum==1)
            {
                Stage[4].gameObject.SetActive(false);
            }
            else
            {
                Stage[4].gameObject.SetActive(true);
            }
            if (maxfloor == currentfloor)
            {           
                stageNum++;
                process = 0;
                ChangeStage(stageNum);      
            }
        }
    }
    public GameObject DoorTrigger;
    public GameObject Background;
    public GameObject SavePoint;
    public GameObject TileObj;
    public GameObject initinfo;

    public GameObject[] cam;

    private Vector3 backinfo;
 
    private int tempfloor;
    public int TempFloor { get; set; }

    public int stageNum = 0;

    public int process = 0;

    public bool isResponeCheck;
    public bool isBoss;

    private static GameManager instance;
    public static GameManager Instance { 
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
       
        if (instance == null)
        {
            
            instance = this;
      
        }
        isBoss = false;
        stageNum = SaveManager.Instance.GameData.Stage;
        process = SaveManager.Instance.GameData.process;
        currentfloor = SaveManager.Instance.GameData.currentfloor;
        backinfo = Background.transform.position;
       
    }

    void Start()
    {
        ChangeStage(stageNum);
  
         uiManager.SetStageText();
    }

    public void SaveGameinfo()
    {
        SaveManager.Instance.GameData.currentfloor = currentFloor;
        SaveManager.Instance.GameData.process = process;
        SaveManager.Instance.GameData.Stage = stageNum;
    }

    IEnumerator NextStageTerm()
    {
        yield return new WaitForSeconds(1.5f);
        Player.PlayerTransform.GetComponent<Player>().gameObject.transform.position = initinfo.transform.position;
        yield return null;
    }

    IEnumerator FloorTime()
    {
        yield return new WaitForSeconds(2f);
        Upact();
    
        currentFloor++;
        yield return null;
    }

    private void Upact()
    {
        Player.PlayerTransform.position = Stage[1].activeSelf ? initinfo.transform.position + new Vector3(0, STAGE2_OFFSET * currentFloor, 0) : initinfo.transform.position + new Vector3(0, STAGE1_OFFSET * currentFloor, 0);
       
    }

    public void UpFloor()
    {
      //  Background.transform.position = backinfo;
       StartCoroutine(FloorTime());
    }


    public void ChangeStage(int num)
    {

        if (num == 0)
        {
            Stage[0].SetActive(true);
            Stage[1].SetActive(false);
            BossStage[0].SetActive(false);
            BGMManager.Instance.ChangeBGM("Company");
        }
        else if(num == 1)
        {
            Stage[0].SetActive(false);
            Stage[1].SetActive(true);
            Player.PlayerTransform.GetComponent<Player>().gameObject.transform.position = initinfo.transform.position;
            currentFloor = 1;
            BGMManager.Instance.ChangeBGM("Dungeon");
        }else if(num == 2)
        {
            SaveGameinfo();
            uiManager.JsonSaveinfo();
            SaveManager.Instance.SaveJson(); 
            uiManager.LoadingEffection();
            StartCoroutine(ScenceChaing());
      
           
            
            StartCoroutine(ChaningBossScence(num));
        }
    }

    IEnumerator ScenceChaing()
    {
        Stage[3].SetActive(false);
        BossStage[0].SetActive(true);
        Player.PlayerTransform.GetComponent<Player>().BossResetPlayer(BossPos);
      
        yield return new WaitForSeconds(1.0f);
        CamSetting();
        yield return null;
    }

    IEnumerator ChaningBossScence(int num)
    {
        Stage[num].SetActive(true);
        yield return new WaitForSeconds(4f);
        Stage[num].SetActive(false);
        cam[3].gameObject.SetActive(true);
        BossStage[1].SetActive(true);
        isBoss = true;

    }
    public void Cam3Down()
    {
        cam[3].gameObject.SetActive(false);
    }

    public void CamSetting()
    {
        cam[0].gameObject.SetActive(false);
        cam[1].gameObject.SetActive(false);
        cam[2].gameObject.SetActive(true);
      
    }

}

public class GameManagerData
{
    public int Stage = 0;
    public int process = 0;
    public int currentfloor = 1;
    public bool isSaveExist = false;
}
