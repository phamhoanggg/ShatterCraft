using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrade : GameUnit
{
    public Enums.UpgradeType Type;
    public PoolType WeaponType;
    public float cost;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private Image weaponImg;
    [SerializeField] private TMP_Text typeText;
    [SerializeField] private Button btn;
    [SerializeField] private Image bgImg;


    private void OnEnable()
    {
        
    }
    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public void SetValue(UpgradeTypeConfig config)
    {
        Type = config.Type;
        WeaponType = config.WeaponType;
        cost = config.Cost;
        weaponImg.sprite = config.WeaponSprite;
        bgImg.sprite = config.BGSprite;

        typeText.text = Type.ToString();
        costText.text = cost.ToString();

        if (cost > GameManager.instance.CoinAmount)
        {
            btn.interactable = false;
        }
        else
        {
            btn.interactable = true;
        }
    }

}
