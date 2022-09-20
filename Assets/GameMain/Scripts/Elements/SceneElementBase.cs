using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameKit.Element;
using GameKit.Setting;
using GameKit.Event;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;

[RequireComponent(typeof(Outline))]
public abstract class SceneElementBase : GameElementBase
{
    public float OutlineWidth = 3f;
    private Outline m_Outline;
    public bool IsOutlineShown
    {
        get
        {
            return m_Outline.enabled;
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        m_Outline = this.gameObject.GetOrAddComponent<Outline>();
        m_Outline.OutlineWidth = OutlineWidth;
        m_Outline.enabled = false;
    }

    public override void OnLoad(object sender, GameEventArgs e)
    {
        base.OnLoad(sender, e);
    }

    public override void OnSave(object sender, GameEventArgs e)
    {
        base.OnSave(sender, e);
    }

    public override void OnInteract()
    {
        base.OnInteract();
    }

    public override void OnHighlightEnter()
    {
        base.OnHighlightEnter();
        if (m_Outline == null)
            return;
        if (!IsOutlineShown)
            m_Outline.enabled = true;
    }

    public override void OnHighlightExit()
    {
        base.OnHighlightExit();
        if (m_Outline == null)
            return;
        if (IsOutlineShown)
            m_Outline.enabled = false;
    }
}