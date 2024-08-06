using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField]
    private Transform[] lifePoints;
    private bool isMoveLeft;
    public bool IsMoveLeft {  get { return isMoveLeft; } }
    private bool isMoveRight;
    public bool IsMoveRight { get {  return isMoveRight; } }

    private void Awake()
    {
        //lifePoints = GameObject.Find("LifePanel").GetComponentsInChildren<Transform>()[1..]; // 자기 자신은 제외
    }

    private void Update()
    {
        
    }

    public void lostLife()
    {
        lifePoints[player.HP].gameObject.SetActive(false);
        if (player.HP == 0)
        {
            Debug.Log("GameOver!");
            Time.timeScale = 0;
            return;
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
