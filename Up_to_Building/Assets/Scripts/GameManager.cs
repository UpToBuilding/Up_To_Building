using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int maxfloor;


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
        backinfo = Background.transform.position;
        for (int i = 0; i < maxfloor; i++)
        {
            GameObject gm =  Instantiate(TileObj,TileObj.transform.position,Quaternion.identity);
            gm.gameObject.SetActive(false);
            gm.transform.position +=  new Vector3(0, 4.1f*i, 0);
            gm.gameObject.SetActive(true) ;
        }
     
    }

    public void UpFloor()
    {
      //  Background.transform.position = backinfo;
        Player.PlayerTransform.position = initinfo.transform.position+new Vector3(0,4.1f*currentFloor,0);
    }



    

   

  




  
}
