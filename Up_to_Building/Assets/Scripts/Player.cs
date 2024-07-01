using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;

using UnityEngine;

public class Player : MonoBehaviour
{
    //플레이어 위치를 저장하기 위한 컴포넌트
    public static Transform PlayerTransform;


    //mainUI 연결 컴포넌트
    [SerializeField] private PlayerUI playerUI;
    
    


    //컴포넌트
    public SpriteRenderer sprite; // 플레이어 스프라이트 렌더러
    public Rigidbody2D rb; // 플레이어 Rigidbody2D
    public BoxCollider2D col; // 플레이어 박스 콜라이더
    public Animator ani; // 플레이어 애니메이터

    [SerializeField]
    private GameObject fireball; // 발사체 프리팹

    
   
    private int hp; // 플레이어 체력
    public int HP
    {
        get => hp;
        set
        {
            hp += value;
            playerUI.lostLife();
            if (IsSave)
                this.gameObject.transform.position = GameManager.Instance.SavePoint.transform.position;
            else { this.gameObject.transform.position = GameManager.Instance.initPoint.transform.position; }
            if (hp <= 0)
            {
                
                IsSave = false;
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

  

    [SerializeField]
    private Vector2 dir; // 이동 방향
    public bool issave;
    public bool IsSave
    {
        get;
        set;
    }


    private float x; // 수평 입력 값

    public GameObject Right_pos;
 
    public GameObject Left_pos;

    private void Awake()
    {
        hp = 4; // 초기 체력 설정
        jump_power = 12; // 초기 점프 힘 설정

        PlayerTransform = this.transform;
      


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
        Debug.DrawRay(transform.position, Vector2.down, new Color(0, 1, 0));
    }

    private void MoveManager()
    {
        // 걷기
        Walk();

     

        // 엎드리기
        /*if (Input.GetKeyDown(KeyCode.Z)) IsCrouch = true; // 엎드리는 키 입력 감지
        else if (Input.GetKeyUp(KeyCode.Z)) IsCrouch = false; // 엎드리기 해제 키 입력 감지*/

        // 달리기
        //if (Input.GetKeyDown(KeyCode.LeftShift)) speed = 6f; // 달리기 키 입력 감지
        //else if (Input.GetKeyUp(KeyCode.LeftShift)) speed = 3; // 달리기 해제 키 입력 감지
    }








    private void Walk()
    {
        x = Input.GetAxisRaw("Horizontal"); // 수평 입력 값 감지
        dir = new Vector2(x * speed, rb.velocity.y); // 이동 방향 설정
        rb.velocity = dir; // Rigidbody에 이동 방향 적용

        if (x != 0) ani.SetBool("run", true); // 이동 중일 때 애니메이션 설정
        else ani.SetBool("run", false); // 이동 중이 아닐 때 애니메이션 해제

        if (x > 0) sprite.flipX = true; // 오른쪽 방향으로 이동할 때 스프라이트 방향 설정
        else if (x < 0) sprite.flipX = false; // 왼쪽 방향으로 이동할 때 스프라이트 방향 설정
    }


    private void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ani.SetTrigger("attack");
            Bull_Dir(sprite.flipX);
        }
    }

    public void Bull_Dir(bool isdir)
    {
        
            Bullet bulletdir = isdir ? Instantiate(fireball.GetComponent<Bullet>(), Right_pos.transform.position, Quaternion.Euler(0, 0, 0))
                : Instantiate(fireball.GetComponent<Bullet>(), Left_pos.transform.position, Quaternion.Euler(0, 0, 0));// 발사체 생성;
        bulletdir.dir = isdir;
    }

       public void Jump_Attack()
    {
        // 점프
        if (rb.velocity.y == 0) Isjump = false; // 점프 중이 아닐 때만 점프 가능하도록 설정
        if (Input.GetButton("Jump") && !Isjump)
        {
            Isjump = true;
            rb.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse); // 점프 힘 적용
        }

        if (this.rb.velocity.y < 0)
        {
            
            RaycastHit2D hit = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f,Vector2.down, 0.5f,LayerMask.GetMask("Monster")); // 아래쪽으로 광선 쏘기
            if (hit.collider != null)
            {
                rb.AddForce(Vector2.up * 11f, ForceMode2D.Impulse);
                isjump = true;
               
                Destroy(hit.collider.gameObject);
            }
        }
    }

    public void OnDeath()
    {
        this.gameObject.SetActive(false); // 게임 오브젝트 비활성화
    }


  
    

}
