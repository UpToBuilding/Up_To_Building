using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBullet : Bullet
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Nomal_Obj")||collision.gameObject.CompareTag("Monster"))
        {
            Destroy(this.gameObject);
            collision.gameObject.SetActive(false);
        }
    }





}
