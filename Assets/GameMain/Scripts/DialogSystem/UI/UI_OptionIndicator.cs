using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI_OptionIndicator : UIFormChildBase
{
    private Color m_CurrentColor;
    public Image Ring;
    public Image Core;

    public void SetColor(Color color) => m_CurrentColor = color;

    public override void OnInit(int parentDepth)
    {
        base.OnInit(parentDepth);
        m_CurrentColor = Color.white;
        
    }

    public override void OnShow(UnityAction callback = null)
    {
        Ring.enabled = true;
        Core.enabled = false;
        base.OnShow(callback);
    }

    public override void OnHide(UnityAction callback = null)
    {
        m_CurrentColor = Color.white;
        Ring.enabled = false;
        Core.enabled = false;
        base.OnHide(callback);
    }

    public void OnCharge()
    {
        Core.enabled = true;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Core.color = m_CurrentColor;
        Ring.color = m_CurrentColor;
    }
}