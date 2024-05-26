using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : Bullet
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
