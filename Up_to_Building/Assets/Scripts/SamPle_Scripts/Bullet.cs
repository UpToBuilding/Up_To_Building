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
        Invoke("OndestroyThis", 3f);
    }

    public virtual void ShootBullet(bool dir)
    {
        spriteRenderer.flipX = dir;
        if (dir)
        {         
            rb.AddForce(Vector2.right * bulletSpeed,ForceMode2D.Impulse);
        }
        else  rb.AddForce( Vector2.left * bulletSpeed,ForceMode2D.Impulse);
    }

    public void OndestroyThis()
    {
        Destroy(this.gameObject);
    }
    


}
