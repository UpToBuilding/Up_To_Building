using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;

using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어 위치를 저장하는 정적 변수
    public static Transform PlayerTransform;
    
    // UI 업데이트를 위한 PlayerUI 컴포넌트
    [SerializeField] private PlayerUI playerUI;

    // 컴포넌트 참조 변수들
    public SpriteRenderer sprite; // 스프라이트 렌더러
    public Rigidbody2D rb; // Rigidbody2D
    public BoxCollider2D col; // 박스 콜라이더
    public Animator ani; // 애니메이터

    [SerializeField]
    private GameObject fireball; // 발사체 프리팹

    private int hp; // 플레이어의 체력
    public int HP
    {
        get => hp;
        set
        {
            hp += value;
            playerUI.lostLife(); // 체력 감소 UI 업데이트

            if (hp <= 0)
            {
                ani.SetTrigger("death"); // 사망 애니메이션 재생
            }
        }
    }

    // 플레이어 이동을 위한 변수들
    [SerializeField]
    private float speed; // 이동 속도
    [SerializeField]
    private float jump_power; // 점프 파워

    private bool isjump; // 점프 상태
    public bool Isjump
    {
        get => isjump;
        set
        {
            isjump = value;
            if (isjump) ani.SetBool("jump", true); // 점프 애니메이션 재생
            else ani.SetBool("jump", false); // 점프 애니메이션 종료
        }
    }

    [SerializeField]
    private Vector2 dir; // 이동 방향

    private float x; // 입력 값

    public GameObject Right_pos; // 발사체 오른쪽 위치
    public GameObject Left_pos; // 발사체 왼쪽 위치

    private void Awake()
    {
        hp = 4; // 초기 체력 설정
        jump_power = 12; // 초기 점프 파워 설정

        PlayerTransform = this.transform; // 플레이어 위치 설정

        // 컴포넌트 초기화
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        // 디버그 레이 그리기
        Debug.DrawRay(this.gameObject.transform.position, Vector2.down, Color.red, 1.0f);
        MoveManager(); // 이동 관리
        if (Input.GetKeyDown(KeyCode.V)) { Debug.Log(HP); HP = 1; } // 체력 감소 테스트
        Shooting(); // 발사체 발사
        Jump_Attack(); // 점프 공격
        Debug.DrawRay(transform.position, Vector2.down, new Color(0, 1, 0));
    }

    private void MoveManager()
    {
        // 이동 함수 호출
        Walk();
    }

    private void Walk()
    {
        x = Input.GetAxisRaw("Horizontal"); // 입력 값 받기
        dir = new Vector2(x * speed, rb.velocity.y); // 이동 방향 설정

        
        
            
        
        
        rb.velocity = dir; // Rigidbody 이동 설정
       
        if (x != 0) ani.SetBool("run", true); // 이동 애니메이션 재생
        else ani.SetBool("run", false); // 이동 애니메이션 종료

        if (x > 0){ sprite.flipX = true; 
            
                //if (this.transform.position.x < 10.6)
                  //  GameManager.Instance.Background.transform.Translate(new Vector3(x, 0, 0) * Time.deltaTime*speed);
            }// 오른쪽 이동 시 스프라이트 뒤집기
        else if (x < 0)
        {
            sprite.flipX = false; // 왼쪽 이동 시 스프라이트 뒤집기
                //if (this.transform.position.x > -11)
                  //  GameManager.Instance.Background.transform.Translate(new Vector3(x, 0, 0) * Time.deltaTime*speed);
            }
    }

    private void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ani.SetTrigger("attack"); // 공격 애니메이션 재생
            Bull_Dir(sprite.flipX); // 발사체 방향 설정
        }
    }

    public void Bull_Dir(bool isdir)
    {
        Bullet bulletdir = isdir ? Instantiate(fireball.GetComponent<Bullet>(), Right_pos.transform.position, Quaternion.Euler(0, 0, 0))
                : Instantiate(fireball.GetComponent<Bullet>(), Left_pos.transform.position, Quaternion.Euler(0, 0, 0)); // 발사체 생성
        bulletdir.dir = isdir; // 발사체 방향 설정
    }

    public void Jump_Attack()
    {
        // 점프 상태 확인
        if (rb.velocity.y == 0) Isjump = false; // 땅에 있을 때 점프 상태 해제
        if (Input.GetButtonDown("Jump") && Isjump == false)
        {
            Isjump = true; // 점프 상태 설정
            rb.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse); // 점프
        }

        if (this.rb.velocity.y < 0)
        {
            // 아래로 떨어질 때 몬스터와 충돌 검사
            RaycastHit2D hit = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, 0.5f, LayerMask.GetMask("Monster"));
            if (hit.collider != null)
            {
                rb.AddForce(Vector2.up * 13f, ForceMode2D.Impulse); // 몬스터를 밟았을 때 다시 점프
                isjump = true;
                Destroy(hit.collider.gameObject); // 몬스터 파괴
            }
        }
    }

    public void OnDeath()
    {
        this.gameObject.SetActive(false); // 사망 시 게임 오브젝트 비활성화
    }
}