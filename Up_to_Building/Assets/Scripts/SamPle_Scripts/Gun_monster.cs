using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Gun_monster : MonsterBase
{

    public GameObject bullet;
    [SerializeField]
    private Transform Right;
    [SerializeField]
    private Transform Left;
    [SerializeField]
    private bool attack;
    public override void Base_Movement()
    {
        base.Base_Movement();
        if (math.abs(Player.PlayerTransform.position.x - this.transform.position.x) <= distance)
        {
            t = 0;
            sp.flipX = (Player.PlayerTransform.position.x - this.transform.position.x) > 0 ? false : true;

            if (!attack) {
                StartCoroutine("rateShooting");
            }
            
            
        }
    }

    public override void Attack()
    {
        MonsterBullet b = !sp.flipX ? Instantiate(bullet.GetComponent<MonsterBullet>(), this.Right.position, Quaternion.Euler(0, 0, 0)) : Instantiate(bullet.GetComponent<MonsterBullet>(), this.Left.position, Quaternion.Euler(0, 0, 0)); ;
        b.dir = sp.flipX;
    }

    IEnumerator rateShooting()
    {
        Attack();
        attack = true;
        yield return new WaitForSeconds(1.5f);
        attack = false;
        
    }
}
