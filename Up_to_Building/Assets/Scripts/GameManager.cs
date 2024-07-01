using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    

    //public GameObject spawner;
    //public GameObject NextStage;
    public GameObject SavePoint;

    



    

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

     
    }




  

   

  




  
}
