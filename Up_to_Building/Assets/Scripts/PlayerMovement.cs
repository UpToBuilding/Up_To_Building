using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int hp;
    public int Hp { get { return hp; } }
    [SerializeField] private int speed;
    [SerializeField] private bool isRight;
    [SerializeField] private PlayerUI playerUI;
    private GameObject fire;
    private bool enableJump;
    private bool isDown;
    private int maxFire; // 한번에 발사할 수 있는 최대 공격 횟수
    private Vector3 playerDirection; // 플레이어 이동 방향
    private int currentSpeed;

    private void Awake()
    {
        fire = Resources.Load<GameObject>("Prefabs/Fire");
        enableJump = true;
        isDown = false;
        maxFire = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    public void Movement()
    {
        playerDirection = Vector3.right * (isRight ? 1 : -1);
        currentSpeed = speed / (isDown ? 2 : 1);

        if (playerUI.IsMoveRight || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * currentSpeed * Time.deltaTime;
            isRight = true;
        }
        else if (playerUI.IsMoveLeft || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * currentSpeed * Time.deltaTime;
            isRight = false;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            jump();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            sit();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            stand();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            attack();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            enableJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            enableJump = false;
        }
    }

    public void jump()
    {
        if (enableJump)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * 5;
            enableJump = false;
        }
        
    }

    public void sit()
    {
        transform.Rotate(Vector3.back * 90 * playerDirection.x); // 오른쪽 방향이면 시계방향 왼쪽 방향이면 반시계방향
        transform.position -= Vector3.up * (transform.localScale.y - transform.localScale.x) / 2;
        isDown = true;
    }

    public void stand()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position += Vector3.up * (transform.localScale.y - transform.localScale.x) / 2;
        isDown = false;
    }

    public void attack()
    {
        if (!isDown && maxFire > 0)
        {
            GameObject fireInstance = Instantiate(fire);
            fireInstance.transform.position = transform.position + playerDirection * (transform.localScale.x + fireInstance.transform.localScale.x);
            FireMovement script = fireInstance.GetComponent<FireMovement>();
            script.setDirection(playerDirection);
            maxFire--;
        }
    }

    public void addFire()
    {
        maxFire++;
    }

    public void setEnableJump(bool enable)
    {
        enableJump = enable;
    }

    public void damaged()
    {
        hp--;
        playerUI.lostLife();
    }
}
