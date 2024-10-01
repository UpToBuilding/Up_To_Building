using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FireInformation
{
    public BossAttackType attackType;     // ???? ???
    public float attackDuration; // ???? ???? ?��?
    public float attackSpeed;    // ?��?? ???
    public float timeNextAttack; // ???????? ????? ?��?
    public int fireHead;
    public int sectionNumber;  // ???? ???
    public int sectionLength;  // ???? ????
    public int attackOrder;    // ???? ???? (0???? ?????? ???? ??????)
}

[CreateAssetMenu(fileName ="New Pattern Data")]
public class BossPattern : ScriptableObject
{
    [SerializeField]
    public List<FireInformation> fireInformations = new List<FireInformation>();
    public int patternNumber;
    public float patternTime;
    public int numOfOrder;
    public string[] fireHeadString;
}