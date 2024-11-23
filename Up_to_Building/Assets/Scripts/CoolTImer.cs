using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class CoolTImer : MonoBehaviour
{
    public Button button;

    [SerializeField]
    private UnityEvent un;
    public float coolTime = 10.0f;
    public bool isClicked = false;
    [SerializeField]
    float leftTime = 10.0f;
    [SerializeField]
    float speed = 5.0f;



    void Update()
    {

        if (isClicked) 
            if (leftTime > 0)
            {
                leftTime -= Time.deltaTime * speed; if (leftTime < 0)
                {
                    leftTime = 0;
                    if (button) button.interactable = true;
                    isClicked = true;
                }
            }
    }


    public void StartCoolTime()
    {
        un.Invoke();
        leftTime = coolTime; 
        isClicked = true;
        if (button) button.interactable=false; 
    }
       
}
