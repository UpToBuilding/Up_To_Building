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
    

    private bool isMoveLeft;
    public bool IsMoveLeft {  get { return isMoveLeft; } }
    private bool isMoveRight;
    public bool IsMoveRight { get {  return isMoveRight; } }

    private void Awake()
    {
        hpIndex = lifePoints.Length-1;
    }

    private void Update()
    {
        
    }

    public void lostLife()
    {
        lifePoints[hpIndex--].gameObject.SetActive(false);
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
