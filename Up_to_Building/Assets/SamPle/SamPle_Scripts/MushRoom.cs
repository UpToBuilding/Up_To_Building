using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MushRoom : MonsterBase
{
    public float overSpeed;


    public override void Attack()
    {
        if (math.abs(Player.PlayerTransform.transform.position.x - this.transform.position.x) != 0)
            rb.velocity = new Vector2(Player.PlayerTransform.transform.position.x - this.transform.position.x, rb.velocity.y) * overSpeed;
        else rb.velocity = Vector2.zero;
    }

 
}
