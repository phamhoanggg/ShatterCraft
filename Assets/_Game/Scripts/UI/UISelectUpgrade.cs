using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISelectUpgrade : UICanvas
{
    [SerializeField] RectTransform[] selections;
    private Upgrade[] upgrades;
    private void OnEnable()
    {
        upgrades = GameManager.instance.DisplayUpgrade();
        for (int i = 0; i < 3; i++)
        {
            Upgrade upgrade = SimplePool.Spawn<Upgrade>(upgrades[i].poolType);
            upgrade.SetParentTF(selections[i]);
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
        for (int i = 0; i < 3; i++)
        {
            upgrades[i].OnDespawn();
        }
    }
}
