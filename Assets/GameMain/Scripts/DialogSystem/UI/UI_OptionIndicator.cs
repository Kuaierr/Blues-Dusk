using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class UI_OptionIndicator : UIFormChildBase
{
    private Color m_CurrentColor;
    public Image Ring;
    public Image Core;
    private Animator m_Animator;

    public void SetColor(Color color) => m_CurrentColor = color;

    public override void OnInit(int parentDepth)
    {
        base.OnInit(parentDepth);
        m_CurrentColor = Color.white;
        m_Animator = GetComponent<Animator>();
    }

    public override void OnShow(UnityAction callback = null)
    {
        if (m_Animator != null && m_Animator.runtimeAnimatorController != null)
        {
            m_Animator.SetTrigger(UIUtility.SHOW_ANIMATION_NAME);
            return;
        }
        Ring.enabled = true;
        Core.enabled = false;
        base.OnShow(callback);
    }

    public override void OnHide(UnityAction callback = null)
    {
        m_CurrentColor = Color.white;
        if (m_Animator != null && m_Animator.runtimeAnimatorController != null)
        {
            m_Animator.SetTrigger(UIUtility.HIDE_ANIMATION_NAME);
            return;
        }
        Ring.enabled = false;
        Core.enabled = false;
        base.OnHide(callback);
    }

    public void OnCharge()
    {
        if (m_Animator != null && m_Animator.runtimeAnimatorController != null)
        {
            m_Animator.SetTrigger(UIUtility.ENABLE_ANIMATION_NAME);
            return;
        }
        Core.enabled = true;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Core.color = m_CurrentColor;
        Ring.color = m_CurrentColor;
    }
}