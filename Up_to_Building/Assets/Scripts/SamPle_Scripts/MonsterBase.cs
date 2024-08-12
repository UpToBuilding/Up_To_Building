using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

// �߻� Ŭ���� MonsterBase: ������ �⺻ ������ ����
public abstract class MonsterBase : MonoBehaviour
{
    protected Animator animator; // ������ �ִϸ�����
    protected Rigidbody2D rb; // ������ ������ٵ�
    protected SpriteRenderer sp; // ������ ��������Ʈ ������
    //public GameObject playerObj; // �÷��̾� ��ü�� �����ϴ� ����
    [SerializeField]
    protected int hp; // ������ ü��

    [SerializeField]
    protected float baseSpeed;
    [SerializeField]
    protected float distance;

    public int HP
    {
        get { return hp; }
        set
        {
            hp += value; // ü���� ���ҽ�Ŵ
            if (hp > 0) animator.SetTrigger("hit"); 
            if (hp <= 0)
            {
                animator.SetTrigger("death"); // ü���� 0 ���ϰ� �Ǹ� ���� �ִϸ��̼� Ʈ����
            }
        }
    }

 
    [SerializeField]
    protected float t; // �ð� ����

    private bool isStop;
    
    private void InitInfo()
    {
        hp = 2;
        baseSpeed = 3;
        
    }

    private void Awake()
    {
        t = 0; // �ʱ� �ð� ����
        isStop = false;
        InitInfo();
        sp = GetComponent<SpriteRenderer>(); // ��������Ʈ ������ ������Ʈ ��������
        rb = GetComponent<Rigidbody2D>(); // ������ٵ� ������Ʈ ��������
        animator = GetComponent<Animator>(); // �ִϸ����� ������Ʈ ��������
    }

    void Update()
    {
        Base_Movement(); // �⺻ �̵� ����
    }

    // ������ �⺻ �̵� ����
    public virtual void Base_Movement()
    {
        int x = rb.velocity.x > 0 ? -1 : 1; // �̵� ���� ����





        // ��������Ʈ ���� ����
        if (x > 0) sp.flipX = false;
        else if (x < 0) sp.flipX = true;

        // �޸��� �ִϸ��̼� ����
        if (x != 0) animator.SetBool("run", true);
        if(rb.velocity.x ==0) animator.SetBool("run", false);

        if (Physics2D.OverlapBox(this.gameObject.transform.position, new Vector2(distance, 2.5f), 0.0f, LayerMask.GetMask("Player")) == null&&!isStop)
        {

            t += Time.deltaTime; // �ð� ����
            if (t <= 1.0f)
            {
                rb.velocity = new Vector2(1, rb.velocity.y) * baseSpeed; // ���������� �̵�
            }
            else if (t <= 2.0f) rb.velocity = new Vector2(-1, rb.velocity.y) * baseSpeed; // �������� �̵�
            else t = 0; // �ð� �ʱ�ȭ
            //StartCoroutine(FIndPlayer());
        }

    }

    


    // ���� �޼��� (�߻� �޼���� �ڽ� Ŭ�������� ���� �ʿ�)
    public abstract void Attack();

    
   
    // ���� Ʈ����
    private void OntriggerDeath()
    {
        Destroy(this.gameObject); // ��ü ��Ȱ��ȭ
    }

   private void Startdeath()
    {
        rb.velocity = Vector2.zero;
    }


    IEnumerator FIndPlayer()
    {
        rb.velocity = new Vector2(1, rb.velocity.y) * baseSpeed;
        yield return new WaitForSeconds(1.0f);
        rb.velocity = new Vector2(-1, rb.velocity.y) * baseSpeed; // �������� �̵�
        yield return null;
    }

    public void StopEvent()
    {
        baseSpeed = 0;
        isStop = true;
    }

    public void RestartEvent()
    {
        baseSpeed = 3;
        isStop = false;
    }
}
