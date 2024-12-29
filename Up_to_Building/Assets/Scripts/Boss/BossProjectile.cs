//using UnityEditor.Animations;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float fireSpeed;
    public GameObject firePrefab;

    private Vector3 TargetLocation;
    private FireInformation fireInformation;
    private float speedMagnification;

    public void Initiailize(Vector3 StartLocation, FireInformation info)
    {
        this.transform.position = StartLocation;
        fireInformation = info;
        TargetLocation = new Vector3(-8.25f + 2.69f * (info.sectionNumber + info.sectionLength - 1), -3.55f, 0f);

        float distStartEnd = Vector3.Distance(StartLocation, TargetLocation);
        speedMagnification = (distStartEnd / 12.0f);

        Vector2 newPos = TargetLocation - StartLocation;
        float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ + 180);

        Animator anim = GetComponent<Animator>();
        anim.SetInteger("ProjectileType", (int)info.attackType);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetLocation, fireSpeed * Time.deltaTime * speedMagnification);
        if(Vector3.Distance(transform.position, TargetLocation) < 0.1f)
        {
            MakeFire(fireInformation);
            Destroy(gameObject);
        }
    }

    void MakeFire(FireInformation fireInfo)
    {
        GameObject fireInstance = Instantiate(firePrefab, transform.position, Quaternion.identity);
        fireInstance.GetComponent<BossFire>().Initialize(
            fireInfo.attackType,
            fireInfo.attackDuration,
            fireInfo.attackSpeed,
            fireInfo.sectionNumber,
            fireInfo.sectionLength
            );
    }
}       
