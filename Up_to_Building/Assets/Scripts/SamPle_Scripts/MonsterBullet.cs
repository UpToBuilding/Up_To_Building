using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : Bullet
{
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Nomal_Obj"))
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Nomal_Obj")){ 
            Destroy(this.gameObject);
        }
    }

}
