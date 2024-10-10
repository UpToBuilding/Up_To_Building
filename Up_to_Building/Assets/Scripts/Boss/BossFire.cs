using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossAttackType
{
    SingleAttack,
    PersistantAttack,
}

public class BossFire : MonoBehaviour
{
    BossAttackType  bossAttackType;         // ���� Ÿ��
    int             sectionNumber;          // 5���� �������� ���� ��ȣ
    float           attackDuration;         // ���� ���ӽð�
    float           timeAfterCreation;       // ������ �� ���� �ð�

    // �Ʒ� ������ Persistant Attack�� �ش�
    int             sectionSize;            // Persistant Attack������ ���� ����
    float           attackSpeed;            // �ұ� �̵��ϴ� �ӵ�

    [SerializeField]
    Sprite[] Firesprites = new Sprite[2];

    // Fire �ʱ�ȭ
    public void Initialize(BossAttackType _bossAttackType, float _attackDuration, float _attackSpeed, int _sectionNumber, int _sectionSize = 1)
    {
        bossAttackType = _bossAttackType;
        sectionNumber = _sectionNumber;
        sectionSize = _sectionSize;
        attackDuration = _attackDuration;
        attackSpeed = _attackSpeed;

        CreateFire();
    }

    // Fire ����
    void CreateFire()
    {
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 5;

        switch(bossAttackType)
        {
            case BossAttackType.SingleAttack:
                collider.size = new Vector2(2.69f, 3.0f);
                spriteRenderer.color = Color.red;
                spriteRenderer.sprite = Firesprites[(int)BossAttackType.SingleAttack];
                gameObject.transform.position = new Vector3(-8.25f + 2.69f * (sectionNumber + sectionSize - 1), -3.55f, 0f);
                break;

            case BossAttackType.PersistantAttack:
                collider.size = new Vector2(1.35f, 3.0f);
                spriteRenderer.color = Color.magenta;
                spriteRenderer.sprite = Firesprites[(int)BossAttackType.PersistantAttack];
                gameObject.transform.position = new Vector3(-8.25f + 2.69f * (sectionNumber + sectionSize - 1), -3.1f, 0f);
                break;
        }

    }

    void Update()
    {
        // ���ӽð��� ������ �������
        timeAfterCreation += Time.deltaTime;
        if(timeAfterCreation > attackDuration)
        {
            Destroy(gameObject);
        }

        // ���� Ÿ�Կ� ���� ���� ���� �Լ� ����
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
            // Damage Player
        }
    }
}
