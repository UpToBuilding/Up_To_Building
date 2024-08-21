using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

  /*  public void jump()
    {
        player.jump();
    }

    public void sit()
    {
        player.sit();
    }*/

    /*public void stand()
    {
        player.stand();
    }*/

   /* public void attack()
    {
        player.attack();
    }*/
}
