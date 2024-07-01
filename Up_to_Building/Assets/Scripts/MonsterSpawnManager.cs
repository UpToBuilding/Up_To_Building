using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    public GameObject monster;
    public Transform spanwerLocation;
    [SerializeField]
    private List<MonsterBase> monsterBases;


    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            MonsterBase m= Instantiate(monster).GetComponent<MonsterBase>();
            m.gameObject.SetActive(false);
            monsterBases.Add(m);
        }

        Invoke("GetPool", 5f);
    }

    public void CreatorMonster(int maxSize)
    {
        for (int i = 0; i < maxSize; i++)
        {
            
        }
    }

    public void GetPool()
    {
        foreach (var item in monsterBases)
        {
            //item.transform.position = new Vector3(Random.Range(-5, 20), spanwerLocation.position.y, 0);
            item.gameObject.SetActive(true);
        }
    }
}
