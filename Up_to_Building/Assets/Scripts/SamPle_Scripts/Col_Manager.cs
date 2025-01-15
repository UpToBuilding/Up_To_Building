using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Col_Manager : MonoBehaviour
{
    [SerializeField]
    private string BarrierTag; // ��ֹ� �±�
    [SerializeField]
    private string Nomal; // �Ϲ� �±�
    [SerializeField]
    private string CheckPoint; // üũ����Ʈ �±�
    [SerializeField]
    private Player player; // �÷��̾� ��ü
    [SerializeField] private UIManager uiManager;


    //public UnityEvent EffectSystem;

    private void Awake()
    {
        player = GetComponent<Player>(); // �÷��̾� ������Ʈ ��������
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Check_Point"))
        {
            // EffectSystem.Invoke();
            GameObject checkPoint = collision.gameObject;
            if (player.checkpoint != checkPoint)
            {

                player.initHp();
                player.AttackEvent.Invoke();
                player.checkpoint = checkPoint;
                checkPoint.GetComponent<CheckPointEffect>().PlayCheckPointSound();
                GameManager.Instance.process++;
                uiManager.SetStageText();
            }

            GameManager.Instance.TempFloor = GameManager.Instance.currentFloor;
        }   
        else if (collision.gameObject.CompareTag(BarrierTag) || (collision.gameObject.CompareTag("Monster")))
        {
            if (player.state == Player.State.NORMAL)
                player.HP -= 1;

        }
        else if (collision.gameObject.CompareTag("Stage"))
        {
                player.playerUI.isattack = false;
                GameManager.Instance.UpFloor();
                player.PlayerUpFloor();
           
            //GameManager.Instance.currentFloor++;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stage"))
        {
            player.StartInvinciblity();
            player.playerUI.isattack = true;
        }
    }


    // �Ϲ� �浹 ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ��ü�� ��ֹ��� ���
        if (collision.collider.gameObject.CompareTag(BarrierTag) || (collision.gameObject.CompareTag("Monster")))
        {
            if(player.state == Player.State.NORMAL)
            player.HP -= 1;
            

        }else if (collision.collider.gameObject.CompareTag("Ground"))
        {
            player.Isjump = false;
        }
    }
    


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Nomal_Obj"))
        {
            
            player.Isjump = true;
        }
    }
}