using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  
    [SerializeField]
    private float bulletSpeed;
    
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public bool dir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        ShootBullet(dir);
    }

    private void ShootBullet(bool dir)
    {
        spriteRenderer.flipX = dir;
        if (!dir)
        {         
            rb.AddForce(Vector2.right * bulletSpeed,ForceMode2D.Impulse);
        }
        else  rb.AddForce( Vector2.left * bulletSpeed,ForceMode2D.Impulse);
    }


    private void monsterBulle(bool dir)
    {
        spriteRenderer.flipX = dir;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster")) this.gameObject.SetActive(false);
        else if (collision.gameObject.CompareTag("Nomal_Obj"))
        {
            this.gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }
}
