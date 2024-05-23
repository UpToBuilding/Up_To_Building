using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster")) Destroy(this.gameObject);
        else if (collision.gameObject.CompareTag("Nomal_Obj"))
        {
            this.gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }
}
