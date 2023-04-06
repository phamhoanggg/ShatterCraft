using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGamePlay : UICanvas
{
    [SerializeField] private Slider levelProgressBar, weaponProgressBar;
    [SerializeField] private Image vibrateImg;
    [SerializeField] private Sprite vibrateOn, vibrateOff;
    public TMP_Text levelText;
    public TMP_Text CoinText;

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

    public void ClickSetting()
    {
        vibrateImg.gameObject.SetActive(vibrateImg.gameObject.activeInHierarchy ? false : true);
        vibrateImg.sprite = GameManager.instance.IsVibrate ? vibrateOn : vibrateOff;
    }

    public void ClickReplay()
    {
        LevelController.instance.RePlayLevel();
    }

    public void ClickVibrateBtn()
    {
        GameManager.instance.IsVibrate = GameManager.instance.IsVibrate ? false : true;
        vibrateImg.sprite = GameManager.instance.IsVibrate ? vibrateOn : vibrateOff;
    }

    public void ClickDragCameraBtn()
    {
        if (GameManager.instance.IsState(Enums.GameState.Playing))
        {
            GameManager.instance.IsCameraDragging = true;
            UI_Manager.instance.CloseUI<UIGamePlay>();
        }
        
    }

    public void PlayAnim(string anim)
    {
        this.anim.SetTrigger(anim);
    }
}
