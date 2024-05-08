using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MonsterBase : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sp;
    public GameObject playerObj;
    [SerializeField]
    private int hp;
    public int HP { get { return hp; }
        set { 
            hp += value;
            if (hp <= 0) { 
                animator.SetTrigger("death");
            }
        }
    }

    [SerializeField]
    private float speed;
    [SerializeField]
    private float t;
    public bool isattack;

    private void Awake()
    {
        t = 0;
        isattack = false;
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();       
        animator = GetComponent<Animator>();   
        
    }

    void Update()
    {
        Movement();
    }


    private void Movement()
    {
        int x = rb.velocity.x >= 0 ? 1 : -1;


        if (x > 0) sp.flipX = false;
        else if(x<0)sp.flipX = true;

        if (x != 0) animator.SetBool("run",true);
        else animator.SetBool("run",false);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(x, 0), 5.0f, LayerMask.GetMask("Player"));
        if(hit.collider != null && playerObj == null)
        {
            playerObj = hit.collider.gameObject;
        }
        
        if (playerObj == null) 
        {
            t+= Time.deltaTime;
            if (t <= 1.5f)
            {
                rb.velocity = new Vector2(1, rb.velocity.y) * speed;
            }
            else if (t <= 3.0f) rb.velocity = new Vector2(-1,rb.velocity.y) * speed;
            else t = 0;
        }
        else
        {
            if(!isattack)
            rb.velocity = new Vector2(playerObj.transform.position.x - this.transform.position.x, rb.velocity.y);
        }
    }




    




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("맞음");
            rb.velocity = Vector3.zero;
            HP = -1;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("attack", true);
            Debug.Log("떄림떄리먜림");   
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
            isattack = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("attack", false);
            isattack = false;
            
        }
    }

    private void OntriggerDeath()
    {
        this.gameObject.SetActive(false);
    }
}
