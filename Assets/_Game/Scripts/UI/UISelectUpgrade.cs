using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISelectUpgrade : UICanvas
{
    [SerializeField] private Upgrade[] upgradesBtn;
    private UpgradeTypeConfig[] upgrades;
    private void OnEnable()
    {
        upgrades = GameManager.instance.GetUpgrade();
        for (int i = 0; i < 3; i++)
        {
            upgradesBtn[i].SetValue(upgrades[i]);
        }
    }
    public void ClickUpgradeButton()
    {
        GameManager.instance.SelectedUpgrade = Cache.GetUpgrade(EventSystem.current.currentSelectedGameObject);
        GameManager.instance.SetCoin(GameManager.instance.CoinAmount - GameManager.instance.SelectedUpgrade.cost);
    }

    public void ClickSkip()
    {
        GameManager.instance.ChangePhase(null);
        GameManager.instance.ChangeState(Enums.GameState.Playing);
        UI_Manager.instance.CloseUI<UISelectUpgrade>();
        GameManager.instance.ResumeGame();
    }

    private void OnDisable()
    {
        
    }
}
