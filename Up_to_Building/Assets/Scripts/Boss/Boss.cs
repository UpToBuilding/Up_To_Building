using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

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
    public float MaxHp = 100f;
    public float CurrentHp;
    int nextPatternNum = 0;
    int nextPatternTemp = 0;
    //BossPhase bossPhase = BossPhase.NormalPhase;
    BossPhase bossPhase;
    public bool attackEnabled =true;
    [SerializeField]
    private bool isBossAlive ;

    private BossCombat combat;
    private SpriteRenderer spriteRenderer;
    private static readonly int TintFloat = Shader.PropertyToID("_TintFloat");

    [SerializeField]
    private UnityEvent deadevent;

    void Start()
    {

        CurrentHp = MaxHp;
        isBossAlive = true;
        bossPhase = BossPhase.NormalPhase;
        combat = GetComponent<BossCombat>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 테스트용 코드. 나중에 없애야 함
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestAttack();
        }

        if (isBossAlive)
        {
            BossAttack();
        }
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
        if (!isBossAlive) return;

        StartCoroutine(WhiteFlash());
        
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
            CurrentHp = 0.0f;
            
            BossSetDead();
        }
    }

    IEnumerator WhiteFlash()
    {
        spriteRenderer.material.SetFloat(TintFloat, 0.2f);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material.SetFloat(TintFloat, 0f);
    }
    
    public void TestAttack()
    {
        BossTakeDamage(10.0f);
    }

    void BossSetDead()
    {
        isBossAlive = false;
        deadevent.Invoke();
        this.gameObject.SetActive(false);
        
    }

    private void OnEnable()
    {
        CurrentHp = MaxHp;
        attackEnabled = true;
        isBossAlive = true;
        bossPhase = BossPhase.NormalPhase;
        
    }

 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BossTakeDamage(1.0f);
        } 
    }
}
