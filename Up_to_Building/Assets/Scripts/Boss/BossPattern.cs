using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FireInformation
{
    
    public BossAttackType attackType;     // ���� Ÿ��
    public float attackDuration; // ���� ���� �ð�
    public float attackSpeed;    // �극�� �ӵ�
    public float timeNextAttack; // �����ϴµ� �ɸ��� �ð�
    public int fireHead;       // �����ϴ� �Ӹ� 0, 1, 2
    public int sectionNumber;  // ���� �ѹ�
    public int sectionLength;  // ���� ����
    public int attackOrder;    // ���� ���� (0���� ������ ���� ������)
}


[CreateAssetMenu(fileName ="New Pattern Data")]
public class BossPattern : ScriptableObject
{
    

    [SerializeField]
    private List<FireInformation> fireInformations = new List<FireInformation>();
    public int patternNumber;
}
