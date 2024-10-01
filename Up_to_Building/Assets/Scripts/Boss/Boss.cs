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
        if (!attackEnabled) return;

        switch(bossPhase)
        {
            case BossPhase.NormalPhase:
                attackEnabled = false;
                combat.OperatePattern(nextPatternNum);
                nextPatternNum = 0 + (nextPatternNum + 1) % 3;
                break;

            case BossPhase.AngerPhase:
                attackEnabled = false;
                combat.OperatePattern(nextPatternNum);
                nextPatternNum = 3 + (nextPatternNum + 1) % 3;
                break;

            case BossPhase.FinalPhase:
                attackEnabled = false;
                combat.OperatePattern(nextPatternNum);
                nextPatternNum = 3 + (nextPatternNum + 1) % 5;
                break;
        }
    }

    void BossTakeDamage(float damage)
    {
        CurrentHp -= damage;

        if(CurrentHp < MaxHp * 0.6f)
        {
            bossPhase = BossPhase.AngerPhase;
            nextPatternNum = 0;
        }
        if(CurrentHp < MaxHp * 0.3f)
        {
            bossPhase = BossPhase.FinalPhase;
        }
        if(CurrentHp <= 0.0f)
        {
            BossSetDead();
        }
    }

    void BossSetDead()
    {
        // Game Clear?
    }
}
