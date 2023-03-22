using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicWeaponPlace : WeaponPlace
{
    [SerializeField] private Transform posA, posB;
    [SerializeField] private float swingSpeed;
    // Update is called once per frame
    void FixedUpdate()
    {
        weaponPosition.position += Vector3.right * swingSpeed;
        if (Vector3.Distance(weaponPosition.position, posA.position) < 0.1f || Vector3.Distance(weaponPosition.position, posB.position) < 0.1f)
        {
            swingSpeed *= -1;
        }
    }
}
