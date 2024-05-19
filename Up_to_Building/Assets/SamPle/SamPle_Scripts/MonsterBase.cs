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
    
    

    public int HP
    {
        get { return hp; }
        set
        {
            hp += value; // ü���� ���ҽ�Ŵ
            if (hp <= 0)
            {
                animator.SetTrigger("death"); // ü���� 0 ���ϰ� �Ǹ� ���� �ִϸ��̼� Ʈ����
            }
        }
    }

 
    [SerializeField]
    private float t; // �ð� ����
    protected bool isattack; // ���� ���� ����
    
    public bool IsAttack
    {
        get => isattack;
        set { 
            isattack = value;
            if(isattack)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void Awake()
    {
        t = 0; // �ʱ� �ð� ����
        IsAttack = false; // �ʱ� ���� ���� ����
        sp = GetComponent<SpriteRenderer>(); // ��������Ʈ ������ ������Ʈ ��������
        rb = GetComponent<Rigidbody2D>(); // ������ٵ� ������Ʈ ��������
        animator = GetComponent<Animator>(); // �ִϸ����� ������Ʈ ��������
    }

    void Update()
    {
        Base_Movement(); // �⺻ �̵� ����
    }

    // ������ �⺻ �̵� ����
    private void Base_Movement()
    {
        int x = rb.velocity.x >= 0 ? 1 : -1; // �̵� ���� ����

        // ��������Ʈ ���� ����
        if (x > 0) sp.flipX = false;
        else if (x < 0) sp.flipX = true;

        // �޸��� �ִϸ��̼� ����
        if (x != 0) animator.SetBool("run", true);
        else animator.SetBool("run", false);


        // �÷��̾ ã�� ������ �� ��Ʈ�� �̵�
        if (math.abs(Player.PlayerTransform.position.x - this.transform.position.x) > 5.0f)
        {
            t += Time.deltaTime; // �ð� ����
            if (t <= 1.5f)
            {
                rb.velocity = new Vector2(1, rb.velocity.y) * baseSpeed; // ���������� �̵�
            }
            else if (t <= 3.0f) rb.velocity = new Vector2(-1, rb.velocity.y) * baseSpeed; // �������� �̵�
            else t = 0; // �ð� �ʱ�ȭ
        }
        else
        {
            t = 0;
            if (!IsAttack)
                Attack(); // ����
        }
    }

    // ���� �޼��� (�߻� �޼���� �ڽ� Ŭ�������� ���� �ʿ�)
    public abstract void Attack();

    // �浹 ó��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            animator.Play("Hit");
            //Rigidbody2D bulletdir = collision.gameObject.GetComponent<Rigidbody2D>();   

            //rb.velocity = new Vector2(bulletdir.velocity.x>0?1:-1,0); // �ӵ� �ʱ�ȭ
            HP = -1; // ü�� ����
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("attack", true);// ���� �ִϸ��̼� ����
            IsAttack = true;
            Debug.Log("���������ٸ�");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("attack", false);// ���� �ִϸ��̼� ����
            IsAttack = false; 
        }
    }



    // ���� Ʈ����
    private void OntriggerDeath()
    {
        this.gameObject.SetActive(false); // ��ü ��Ȱ��ȭ
    }

   


    public void Onisattack()
    {
        IsAttack = false;
    }
}
