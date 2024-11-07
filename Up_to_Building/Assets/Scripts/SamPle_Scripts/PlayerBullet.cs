using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBullet : Bullet
{
    private void Start()
    {
        Invoke("outoDestory",2.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster") && collision.GetComponent<MonsterBase>() != null)
        {
            Destroy(this.gameObject);
            MonsterBase m = collision.gameObject.GetComponent<MonsterBase>();
            m.HP = -1;
        } else if (collision.gameObject.CompareTag("Nomal_Obj")||collision.gameObject.CompareTag("Barrier"))
        {
            Destroy(this.gameObject);
        }
    }



    public void outoDestroy()
    {
        Destroy(this.gameObject);
    }

    
}
