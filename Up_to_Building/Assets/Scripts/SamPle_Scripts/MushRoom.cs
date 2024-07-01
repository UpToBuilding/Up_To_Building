using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MushRoom : MonsterBase
{
    public float overSpeed;
    private bool isCol;

    public override void Attack()
    {
        sp.flipX = (Player.PlayerTransform.position.x - this.transform.position.x) > 0 ? false : true;
        if (math.abs(Player.PlayerTransform.transform.position.x - this.transform.position.x) != 0 && !isCol)
            rb.velocity = new Vector2(Player.PlayerTransform.transform.position.x - this.transform.position.x, rb.velocity.y) * overSpeed;
        else rb.velocity = Vector2.zero;
    }

    public override void Base_Movement()
    {
        base.Base_Movement();
        // 플레이어를 찾지 못했을 때 패트롤 이동

        if(math.abs(Player.PlayerTransform.position.x - this.transform.position.x) <= distance)
        {
            t = 0;
            rb.velocity = Vector2.zero;
            Attack();

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            animator.Play("Hit");
            HP = -1;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            isCol = true; 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCol = false;
        }
    }

}
