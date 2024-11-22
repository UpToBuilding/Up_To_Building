using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;



public class GameManager : MonoBehaviour
{
    [SerializeField] UIManager uiManager;

    [SerializeField]
    private int maxfloor;

    private const float STAGE1_OFFSET = 4.1f;
    private const float STAGE2_OFFSET = 4.5f;
    public bool ischangeMap;

    public GameObject[] Stage;

 

    [SerializeField]
    private int currentfloor;

    public int currentFloor
    {
        get { return currentfloor; }
        set
        {
            currentfloor = value;
            if (maxfloor == currentfloor)
            {


                
                stageNum++;
                ChangeStage(stageNum);

         
                //StartCoroutine(NextStageTerm());    
                
            }
        }
    }
    public GameObject DoorTrigger;
    public GameObject Background;
    public GameObject SavePoint;
    public GameObject TileObj;
    public GameObject initinfo;

    private Vector3 backinfo;
 
    private int tempfloor;
    public int TempFloor { get; set; }

    public int stageNum = 0;

    public int process = 0;

    public bool isResponeCheck;

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
            uiManager.LoadingEffection();
         
            StartCoroutine(ChaningBossScence(num));
        }
    }

    IEnumerator ChaningBossScence(int num)
    {
        Stage[num].SetActive(true);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(3);
    }



}

public class GameManagerData
{
    public int Stage = 0;
    public int process = 0;
    public int currentfloor = 1;
}
