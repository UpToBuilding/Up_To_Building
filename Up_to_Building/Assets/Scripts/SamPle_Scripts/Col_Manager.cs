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


    //public UnityEvent EffectSystem;

    private void Awake()
    {
        player = GetComponent<Player>(); // �÷��̾� ������Ʈ ��������
    }

    // Ʈ���� �浹 ����
    private void OnTriggerStay2D(Collider2D collision)
    {
        // �浹�� ��ü�� üũ����Ʈ�� ���
        if (collision.gameObject.CompareTag("Stage"))
        {
            GameManager.Instance.UpFloor();
            GameManager.Instance.currentFloor++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Check_Point"))
        {
           // EffectSystem.Invoke();
            player.checkpoint = collision.gameObject;
            
            GameManager.Instance.TempFloor = GameManager.Instance.currentFloor;
        }   
        else if (collision.gameObject.CompareTag(BarrierTag) || (collision.gameObject.CompareTag("Monster")))
        {
            player.HP = -1;

        }
    }


    // �Ϲ� �浹 ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ��ü�� ��ֹ��� ���
        if (collision.collider.gameObject.CompareTag(BarrierTag) || (collision.gameObject.CompareTag("Monster")))
        {
            player.HP = -1;

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