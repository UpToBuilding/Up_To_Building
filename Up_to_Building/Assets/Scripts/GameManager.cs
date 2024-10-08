using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;



public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int maxfloor;

    private const float STAGE1_OFFSET = 4.1f;
    private const float STAGE2_OFFSET = 4.5f;

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

                currentFloor = 1;
      
                Player.PlayerTransform.GetComponent<Player>().gameObject.transform.position = initinfo.transform.position;
            }
        }
    }
    public GameObject Background;
    public GameObject SavePoint;
    public GameObject TileObj;
    public GameObject initinfo;

    private Vector3 backinfo;
 
    private int tempfloor;
    public int TempFloor { get; set; }

    public int stageNum = 0;

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
        ChangeStage(stageNum);
        currentfloor = SaveManager.Instance.GameData.currentfloor;
        backinfo = Background.transform.position;
       
    }

    public void SaveGameinfo()
    {
        SaveManager.Instance.GameData.currentfloor =currentFloor;
        SaveManager.Instance.GameData.Stage = stageNum;
    }


 


    public void UpFloor()
    {
      //  Background.transform.position = backinfo;
        Player.PlayerTransform.position = Stage[1].activeSelf ? initinfo.transform.position+new Vector3(0,STAGE2_OFFSET*currentFloor,0): initinfo.transform.position + new Vector3(0, STAGE1_OFFSET * currentFloor, 0);
    }


    public void ChangeStage(int num)
    {
        if (num == 0)
        {
            Stage[0].SetActive(true);
            Stage[1].SetActive(false);
            BGMManager.Instance.ChangeBGM("Company");
        }
        else
        {
            Stage[0].SetActive(false);
            Stage[1].SetActive(true);
            BGMManager.Instance.ChangeBGM("Dungeon");
        }
    }




}

public class GameManagerData
{
    public int Stage = 0;
    public int currentfloor = 1;
}
