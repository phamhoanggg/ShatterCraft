using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeaponState : IState
{
    Enums.UpgradeType action;
    public void OnEnter()
    {
        action = Enums.UpgradeType.None;
        GameManager.instance.SelectedUpgrade = null;
        UI_Manager.instance.OpenUI<UISelectUpgrade>();
        GameManager.instance.PauseGame();
        GameManager.instance.ChangeState(Enums.GameState.ChoosingWeapon);
    }

    public void OnExecute()
    {
        if (action == Enums.UpgradeType.None)
        {
            if (GameManager.instance.SelectedUpgrade != null)
            {
                action = GameManager.instance.SelectedUpgrade.Type;
            } 
        }
        else if (action == Enums.UpgradeType.ADD)
        {
            GameManager.instance.SelectedWeapon = GameManager.instance.SelectedUpgrade.WeaponType;
            GameManager.instance.ChangePhase(GameManager.instance.SetPlaceWeaponPhase);
            OnExit();
        }
        else
        {
            GameManager.instance.ResumeGame();
            GameManager.instance.ChangePhase(null);
            LevelController.instance.CurrentLevel.UpgradeWeapon(GameManager.instance.SelectedUpgrade.WeaponType, action);
            OnExit();
        }
    }

    public void OnExit()
    {
        UI_Manager.instance.CloseUI<UISelectUpgrade>();
    }
}
