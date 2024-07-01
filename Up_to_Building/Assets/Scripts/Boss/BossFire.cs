using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BossFire : MonoBehaviour
{
    public enum BossAttackType
    {
        SingleAttack,
        PersistantAttack,
    }

    BossAttackType  bossAttackType;         // 공격 타입
    int             sectionNumber;          // 5개로 나누어진 구역 번호
    float           attackDuration;         // 패턴 지속시간
    float           timeAfterCreation;       // 생성된 후 지난 시간

    // 아래 변수는 Persistant Attack만 해당
    int             sectionSize;            // Persistant Attack에서의 공격 범위
    float           attackSpeed;            // 불길 이동하는 속도

    [SerializeField]
    Sprite[] Firesprites = new Sprite[2];

    // Fire 초기화
    public void Initialize(BossAttackType _bossAttackType, float _attackDuration, int _sectionNumber, int _sectionSize = 1)
    {
        bossAttackType = _bossAttackType;
        sectionNumber = _sectionNumber;
        sectionSize = _sectionSize;
        attackDuration = _attackDuration;

        CreateFire();
    }

    // Fire 생성
    void CreateFire()
    {
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 1;

        switch(bossAttackType)
        {
            case BossAttackType.SingleAttack:
                collider.size = new Vector2(2.69f, 3.0f);
                spriteRenderer.color = Color.red;
                spriteRenderer.sprite = Firesprites[(int)BossAttackType.SingleAttack];
                break;

            case BossAttackType.PersistantAttack:
                collider.size = new Vector2(1.35f, 3.0f);
                spriteRenderer.color = Color.magenta;
                spriteRenderer.sprite = Firesprites[(int)BossAttackType.PersistantAttack];
                attackSpeed = -3.0f;
                break;
        }

        gameObject.transform.position = new Vector3(-8.25f + 2.69f * (sectionNumber + sectionSize -1), -0.9f, 0f);
    }

    void Update()
    {
        // 지속시간이 지나면 사라진다
        timeAfterCreation += Time.deltaTime;
        if(timeAfterCreation > attackDuration)
        {
            Destroy(gameObject);
        }

        // 공격 타입에 대해 각각 독립 함수 실행
        switch(bossAttackType)
        {
            case BossAttackType.SingleAttack:
                SingleAttack();
                break;

            case BossAttackType.PersistantAttack:
                PersistantAttack();
                break;
        }
    }

    void SingleAttack()
    {

    }

    void PersistantAttack()
    {
        Vector3 CurrentVector = transform.position;
        Vector3 NextVector = new Vector3(CurrentVector.x + attackSpeed * Time.deltaTime, CurrentVector.y, 0f);
        
        float minPos = -9.6f + 2.69f * (sectionNumber) + 0.675f;
        float maxPos = -9.6f + 2.69f * (sectionNumber + sectionSize) - 0.675f;
        
        if(NextVector.x < minPos)
        {
            attackSpeed = Mathf.Abs(attackSpeed);
        }
        else if(NextVector.x > maxPos)
        {
            attackSpeed = Mathf.Abs(attackSpeed) * (-1);
        }

        transform.position = NextVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        Player player = collisionObject.GetComponent<Player>();

        if(player != null)
        {
            // 캐릭터 데미지 받는 함수 호출한다
        }
    }
}
