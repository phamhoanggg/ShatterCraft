using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointText : GameUnit
{
    [SerializeField] private TMP_Text pointText;
    [SerializeField] private RectTransform rectTf;
    public override void OnInit()
    {
        Invoke(nameof(OnDespawn), 0.5f);
        rectTf.position -= new Vector3(0, rectTf.position.y, 0);
    }

    public void SetText(float val)
    {
        pointText.text = "+" + val;
    }
    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    
}
