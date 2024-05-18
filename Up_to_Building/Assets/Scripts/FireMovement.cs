using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMovement : MonoBehaviour
{
    private Vector3 direction;
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid.velocity = direction * 3;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position += direction * 4 * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        float colPos = col.transform.position.y;
        float colHalfSize = col.transform.localScale.y / 2;
        float firePos = transform.position.y;
        if (!col.gameObject.CompareTag("Player") && colPos - colHalfSize < firePos && colPos + colHalfSize > firePos) // 맵 형태에 따라 세밀한 조정 필요
        {
            destroyFire();
        }
        if (col.gameObject.tag == "Ground")
        {
            rigid.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
        }
    }

    public void setDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    private void destroyFire()
    {
        Destroy(gameObject);
        GameObject.Find("Player").GetComponent<PlayerMovement>().addFire();
    }
}
