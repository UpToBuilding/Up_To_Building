using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{

    public int maxFloor;
    public int currentFloor;
    public GameObject Background;
    public GameObject SavePoint;
    public GameObject TileObj;
    public GameObject initinfo;

    private Vector3 backinfo;

    

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
        for (int i = 0; i < 21; i++)
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
