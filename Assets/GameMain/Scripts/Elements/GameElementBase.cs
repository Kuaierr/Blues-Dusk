using System;
using UnityEngine;
using UnityEngine.Events;
using GameKit.Element;
using GameKit.Setting;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;

public abstract class GameElementBase : ElementBase
{
    public float OutlineWidth = 3f;
    public UnityEvent InteractCallback;
    private Outline m_Outline;
    private Transform m_InteractTrans;
    public Vector3 InteractPosition
    {
        get
        {
            return m_InteractTrans.position;
        }
    }
    public bool IsOutlineShown
    {
        get
        {
            return m_Outline.enabled;
        }
    }

    public override void OnInit()
    {
        m_InteractTrans = transform.Find("Destination");
        m_Outline = this.gameObject.GetOrAddComponent<Outline>();
        m_Outline.OutlineWidth = OutlineWidth;
        m_Outline.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Interactive");
        // OnLoad();
    }

    public override void OnLoad()
    {
        bool b_active = GameKitCenter.Setting.GetBool(string.Format("{0}({1})", Name, "Is Active"), true);
        gameObject.SetActive(b_active);
    }

    public override void OnSave()
    {
        GameKitCenter.Setting.SetBool(string.Format("{0}({1})", Name, "Is Active"), gameObject.activeSelf);
    }

    public override void OnInteract()
    {
        base.OnInteract();
        InteractCallback?.Invoke();
    }

    public override void OnHighlightEnter()
    {
        base.OnHighlightEnter();
        if (m_Outline == null)
            Log.Warning(gameObject.name);
        if (!IsOutlineShown)
            m_Outline.enabled = true;
    }

    public override void OnHighlightExit()
    {
        base.OnHighlightExit();
        if (m_Outline == null)
            Log.Warning(gameObject.name);
        if (IsOutlineShown)
            m_Outline.enabled = false;
    }
}