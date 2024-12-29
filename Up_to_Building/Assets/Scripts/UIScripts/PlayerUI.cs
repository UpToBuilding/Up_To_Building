
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField]
    private Image[] lifePoints;

    [SerializeField]
    private int hpIndex;
    
    public bool isattack;



    private bool isMoveLeft;
    public bool IsMoveLeft {  get { return isMoveLeft; } }
    private bool isMoveRight;
    public bool IsMoveRight { get {  return isMoveRight; } }

    private void Awake()
    {
        isattack = true;
        hpIndex = lifePoints.Length-1;
    }

    private void Update()
    {
        
    }

    public void lostLife()
    {
        if (hpIndex < 0) return;
        lifePoints[hpIndex--].gameObject.SetActive(false);
    }

    public void HealHp()
    {
        hpIndex = lifePoints.Length - 1;
        foreach (Image img in lifePoints)
        {
            img.gameObject.SetActive(true);
        }
        player.checkpoint = null;
        player.ResetData();
        player.gameObject.SetActive(true);
        player.Revive();
    }

    public void CheckHealup()
    {
        hpIndex = lifePoints.Length - 1;
        foreach (Image img in lifePoints)
        {
            img.gameObject.SetActive(true);
        }
    }

    public void Bossreset()
    {
        hpIndex = lifePoints.Length - 1;
        foreach (Image img in lifePoints)
        {
            img.gameObject.SetActive(true);
        }
    }

    public void moveLeft()
    {
        isMoveLeft = true;
    }

    public void stopLeft()
    {
        isMoveLeft = false;
    }

    public void moveRight()
    {
        isMoveRight = true;
    }

    public void stopRight()
    {
        isMoveRight = false;
    }

    public void jump()
    {
        player.Jump();
    }

    public void attack()
    {
        
        if(isattack)
            player.Shooting();
    }

    public void Clicked()
    {
        ChangeImg(1);
    }

    public void Unclick()
    {
        ChangeImg(2);
    }

    private void ChangeImg(int type) // 1 = on -> off, 2 = off -> on
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        string name = clickObject.name;
        clickObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI Sources/Hud/" + name.Substring(0, -6) + type);
    }
}
