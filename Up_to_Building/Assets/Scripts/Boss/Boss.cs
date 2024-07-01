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
    float MaxHp;
    float CurrentHp;
    int nextPatternNum = 1;
    BossPhase bossPhase = BossPhase.NormalPhase;

    void Start()
    {
        CurrentHp = MaxHp;
    }

    void Update()
    {
        switch(bossPhase)
        {
            case BossPhase.NormalPhase:

                break;

            case BossPhase.AngerPhase:

                break;

            case BossPhase.FinalPhase:

                break;
        }
    }

    void BossTakeDamage(float damage)
    {
        CurrentHp -= damage;

        if(CurrentHp < MaxHp * 0.6f)
        {
            bossPhase = BossPhase.AngerPhase;
            nextPatternNum = 3;
        }
        if(CurrentHp < MaxHp * 0.3f)
        {
            bossPhase = BossPhase.FinalPhase;
            nextPatternNum = 6;
        }
        if(CurrentHp <= 0.0f)
        {
            BossSetDead();
        }
    }

    void BossSetDead()
    {

    }
}
