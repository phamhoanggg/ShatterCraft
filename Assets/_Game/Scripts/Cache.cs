using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    #region Cache String
    public const string ANIM_DEAD = "die";
    public const string ANIM_HIGHLIGHT = "highlight";
    public const string ANIM_NORMAL = "normal";

    public const string TAG_WEAPON = "Weapon";
    public const string TAG_DEATHZONE = "DeathZone";
    public const string TAG_ENEMY = "Enemy";

    #endregion

    #region Cache GetComponent
    private static Dictionary<Collider, WeaponPlace> weaponPlaces = new Dictionary<Collider, WeaponPlace>();
    private static Dictionary<Collider, Zombie> zombies = new Dictionary<Collider, Zombie>();
    private static Dictionary<GameObject, Upgrade> upgrades = new Dictionary<GameObject, Upgrade>();

    public static WeaponPlace GetWeaponPlace(Collider collider)
    {
        if (!weaponPlaces.ContainsKey(collider))
        {
            weaponPlaces.Add(collider, collider.GetComponent<WeaponPlace>());
        }

        return weaponPlaces[collider];
    }

    public static Zombie GetZombie(Collider collider)
    {
        if (!zombies.ContainsKey(collider))
        {
            zombies.Add(collider, collider.GetComponent<Zombie>());
        }

        return zombies[collider];
    }
    
    public static Upgrade GetUpgrade(GameObject go)
    {
        if (!upgrades.ContainsKey(go))
        {
            upgrades.Add(go, go.GetComponent<Upgrade>());
        }

        return upgrades[go];
    }


    #endregion


}
