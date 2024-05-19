using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Dragon;


   


    [SerializeField]
    private Transform PlayerTrans;

    public Vector2 Check_Pos;

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




    private void DragonSys()
    {
        StartCoroutine(DrangonSpowner());
    }

    IEnumerator DrangonSpowner()
    {
        Dragon.transform.position = new Vector3(-12,PlayerTrans.position.y);
        Dragon.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Dragon.SetActive(false);
        yield return null;
    }






    public void Respone(Transform transform)
    {
        transform.localPosition = Check_Pos; 
    }

}
