using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Playables;

public class Player : MonoBehaviour
{
    // 플레이어 위치를 저장하는 정적 변수
    public static Transform PlayerTransform;

    // UI 업데이트를 위한 PlayerUI 컴포넌트
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private UIManager uiManager;
    

    // 컴포넌트 참조 변수들
    public SpriteRenderer sprite; // 스프라이트 렌더러
    public Rigidbody2D rb; // Rigidbody2D
    public BoxCollider2D col; // 박스 콜라이더
    public Animator ani; // 애니메이터
    public GameObject checkpoint;

    public UnityEvent DeathSystem;


    [SerializeField]
    private GameObject fireball; // 발사체 프리팹

    public Action playerDead;

    private int hp; // 플레이어의 체력
    public int HP
    {
        get => hp;
        set
        {
            hp += value;
            playerUI.lostLife(); // 체력 감소 UI 업데이트
            hitSound.Play();
            playerDead();

            if (hp > 0)
            {
                Invoke("Revive", 1);
            }
            else
            {
                GameManager.Instance.currentFloor = 1;
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

    // 플레이어 사운드
    [SerializeField] private AudioSource jumpSound; // 플레이어 점프 사운드
    [SerializeField] private AudioSource hitSound; // 플레이어 피격 사운드
    [SerializeField] private AudioSource reviveSound; // 플레이어 부활 사운드
    [SerializeField] private AudioSource runSound1; // 플레이어 이동 사운드 -> 1, 2 번갈아가며 runSound에 저장
    [SerializeField] private AudioSource runSound2;

    private AudioSource runSound; // 플래이어 이동 사운드 저장 변수

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

        runSound = runSound2;
    }

    private void Start()
    {
    }

    void Update()
    {
        // 디버그 레이 그리기
        Debug.DrawRay(this.gameObject.transform.position, Vector2.down, Color.red, 1.0f);
        MoveManager(); // 이동 관리
        if (Input.GetKeyDown(KeyCode.V)) { Debug.Log(HP); HP = 1; } // 체력 감소 테스트
        if (Input.GetKey(KeyCode.Space)) Jump(); // 점프
        if (Input.GetKeyDown(KeyCode.Z)) Shooting(); // 발사체 발사
        Jump_Attack(); // 점프 공격
        Debug.DrawRay(transform.position, Vector2.down, new Color(0, 1, 0));
    }

    private void Revive()
    {
        if (checkpoint != null)
        {
            this.transform.position = checkpoint.transform.position;
            GameManager.Instance.currentFloor = GameManager.Instance.TempFloor;
        }
        else
        {
            this.transform.position = GameManager.Instance.initinfo.transform.position;
            GameManager.Instance.currentFloor = 1;
        }
        reviveSound.Play();
    }

    private void MoveManager()
    {
        // 이동 함수 호출
        Walk();
    }

    private void Walk()
    {
        if (playerUI.IsMoveRight) x = 1;
        else if (playerUI.IsMoveLeft) x = -1;
        else x = Input.GetAxisRaw("Horizontal");

        dir = new Vector2(x * speed, rb.velocity.y); // 이동 방향 설정
        
        rb.velocity = dir; // Rigidbody 이동 설정

        if (rb.velocity.x != 0)
        {
            ani.SetBool("run", true); // 이동 애니메이션 재생
            
            if (!runSound.isPlaying && rb.velocity.y == 0)
            {
                if (runSound == runSound1) runSound = runSound2;
                else if (runSound == runSound2) runSound = runSound1;
                runSound.Play();
            }
        }
        else
        {
            ani.SetBool("run", false); // 이동 애니메이션 종료
            if (!runSound.isPlaying) runSound = runSound2;
        }

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

    public void Shooting()
    {
        ani.SetTrigger("attack"); // 공격 애니메이션 재생
        Bull_Dir(sprite.flipX); // 발사체 방향 설정
    }

    public void Bull_Dir(bool isdir)
    {
        Bullet bulletdir = isdir ? Instantiate(fireball.GetComponent<Bullet>(), Right_pos.transform.position, Quaternion.Euler(0, 0, 0))
                : Instantiate(fireball.GetComponent<Bullet>(), Left_pos.transform.position, Quaternion.Euler(0, 0, 0)); // 발사체 생성
        bulletdir.dir = isdir; // 발사체 방향 설정
    }

    public void Jump()
    {
        // 점프 상태 확인
        if (rb.velocity.y == 0) Isjump = false; // 땅에 있을 때 점프 상태 해제
        if (Isjump == false)
        {
            DoJump();
        }
    }

    public void Jump_Attack()
    {
        if (this.rb.velocity.y < 0)
        {
            // 아래로 떨어질 때 몬스터와 충돌 검사
            RaycastHit2D hit = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, 0.5f, LayerMask.GetMask("Monster"));
            if (hit.collider != null)
            {
                DoJump(false);
                hit.collider.gameObject.SetActive(false); // 몬스터 파괴
            }
        }
    }

    public void DoJump(bool playSound = true)
    {
        Isjump = true; // 점프 상태 설정
        rb.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse); // 점프

        if (playSound)
        {
            jumpSound.Play();
        }
    }

    public void OnDeath()
    {
        this.gameObject.SetActive(false); // 사망 시 게임 오브젝트 비활성화
        uiManager.fail();
    }
}