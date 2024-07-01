using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FireInformation
{
    
    public BossAttackType attackType;     // 공격 타입
    public float attackDuration; // 공격 지속 시간
    public float attackSpeed;    // 브레스 속도
    public float timeNextAttack; // 공격하는데 걸리는 시간
    public int fireHead;       // 공격하는 머리 0, 1, 2
    public int sectionNumber;  // 섹션 넘버
    public int sectionLength;  // 섹션 길이
    public int attackOrder;    // 공격 순서 (0번의 공격이 가장 빠르다)
}


[CreateAssetMenu(fileName ="New Pattern Data")]
public class BossPattern : ScriptableObject
{
    

    [SerializeField]
    private List<FireInformation> fireInformations = new List<FireInformation>();
    public int patternNumber;
}
