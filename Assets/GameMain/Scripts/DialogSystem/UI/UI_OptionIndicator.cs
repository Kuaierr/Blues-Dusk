using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class UI_OptionIndicator : UIFormChildBase
{
    private Animator m_Animator;
    public override void OnInit(int parentDepth)
    {
        base.OnInit(parentDepth);
        m_Animator = GetComponent<Animator>();
    }
    public override void OnShow(UnityAction callback = null)
    {
        if (m_Animator != null && m_Animator.runtimeAnimatorController != null)
        {
            m_Animator.SetTrigger(UIUtility.SHOW_ANIMATION_NAME);
            return;
        }
        base.OnShow(callback);
    }

    public override void OnHide(UnityAction callback = null)
    {
        if (m_Animator != null && m_Animator.runtimeAnimatorController != null)
        {
            m_Animator.SetTrigger(UIUtility.HIDE_ANIMATION_NAME);
            return;
        }
        base.OnHide(callback);
    }

    public void OnCharge()
    {
        if (m_Animator != null && m_Animator.runtimeAnimatorController != null)
        {
            m_Animator.SetTrigger(UIUtility.ENABLE_ANIMATION_NAME);
            return;
        }
    }
}