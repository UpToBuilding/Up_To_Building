using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Col_Manager : MonoBehaviour
{
    [SerializeField]
    private string BarrierTag; // 장애물 태그
    [SerializeField]
    private string Nomal; // 일반 태그
    [SerializeField]
    private string CheckPoint; // 체크포인트 태그
    [SerializeField]
    private Player player; // 플레이어 객체

    private void Awake()
    {
        player = GetComponent<Player>(); // 플레이어 컴포넌트 가져오기
    }

    // 트리거 충돌 감지
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체가 체크포인트일 경우
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

    // 일반 충돌 감지
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 객체가 장애물일 경우
        if (collision.collider.gameObject.CompareTag(BarrierTag)|| (collision.gameObject.CompareTag("Monster")))
        {
            player.HP = -1;
        }
    }
}