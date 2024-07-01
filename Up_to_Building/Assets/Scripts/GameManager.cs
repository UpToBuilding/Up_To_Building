using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float floorOffset;

    public GameObject spawner;
    public GameObject NextStage;
    public GameObject SavePoint;
    public GameObject initPoint;
    [SerializeField]
    private int maxfloor;

    [SerializeField]
    private List<GameObject> backgrond;
    [SerializeField]
    private int mapcount = 0;
    [SerializeField]
    private int stageLevel = 0;
    public int MapCount {
        get { return mapcount; }
        set
        {
            mapcount = value;
            if(mapcount == maxfloor)
            {
                NextStage.transform.localPosition = new Vector3(0, initPoint.transform.position.y, 0);
                mapcount = 0;
                backgrond[stageLevel++].gameObject.SetActive(false);
                //backgrond.RemoveAt(0);
                backgrond[stageLevel++].gameObject.SetActive(true);
               
            }
            else { NextStage.transform.localPosition += new Vector3(0, 3.7f, 0); }
        }
    }

    [SerializeField]
    private Transform PlayerTrans;

    

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

    public void NextLocation(GameObject player)
    {
        
        player.transform.position = spawner.transform.position;
        //spawner.transform.localPosition += MapCount <= 3 ? new Vector3(0, 3.7f, 0) : new Vector3(0, -14.8f, 0);
        spawner.transform.position = mapcount < maxfloor? new Vector3(spawner.transform.position.x, initPoint.transform.position.y + (floorOffset * mapcount+1), 0)
            :new Vector3(0,initPoint.transform.position.y,0);

    }




  
}
