using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    enum BossPhase
    {
        NormalPhase,
        AngerPhase,
        FinalPhase,
    }

    [SerializeField]
    public Transform[] HeadTransform;

    [SerializeField]
    float MaxHp = 100f;
    float CurrentHp;
    int nextPatternNum = 0;
    int nextPatternTemp = 0;
    BossPhase bossPhase = BossPhase.NormalPhase;
    public bool attackEnabled = true;


    BossCombat combat;

    void Start()
    {
        CurrentHp = MaxHp;
        combat = gameObject.GetComponent<BossCombat>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestAttack();
        }

        BossAttack();
    }


    void BossAttack()
    {
        if (!attackEnabled) return;

        switch (bossPhase)
        {
            case BossPhase.NormalPhase:
                attackEnabled = false;
                combat.OperatePattern(nextPatternNum);
                nextPatternNum = 0 + (nextPatternTemp++) % 3;
                break;

            case BossPhase.AngerPhase:
                attackEnabled = false;
                combat.OperatePattern(nextPatternNum);
                nextPatternNum = 3 + (nextPatternTemp++) % 3;
                break;

            case BossPhase.FinalPhase:
                attackEnabled = false;
                combat.OperatePattern(nextPatternNum);
                nextPatternNum = 3 + (nextPatternTemp++) % 5;
                break;
        }
        Debug.Log(nextPatternNum);
    }
    void BossTakeDamage(float damage)
    {
        CurrentHp -= damage;

        if(CurrentHp < MaxHp * 0.65f)
        {
            bossPhase = BossPhase.AngerPhase;
        }
        if(CurrentHp < MaxHp * 0.31f)
        {
            bossPhase = BossPhase.FinalPhase;
        }
        if(CurrentHp <= 0.0f)
        {
            BossSetDead();
        }
    }

    public void TestAttack()
    {
        BossTakeDamage(10.0f);
    }

    void BossSetDead()
    {
        // Game Clear?
    }
}
