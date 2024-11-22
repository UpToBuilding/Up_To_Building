using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

// 추상 클래스 MonsterBase: 몬스터의 기본 동작을 정의
public abstract class MonsterBase : MonoBehaviour
{
    protected Animator animator; // 몬스터의 애니메이터
    protected Rigidbody2D rb; // 몬스터의 리지드바디
    protected SpriteRenderer sp; // 몬스터의 스프라이트 렌더러
    //public GameObject playerObj; // 플레이어 객체를 참조하는 변수
    protected int x;
    [SerializeField]
    protected int hp; // 몬스터의 체력
    [SerializeField]
    private string name;

    [SerializeField]
    protected float baseSpeed;
    [SerializeField]
    protected float distance;

    public int HP
    {
        get { return hp; }
        set
        {
            hp += value; // 체력을 감소시킴
            if (hp > 0) { animator.SetTrigger("hit");
                   
            }
            if (hp <= 0)
            {
                animator.SetTrigger("death"); // 체력이 0 이하가 되면 죽음 애니메이션 트리거
            }
        }
    }

    private Vector2 initPostion;
 
    [SerializeField]
    protected float t; // 시간 변수

    private AudioSource dieSound; // 죽는 사운드
    
    private void InitInfo()
    {
        this.transform.localPosition = initPostion;
        hp = 2;
        baseSpeed = 3;
        
    }

    private void Awake()
    {
        t = 0; // 초기 시간 설정
        Player player = FindObjectOfType<Player>();
        player.playerDead += MonsterObjSetActivation;
        
        sp = GetComponent<SpriteRenderer>(); // 스프라이트 렌더러 컴포넌트 가져오기
        rb = GetComponent<Rigidbody2D>(); // 리지드바디 컴포넌트 가져오기
        animator = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기
        dieSound = GetComponent<AudioSource>(); // 몬스터 죽는 사운드 가져오기
    }

    private void Start()
    {
       initPostion =  new Vector2(this.transform.localPosition.x,this.transform.localPosition.y);
       
        StartCoroutine(activeterm());
          
        
    }

    void Update()
    {
        Base_Movement(); // 기본 이동 동작
    }

    // 몬스터의 기본 이동 로직
    public virtual void Base_Movement()
    {
        x = rb.velocity.x > 0 ? -1 : 1; // 이동 방향 결정





        // 스프라이트 방향 설정
        if (x > 0) sp.flipX = false;
        else if (x < 0) sp.flipX = true;

        // 달리는 애니메이션 설정
        //if (x != 0) animator.SetBool("run", true);
        //if(rb.velocity.x ==0) animator.SetBool("run", false);

        if (Physics2D.OverlapBox(this.gameObject.transform.position, new Vector2(distance, 2.5f), 0.0f, LayerMask.GetMask("Player")) == null)
        {
            if (name == "Gun") animator.SetBool("run", true);
            t += Time.deltaTime; // 시간 증가
            if (t <= 1.0f)
            {
                rb.velocity = new Vector2(1, rb.velocity.y) * baseSpeed; // 오른쪽으로 이동
            }
            else if (t <= 2.0f) rb.velocity = new Vector2(-1, rb.velocity.y) * baseSpeed; // 왼쪽으로 이동
            else t = 0; // 시간 초기화
            //StartCoroutine(FIndPlayer());
        }

    }

    IEnumerator activeterm()
    {

        yield return new WaitForSeconds(0.3f);
        if (this.transform.position.y < Player.PlayerTransform.position.y - 1f)
        {
            this.gameObject.SetActive(false);
        }
    }
    


    // 공격 메서드 (추상 메서드로 자식 클래스에서 구현 필요)
    public abstract void Attack();

    
   
    // 죽음 트리거
    private void OntriggerDeath()
    {
        dieSound.Play();
        this.gameObject.SetActive(false);// 객체 비활성화
    }

   private void Startdeath()
   {
        rb.velocity = Vector2.zero;
   }


    IEnumerator FIndPlayer()
    {
        rb.velocity = new Vector2(1, rb.velocity.y) * baseSpeed;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = new Vector2(-1, rb.velocity.y) * baseSpeed; // 왼쪽으로 이동
        yield return null;
    }

    public void MonsterObjSetActivation()
    {
        if (!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
            InitInfo(); 
        }
    }

}


public class MonsterData
{
    public int hp = 2;
    public Vector3 monsterPostion;

}


