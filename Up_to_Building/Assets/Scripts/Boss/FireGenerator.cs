using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGenerator : MonoBehaviour
{
    public GameObject firePrefeb;

    [SerializeField]
    float TimeAfter = 0.0f;
    int section = 0;

    void Start()
    {
        GameObject fireInstance = Instantiate(firePrefeb, transform.position, Quaternion.identity);
        fireInstance.GetComponent<BossFire>().Initialize(BossFire.BossAttackType.PersistantAttack, 5f, 1, 3);
    }

    void Update()
    {
        if (section > 4) return;

        TimeAfter += Time.deltaTime;

        if(TimeAfter > 1)
        {
            Debug.Log("Section : " + section + "번 패턴 실행");
            TimeAfter -= 1.0f;
            GameObject fireInstance = Instantiate(firePrefeb, transform.position, Quaternion.identity);
            fireInstance.GetComponent<BossFire>().Initialize(BossFire.BossAttackType.SingleAttack, 0.1f, section);
            section++;
        }
    }

}
