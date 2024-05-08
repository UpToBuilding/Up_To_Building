using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;

using UnityEngine;

public class Player : MonoBehaviour
{
    //컴포넌트
    public SpriteRenderer sprite; // 플레이어 스프라이트 렌더러
    public Rigidbody2D rb; // 플레이어 Rigidbody2D
    public BoxCollider2D col; // 플레이어 박스 콜라이더
    public Animator ani; // 플레이어 애니메이터

    [SerializeField]
    private GameObject fireball; // 발사체 프리팹

    //hp
   
    private int hp; // 플레이어 체력
    public int HP
    {
        get => hp;
        set
        {
            hp -= value;
            if(GameManager.Instance.isResponeCheck)
            GameManager.Instance.Respone(this.transform);
            if (hp <= 0)
            {
                ani.SetTrigger("death"); // 사망 트리거 설정
            }
        }
    }

    //캐릭터 이동 변수
    [SerializeField]
    private float speed; // 이동 속도
    [SerializeField]
    private float jump_power; // 점프 힘

    private bool isjump; // 점프 상태
    public bool Isjump
    {
        get => isjump;
        set
        {
            isjump = value;
            if (isjump) ani.SetBool("jump", true); // 점프 애니메이션 설정
            else ani.SetBool("jump", false); // 점프 애니메이션 해제
        }
    }

    private bool isCrouch; // 엎드림 상태
    public bool IsCrouch
    {
        get => isCrouch;
        set
        {
            isCrouch = value;
            if (isCrouch)
            {
                speed = 1f; // 엎드릴 때 이동 속도 감소
                ani.SetBool("Crouch", true); // 엎드리는 애니메이션 설정
            }
            else
            {
                speed = 3f; // 일어설 때 이동 속도 증가
                ani.SetBool("Crouch", false); // 엎드리는 애니메이션 해제
            }
        }
    }

    [SerializeField]
    private Vector2 dir; // 이동 방향

    private float x; // 수평 입력 값

    public GameObject Right_pos;
 
    public GameObject Left_pos;

    private void Awake()
    {
        hp = 5; // 초기 체력 설정
        jump_power = 15; // 초기 점프 힘 설정

        // 컴포넌트 초기화
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        Debug.DrawRay(this.gameObject.transform.position, Vector2.down, Color.red, 1.0f);
        MoveManager(); // 이동 관리
        if (Input.GetKeyDown(KeyCode.V)) { Debug.Log(HP); HP = 1; } // 체력 회복 키 입력 감지
        Shooting(); // 발사체 발사
        Jump_Attack(); // 점프 공격
    }

    private void MoveManager()
    {
        // 걷기
        Walk();

        // 점프
        if (rb.velocity.y == 0) Isjump = false; // 점프 중이 아닐 때만 점프 가능하도록 설정
        if (Input.GetButton("Jump") && !Isjump && !IsCrouch)
        {
            Isjump = true;
            rb.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse); // 점프 힘 적용
        }

        // 엎드리기
        if (Input.GetKeyDown(KeyCode.Z)) IsCrouch = true; // 엎드리는 키 입력 감지
        else if (Input.GetKeyUp(KeyCode.Z)) IsCrouch = false; // 엎드리기 해제 키 입력 감지

        // 달리기
        if (Input.GetKeyDown(KeyCode.LeftShift) && !IsCrouch) speed = 6f; // 달리기 키 입력 감지
        else if (Input.GetKeyUp(KeyCode.LeftShift)) speed = 3; // 달리기 해제 키 입력 감지
    }

    private void Walk()
    {
        x = Input.GetAxisRaw("Horizontal"); // 수평 입력 값 감지
        dir = new Vector2(x * speed, rb.velocity.y); // 이동 방향 설정
        rb.velocity = dir; // Rigidbody에 이동 방향 적용

        if (x != 0) ani.SetBool("run", true); // 이동 중일 때 애니메이션 설정
        else ani.SetBool("run", false); // 이동 중이 아닐 때 애니메이션 해제

        if (x > 0) sprite.flipX = false; // 오른쪽 방향으로 이동할 때 스프라이트 방향 설정
        else if (x < 0) sprite.flipX = true; // 왼쪽 방향으로 이동할 때 스프라이트 방향 설정
    }

    private void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.K)&&!isCrouch)
        {
            Bull_Dir(sprite.flipX);
        }
    }

    public void Bull_Dir(bool isdir)
    {
        
            Rigidbody2D bull_rb = !isdir ? Instantiate(fireball.GetComponent<Rigidbody2D>(), Right_pos.transform.position, Quaternion.Euler(0, 0, 0))
                : Instantiate(fireball.GetComponent<Rigidbody2D>(), Left_pos.transform.position, Quaternion.Euler(0, 0, 0));// 발사체 생성;

            bull_rb.velocity = !isdir? new Vector2(10,bull_rb.velocity.y) : new Vector2(-10, bull_rb.velocity.y);

        
    }

       public void Jump_Attack()
    {
        if (this.rb.velocity.y < 0)
        {
            
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, 1.0f, LayerMask.GetMask("Monster")); // 아래쪽으로 광선 쏘기
            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<MonsterBase>().HP = -1; // 몬스터의 체력 감소
               // hit.collider.gameObject.GetComponent<BoxCollider2D>().isTrigger = true; // 몬스터의 충돌 체크 해제
            }
        }
    }

    public void OnDeath()
    {
        this.gameObject.SetActive(false); // 게임 오브젝트 비활성화
    }
}
