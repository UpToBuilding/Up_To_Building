using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  
    [SerializeField]
    private float bulletSpeed;
    
    protected SpriteRenderer spriteRenderer;
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

    public virtual void ShootBullet(bool dir)
    {
        spriteRenderer.flipX = dir;
        if (!dir)
        {         
            rb.AddForce(Vector2.right * bulletSpeed*Time.deltaTime,ForceMode2D.Impulse);
        }
        else  rb.AddForce( Vector2.left * bulletSpeed*Time.deltaTime,ForceMode2D.Impulse);
    }




}
