using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    public bool IsDestroyOnClose = false;

    protected RectTransform m_RectTransform;
    private bool m_IsInit = false;
    private float m_OffsetY = 0;

    private void Start()
    {
        Init();
    }

    protected void Init()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    public virtual void Setup()
    {
        UI_Manager.instance.AddBackUI(this);
        UI_Manager.instance.PushBackAction(this, BackKey);
    }

    public virtual void BackKey()
    {

    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        //anim
    }

    public virtual void Close()
    {
        UI_Manager.instance.RemoveBackUI(this);
        //anim
        gameObject.SetActive(false);
        if (IsDestroyOnClose)
        {
            Destroy(gameObject);
        }

    }


}
