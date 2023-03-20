using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "UpgradeTypeConfig", menuName = "GameConfiguration/UpgradeSetup", order = 1)]
public class UpgradeTypeConfig : ScriptableObject
{
    public Enums.UpgradeType Type;
    public PoolType WeaponType;
    public Sprite WeaponSprite;
    public float Cost;
}
