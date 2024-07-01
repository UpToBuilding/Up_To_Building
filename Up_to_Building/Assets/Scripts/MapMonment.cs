using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMonment : MonoBehaviour
{
    public Transform cam;

    private float thistrans;
    private void Awake()
    {
        thistrans = transform.position.y; 
    }
    void Update()
    {
        transform.position = new Vector3(cam.position.x,thistrans,1);
    }
}
