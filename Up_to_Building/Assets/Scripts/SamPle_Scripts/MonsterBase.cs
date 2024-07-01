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
   
    
    private void InitInfo()
    {
        hp = 2;
        baseSpeed = 3;
    }

    private void Awake()
    {
        t = 0; // �ʱ� �ð� ����
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
        int x = rb.velocity.x >= 0 ? 1 : -1; // �̵� ���� ����

        // ��������Ʈ ���� ����
        if (x > 0) sp.flipX = false;
        else if (x < 0) sp.flipX = true;

        // �޸��� �ִϸ��̼� ����
        if (x != 0) animator.SetBool("run", true);
        if(rb.velocity.x ==0) animator.SetBool("run", false);

        if (math.abs(Player.PlayerTransform.position.x - this.transform.position.x) > distance)
        {

            t += Time.deltaTime; // �ð� ����
            if (t <= 1.5f)
            {
                rb.velocity = new Vector2(1, rb.velocity.y) * baseSpeed; // ���������� �̵�
            }
            else if (t <= 3.0f) rb.velocity = new Vector2(-1, rb.velocity.y) * baseSpeed; // �������� �̵�
            else t = 0; // �ð� �ʱ�ȭ
        }

    }

    // ���� �޼��� (�߻� �޼���� �ڽ� Ŭ�������� ���� �ʿ�)
    public abstract void Attack();

    
   
    // ���� Ʈ����
    private void OntriggerDeath()
    {
        this.gameObject.SetActive(false); // ��ü ��Ȱ��ȭ
    }

   private void Startdeath()
    {
        rb.velocity = Vector2.zero;
    }


}
