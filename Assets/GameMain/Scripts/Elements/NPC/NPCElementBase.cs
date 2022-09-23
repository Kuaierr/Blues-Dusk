using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameKit.Element;
using GameKit.Setting;
using GameKit.Event;
using UnityGameKit.Runtime;

public abstract class NPCElementBase : GameElementBase
{
    public float OutlineWidth = 3f;
    private Outline[] m_Outlines;
    public bool IsOutlineShown
    {
        get
        {
            if (m_Outlines == null || m_Outlines.Length == 0)
                return false;
            for (int i = 0; i < m_Outlines.Length; i++)
            {
                if (!m_Outlines[i].enabled)
                    return false;
            }
            return true;
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        if (m_Outlines == null || m_Outlines.Length == 0)
            m_Outlines = this.gameObject.GetComponentsInChildren<Outline>();
        for (int i = 0; i < m_Outlines.Length; i++)
        {
            m_Outlines[i].OutlineWidth = OutlineWidth;
            m_Outlines[i].enabled = false;
        }
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
        if (m_Outlines == null)
            return;
        if (!IsOutlineShown)
            SetOutline(true);
    }

    public override void OnHighlightExit()
    {
        base.OnHighlightExit();
        if (m_Outlines == null)
            return;
        if (IsOutlineShown)
            SetOutline(false);
    }

    protected override void OnValidate()
    {
        base.OnValidate();
    }

    private void SetOutline(bool status)
    {
        for (int i = 0; i < m_Outlines.Length; i++)
        {
            m_Outlines[i].enabled = status;
        }
    }
}