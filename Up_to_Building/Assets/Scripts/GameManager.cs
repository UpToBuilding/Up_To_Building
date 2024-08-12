using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int maxfloor;

    private const float STAGE1_OFFSET = 4.1f;
    private const float STAGE2_OFFSET = 4.5f;

    public GameObject[] Stage;

    public List<GameObject> WallManager = new List<GameObject>();

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
                Stage[0].SetActive(false);
                Stage[1].SetActive(true);

                TileObj = null;
                TileObj = GameObject.Find("BaseWall");
                DestoryObj();
                WallManager.Clear();
                
                createObj();
                ReloactionWall(4.5f);
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
            
            DontDestroyOnLoad(this.gameObject);
        }

        Stage[0].SetActive(true);
        Stage[1].SetActive(false);
        backinfo = Background.transform.position;
        createObj();
        ReloactionWall(4.1f);
    }

    private  void createObj()
    {
        for (int i = 0; i < maxfloor; i++)
        {
            WallManager.Add(initiativeWall());
        }
    }


    private GameObject initiativeWall()
    {
     
            GameObject gm = Instantiate(TileObj, TileObj.transform.position, Quaternion.identity);
            gm.gameObject.SetActive(false);
            //gm.transform.position += new Vector3(0, offset , 0);
            

        return gm;
    }

    private void ReloactionWall(float offset)
    {
        for (int i = 1; i < maxfloor; i++)
        {
            WallManager[i-1].transform.localPosition += new Vector3(0, offset * i, 0);
            WallManager[i-1].SetActive(true);
        }
    }
    public void UpFloor()
    {
      //  Background.transform.position = backinfo;
        Player.PlayerTransform.position = Stage[1].activeSelf ? initinfo.transform.position+new Vector3(0,STAGE2_OFFSET*currentFloor,0): initinfo.transform.position + new Vector3(0, STAGE1_OFFSET * currentFloor, 0);
    }

    private void DestoryObj()
    {
        foreach (GameObject item in WallManager)
        {
            Destroy(item);
        }
    }

    

   

  




  
}
