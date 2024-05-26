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
        rigid.velocity = direction * 5;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position += direction * 4 * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        /*float colPos = col.transform.position.y;
        float colHalfSize = col.transform.localScale.y / 2;
        float firePos = transform.position.y;
        if (!col.gameObject.CompareTag("Player") && colPos - colHalfSize < firePos && colPos + colHalfSize > firePos) // �� ���¿� ���� ������ ���� �ʿ�
        {
            destroyFire();
        }*/
        destroyFire();
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
