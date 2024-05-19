using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun_monster : MonsterBase
{

    public GameObject bullet;
    [SerializeField]
    private Transform Right;
    [SerializeField]
    private Transform Left;
   
    public override void Attack()
    {
        rb.velocity = Vector2.zero;
        if(Player.PlayerTransform.position.x - rb.velocity.x < 0 )
            sp.flipX = true;
        Bullet b = !sp.flipX?Instantiate(bullet.GetComponent<Bullet>(), this.Right.position, Quaternion.Euler(0, 0, 0)): Instantiate(bullet.GetComponent<Bullet>(), this.Left.position, Quaternion.Euler(0, 0, 0)); ;
        b.dir = sp.flipX; 
        StartCoroutine("rateShooting");
    }

    IEnumerator rateShooting()
    {
        
        yield return new WaitForSeconds(1.0f);
    }
}
