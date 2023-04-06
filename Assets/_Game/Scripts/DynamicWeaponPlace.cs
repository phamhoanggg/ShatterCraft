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
        if (GameManager.instance.IsState(Enums.GameState.Playing) && IsPlaced)
        {
            weaponPosition.localPosition += Vector3.right * swingSpeed;
            if (Vector3.Distance(weaponPosition.localPosition, posA.localPosition) < 0.1f || Vector3.Distance(weaponPosition.localPosition, posB.localPosition) < 0.1f)
            {
                swingSpeed *= -1;
            }
        }
        
    }
}
