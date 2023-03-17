using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGamePlay : UICanvas
{
    [SerializeField] private Slider levelProgressBar, weaponProgressBar;
    [SerializeField] private TMP_Text levelText;
    public TMP_Text CoinText;

    private void OnEnable()
    {
        levelText.text = "LEVEL " + LevelController.instance.curLevelIdx;
    }

    private void Update()
    {
        SetLevelProgressValue(LevelController.instance.CurrentLevel.LevelProgressValue);
        SetWeaponProgressValue(LevelController.instance.CurrentLevel.WeaponProgressValue);
    }
    public void SetLevelProgressValue(float val)
    {
        levelProgressBar.value = val / LevelController.instance.CurrentLevel.LevelMaxValue;
    }

    public void SetWeaponProgressValue(float val)
    {
        weaponProgressBar.value = val / LevelController.instance.CurrentLevel.WeaponMaxValue;
    }
}
