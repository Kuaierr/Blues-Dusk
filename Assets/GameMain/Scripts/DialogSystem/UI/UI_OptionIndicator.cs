using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityGameKit.Runtime;
using GameKit;

public class UI_OptionIndicator : UIFormChildBase
{
    private Color m_CurrentColor;
    public Image Ring;
    public Image Core;
    private string m_CachedDiceFaceType;
    public string CachedType => m_CachedDiceFaceType;

    public void SetColor(Color color) => m_CurrentColor = color;
    public void SetDiceRequire(string diceFaceType) 
    {
        m_CachedDiceFaceType = diceFaceType;
    }

    public void Init(int parentDepth)
    {
        base.OnInit(parentDepth);
        m_CurrentColor = Color.white;
        m_CachedDiceFaceType = string.Empty;
        OnDepthChanged(1);
    }

    public void Show(UnityAction callback = null)
    {
        base.OnShow(callback);
    }

    public void Hide(UnityAction callback = null)
    {
        base.OnHide(callback);
        m_CurrentColor = Color.white;
        m_CachedDiceFaceType = string.Empty;
        
        Animator.SetTrigger(UIUtility.FORCE_OFF_ANIMATION_NAME);
    }

    public void Charge(string diceFaceKeyName)
    {
        if (m_CachedDiceFaceType == diceFaceKeyName)
            Animator.SetTrigger(UIUtility.ENABLE_ANIMATION_NAME);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Core.color = m_CurrentColor;
        Ring.color = m_CurrentColor;
    }
}