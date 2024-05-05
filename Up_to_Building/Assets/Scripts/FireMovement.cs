using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMovement : MonoBehaviour
{
    private int direction;
    private GameObject clone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * 4 * Time.deltaTime * direction;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        float yPos = col.transform.position.y;
        float yHalfSize = col.transform.localScale.y / 2;
        float firPos = transform.position.y;
        if (yPos - yHalfSize < firPos && yPos + yHalfSize > firPos && col.gameObject.name != "Player")
        {
            destroyFire();
        }
        if (col.gameObject.tag == "Ground")
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3, ForceMode2D.Impulse);
        }
    }

    public void setDirection(int direction)
    {
        this.direction = direction;
    }

    public void setClone(GameObject clone)
    {
        this.clone = clone;
    }

    private void destroyFire()
    {
        Destroy(clone);
        GameObject.Find("Player").GetComponent<PlayerMovement>().addFire();
    }
}
