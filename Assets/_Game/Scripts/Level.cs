using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    public ZombieSpawner Spawner;
    public float LevelMaxValue;
    public float LevelProgressValue;
    public float WeaponMaxValue;
    public float WeaponProgressValue;

    public WeaponPlace[] WeaponPlacesList;
    public void OnInit()
    {
        gameObject.SetActive(true);
        Spawner.OnInit();
        LevelProgressValue = 0;
        WeaponProgressValue = 0;
        GameManager.instance.GamePlayObject.levelText.text = "LEVEL " + LevelController.instance.curLevelIdx;
        GameManager.instance.GamePlayObject.SetLevelProgressValue(0);
        GameManager.instance.GamePlayObject.SetWeaponProgressValue(0);
    }

    public void UpgradeWeapon(PoolType weaponType, Enums.UpgradeType action)
    {
        for(int i = 0; i < WeaponPlacesList.Length; i++)
        {
            if (WeaponPlacesList[i].IsPlaced == true)
            {
                if (action == Enums.UpgradeType.RANGE)
                {
                    WeaponPlacesList[i].WeaponPlaced.RangeUp();
                }
                else if (action == Enums.UpgradeType.SIZE)
                {
                    WeaponPlacesList[i].WeaponPlaced.SizeUp();
                }
                else if (action == Enums.UpgradeType.SPEED)
                {
                    WeaponPlacesList[i].WeaponPlaced.SpeedUp();
                }
            }
        }
    }
}
