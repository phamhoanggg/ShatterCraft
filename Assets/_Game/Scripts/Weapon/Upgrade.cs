using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrade : GameUnit
{
    public Enums.UpgradeType Type;
    public PoolType WeaponType;
    private RectTransform m_RectTransform;
    public float cost;
    [SerializeField] private TMP_Text costText;
    private Button btn;


    private void Awake()
    {
        GameManager.instance.UpgradeList.Add(this);
        m_RectTransform = GetComponent<RectTransform>();
        btn = GetComponent<Button>();
        costText.text = cost.ToString();
    }

    private void OnEnable()
    {
        if (cost > GameManager.instance.CoinAmount)
        {
            btn.interactable = false;
        }
        else
        {
            btn.interactable = true;
        }
    }
    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    public void SetParentTF(RectTransform parent)
    {
        m_RectTransform.SetParent(parent);
        m_RectTransform.anchoredPosition = Vector3.zero;
    }
}
