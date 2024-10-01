using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private Vector2 TargetLocation;

    public void Initiailize(Vector2 StartLocation)
    {
        this.transform.position = StartLocation;
    }

    void Update()
    {
        
    }
}
