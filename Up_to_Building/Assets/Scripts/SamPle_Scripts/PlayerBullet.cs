using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBullet : Bullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Destroy(this.gameObject);
            MonsterBase m = collision.gameObject.GetComponent<MonsterBase>();
            m.HP = -1;
        }
    }
}
