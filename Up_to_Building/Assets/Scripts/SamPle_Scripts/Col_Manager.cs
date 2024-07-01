using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        player = GetComponent<Player>(); // �÷��̾� ������Ʈ ��������
    }

    // Ʈ���� �浹 ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ��ü�� üũ����Ʈ�� ���
        if (collision.gameObject.CompareTag("Stage"))
        {
            GameManager.Instance.MapCount++;
            GameManager.Instance.NextLocation(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Check_Point"))
        {
            player.issave = true;
        }
  

       
    }

    // �Ϲ� �浹 ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ��ü�� ��ֹ��� ���
        if (collision.collider.gameObject.CompareTag(BarrierTag)|| (collision.gameObject.CompareTag("Monster")))
        {
            player.HP = -1;
        }
    }
}