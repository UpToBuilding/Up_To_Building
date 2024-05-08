using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float bulletSpeed;




    private void Awake()
    {
        //DIR = 1;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster")) this.gameObject.SetActive(false);
        else if (collision.gameObject.CompareTag("Nomal_Obj"))
        {
            this.gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }
}
