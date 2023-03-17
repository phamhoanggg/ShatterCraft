using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direct
{
    Left = 0,
    Forward = 90,
    Right = 180,
    Back = 270,
}
public class WeaponPlace : MonoBehaviour
{
    [SerializeField] private Transform weaponPosition;
    [SerializeField] private Animator anim;
    [SerializeField] private Direct direction;
    public Weapon WeaponPlaced { get; private set; }
    public bool IsPlaced { get; private set; }

    private void Start()
    {
        IsPlaced = false;
        weaponPosition.rotation = Quaternion.Euler(0, (int)direction, 0);
    }
    public void SetWeapon(PoolType weaponType)
    {
        if (IsPlaced == false)
        {
            Weapon weapon = SimplePool.Spawn<Weapon>(weaponType);
            weapon.TF.SetParent(weaponPosition);
            weapon.TF.localPosition = Vector3.zero;
            WeaponPlaced = weapon;
            IsPlaced = true;
        } 
    }

    public void DisplayPlaceable()
    {
        anim.ResetTrigger(Cache.ANIM_NORMAL);
        anim.SetTrigger(Cache.ANIM_HIGHLIGHT);
    }

    public void DisplayPlaced()
    {
        anim.ResetTrigger(Cache.ANIM_HIGHLIGHT);
        anim.SetTrigger(Cache.ANIM_NORMAL);
    }
}
