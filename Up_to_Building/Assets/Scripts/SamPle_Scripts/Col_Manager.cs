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
        if (collision.gameObject.CompareTag(CheckPoint))
        {
            // 게임 매니저의 체크포인트 설정 변수를 변경하고 위치를 저장
            GameManager.Instance.isResponeCheck = true;
            GameManager.Instance.Check_Pos = new Vector2(this.transform.position.x, -3.2725f);
        }
  

       
    }

    // 일반 충돌 감지
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 객체가 장애물일 경우
        if (collision.collider.gameObject.CompareTag(BarrierTag)|| (collision.gameObject.CompareTag("Monster")))
        {
            // 플레이어의 체력감소
            player.HP = 1;
        }
    }
}