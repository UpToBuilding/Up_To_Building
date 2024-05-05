using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private GameObject fire;
    private bool isGround = true;
    private int isRight = 1; // 1 is right, -1 is left
    private int isDown = 1; // 1 is stand, 2 is down
    private int maxFire = 2; // max fire count

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && isGround)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * 2;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed / isDown * Time.deltaTime;
            isRight = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed / isDown * Time.deltaTime;
            isRight = 1;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.back * 90 * isRight);
            transform.position += Vector3.down * (transform.localScale.y - transform.localScale.x) / 2;
            isDown = 2;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0) ;
            isDown = 1;
        }
        if (Input.GetKeyDown(KeyCode.Z) && maxFire > 0)
        {
            GameObject fireInstance = Instantiate(fire);
            fireInstance.transform.position = transform.position + Vector3.right * (transform.localScale.x + fireInstance.transform.localScale.x) / 2 * isRight;
            FireMovement script = fireInstance.GetComponent<FireMovement>();
            script.setDirection(isRight);
            script.setClone(fireInstance);
            maxFire--;
        } 
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }

    public void addFire()
    {
        maxFire++;
    }
}
