using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    public float Damage;
    public float ATKSpeed;
    public float ATKRange;
    public Upgrade[] UpgradeList;


    public override void OnInit()
    {

    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public void SizeUp()
    {
        TF.localScale *= 1.1f;
    }

    public void SpeedUp()
    {
        ATKSpeed *= 1.2f;
    }

    public void RangeUp()
    {
        ATKRange *= 1.2f;
    }

}
