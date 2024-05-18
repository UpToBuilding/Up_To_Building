using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;

using UnityEngine;

public class Player : MonoBehaviour
{
    //������Ʈ
    public SpriteRenderer sprite; // �÷��̾� ��������Ʈ ������
    public Rigidbody2D rb; // �÷��̾� Rigidbody2D
    public BoxCollider2D col; // �÷��̾� �ڽ� �ݶ��̴�
    public Animator ani; // �÷��̾� �ִϸ�����

    [SerializeField]
    private GameObject fireball; // �߻�ü ������

    //hp
   
    private int hp; // �÷��̾� ü��
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
                ani.SetTrigger("death"); // ��� Ʈ���� ����
            }
        }
    }

    //ĳ���� �̵� ����
    [SerializeField]
    private float speed; // �̵� �ӵ�
    [SerializeField]
    private float jump_power; // ���� ��

    private bool isjump; // ���� ����
    public bool Isjump
    {
        get => isjump;
        set
        {
            isjump = value;
            if (isjump) ani.SetBool("jump", true); // ���� �ִϸ��̼� ����
            else ani.SetBool("jump", false); // ���� �ִϸ��̼� ����
        }
    }

    private bool isCrouch; // ���帲 ����
    public bool IsCrouch
    {
        get => isCrouch;
        set
        {
            isCrouch = value;
            if (isCrouch)
            {
                speed = 1f; // ���帱 �� �̵� �ӵ� ����
                ani.SetBool("Crouch", true); // ���帮�� �ִϸ��̼� ����
            }
            else
            {
                speed = 3f; // �Ͼ �� �̵� �ӵ� ����
                ani.SetBool("Crouch", false); // ���帮�� �ִϸ��̼� ����
            }
        }
    }

    [SerializeField]
    private Vector2 dir; // �̵� ����

    private float x; // ���� �Է� ��

    public GameObject Right_pos;
 
    public GameObject Left_pos;

    private void Awake()
    {
        hp = 5; // �ʱ� ü�� ����
        jump_power = 15; // �ʱ� ���� �� ����

        // ������Ʈ �ʱ�ȭ
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        Debug.DrawRay(this.gameObject.transform.position, Vector2.down, Color.red, 1.0f);
        MoveManager(); // �̵� ����
        if (Input.GetKeyDown(KeyCode.V)) { Debug.Log(HP); HP = 1; } // ü�� ȸ�� Ű �Է� ����
        Shooting(); // �߻�ü �߻�
        Jump_Attack(); // ���� ����
    }

    private void MoveManager()
    {
        // �ȱ�
        Walk();

        // ����
        if (rb.velocity.y == 0) Isjump = false; // ���� ���� �ƴ� ���� ���� �����ϵ��� ����
        if (Input.GetButton("Jump") && !Isjump && !IsCrouch)
        {
            Isjump = true;
            rb.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse); // ���� �� ����
        }

        // ���帮��
        if (Input.GetKeyDown(KeyCode.Z)) IsCrouch = true; // ���帮�� Ű �Է� ����
        else if (Input.GetKeyUp(KeyCode.Z)) IsCrouch = false; // ���帮�� ���� Ű �Է� ����

        // �޸���
        if (Input.GetKeyDown(KeyCode.LeftShift) && !IsCrouch) speed = 6f; // �޸��� Ű �Է� ����
        else if (Input.GetKeyUp(KeyCode.LeftShift)) speed = 3; // �޸��� ���� Ű �Է� ����
    }

    private void Walk()
    {
        x = Input.GetAxisRaw("Horizontal"); // ���� �Է� �� ����
        dir = new Vector2(x * speed, rb.velocity.y); // �̵� ���� ����
        rb.velocity = dir; // Rigidbody�� �̵� ���� ����

        if (x != 0) ani.SetBool("run", true); // �̵� ���� �� �ִϸ��̼� ����
        else ani.SetBool("run", false); // �̵� ���� �ƴ� �� �ִϸ��̼� ����

        if (x > 0) sprite.flipX = false; // ������ �������� �̵��� �� ��������Ʈ ���� ����
        else if (x < 0) sprite.flipX = true; // ���� �������� �̵��� �� ��������Ʈ ���� ����
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
                : Instantiate(fireball.GetComponent<Rigidbody2D>(), Left_pos.transform.position, Quaternion.Euler(0, 0, 0));// �߻�ü ����;

            bull_rb.velocity = !isdir? new Vector2(10,bull_rb.velocity.y) : new Vector2(-10, bull_rb.velocity.y);

        
    }

       public void Jump_Attack()
    {
        if (this.rb.velocity.y < 0)
        {
            
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, 1.0f, LayerMask.GetMask("Monster")); // �Ʒ������� ���� ���
            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<MonsterBase>().HP = -1; // ������ ü�� ����
               // hit.collider.gameObject.GetComponent<BoxCollider2D>().isTrigger = true; // ������ �浹 üũ ����
            }
        }
    }

    public void OnDeath()
    {
        this.gameObject.SetActive(false); // ���� ������Ʈ ��Ȱ��ȭ
    }
}