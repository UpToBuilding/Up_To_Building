using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MushRoom : MonsterBase
{
    public float overSpeed;
    private bool isCol;
    private float temp;

    public override void Attack()
    {
        sp.flipX = (Player.PlayerTransform.position.x - this.transform.position.x) > 0 ? true : false;
        if (math.abs(Player.PlayerTransform.transform.position.x - this.transform.position.x) != 0 && !isCol)
            rb.velocity = new Vector2(Player.PlayerTransform.transform.position.x - this.transform.position.x, rb.velocity.y) * overSpeed;
        else rb.velocity = Vector2.zero;
    }

    public override void Base_Movement()
    {
        base.Base_Movement();
        // 플레이어를 찾지 못했을 때 패트롤 이동

        if (Physics2D.OverlapBox(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.5f), new Vector2(distance, 2f ), 0.0f, LayerMask.GetMask("Player")) != null)
        {
            t = 0;
            rb.velocity = Vector2.zero;
            Attack();

        }
    }

    public void Initinfo()
    {
        temp = overSpeed;
        overSpeed = 0;
    }
    public void reinfo()
    {
        overSpeed = temp;
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
