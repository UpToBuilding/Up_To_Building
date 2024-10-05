using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class BossCombat : MonoBehaviour
{       
    [SerializeField]
    private BossPattern[] BossPatternDB;
        
    public GameObject firePrefeb;

    public GameObject fireProjectilePrefab;
        
    private float patternTimer = 0f;
    bool canFire = true;
        
    BossPattern currentPattern;
        
    public void OperatePattern(int patternNum)
    {   
        currentPattern = BossPatternDB[patternNum];

        OperateFireOrder(0);

        StartCoroutine(PatternTimer(currentPattern.patternTime));
    }   
        
    void OperateFireOrder(int orderNum)
    {   
        float fireTime = 0f;
        string patternAnimString = currentPattern.fireHeadString[orderNum] + "Attack";
        gameObject.GetComponent<Animator>().Play(patternAnimString);
        foreach(FireInformation info in currentPattern.fireInformations)
        {
            if(info.attackOrder == orderNum)
            {
                GameObject projectile = Instantiate(fireProjectilePrefab, transform.position, Quaternion.identity);
                Vector3 startLocation = gameObject.GetComponent<Boss>().HeadTransform[info.fireHead].position;
                projectile.GetComponent<BossProjectile>().Initiailize(startLocation, info);
                fireTime = info.timeNextAttack;
            }    
        }

        orderNum++;
        if (orderNum >= currentPattern.numOfOrder) return;

        StartCoroutine(FireTimer(fireTime, orderNum));
    }   
        
    IEnumerator PatternTimer(float patternTime)
    {
        yield return new WaitForSeconds(patternTime);
        gameObject.GetComponent<Boss>().attackEnabled = true;
    }

    IEnumerator FireTimer(float fireTime, int order)
    {
        yield return new WaitForSeconds(fireTime);
        OperateFireOrder(order);
    }


    private void Update()
    {
        patternTimer += Time.deltaTime;
    }
}
