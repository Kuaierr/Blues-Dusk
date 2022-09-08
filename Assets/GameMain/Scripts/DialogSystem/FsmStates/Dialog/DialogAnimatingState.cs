using System;
using UnityEngine;
using GameKit.Fsm;
using GameKit;
using UnityGameKit.Runtime;
using FsmInterface = GameKit.Fsm.IFsm<UI_Dialog>;


public class DialogAnimatingState : FsmState<UI_Dialog>, IReference
{
    private UI_Dialog fsmMaster;
    private Type m_CachedState;
    private Animator m_CachedAnimator;
    private string m_CachedTriggerName;
    private bool m_isAnimating;
    public void Clear()
    {

    }

    protected override void OnInit(FsmInterface fsmOwner)
    {
        base.OnInit(fsmOwner);
        fsmOwner.SetData<VarType>(DialogStateUtility.CACHED_AFTER_ANIMATING_STATE, null);
        fsmOwner.SetData<VarAnimator>(DialogStateUtility.CACHED_ANIMATOR, null);
        fsmOwner.SetData<VarString>(DialogStateUtility.CACHED_ANIMATOR_TRIGGER_NAME, string.Empty);
        fsmMaster = fsmOwner.User;
    }

    protected override void OnEnter(FsmInterface fsmOwner)
    {
        base.OnEnter(fsmOwner);
        m_CachedState = fsmOwner.GetData<VarType>(DialogStateUtility.CACHED_AFTER_ANIMATING_STATE);
        m_CachedAnimator = fsmOwner.GetData<VarAnimator>(DialogStateUtility.CACHED_ANIMATOR);
        m_CachedTriggerName = fsmOwner.GetData<VarString>(DialogStateUtility.CACHED_ANIMATOR_TRIGGER_NAME);
        if (m_CachedTriggerName != string.Empty)
            m_CachedAnimator.SetTrigger(m_CachedTriggerName);
        Log.Info("DialogAnimatingState");
    }

    protected override void OnUpdate(FsmInterface fsmOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsmOwner, elapseSeconds, realElapseSeconds);
        if (m_CachedAnimator.IsComplete() && m_CachedState != null)
            ChangeState(fsmOwner, m_CachedState);
    }

    protected override void OnExit(FsmInterface fsmOwner, bool isShutdown)
    {
        base.OnExit(fsmOwner, isShutdown);
        fsmOwner.SetData<VarType>(DialogStateUtility.CACHED_AFTER_ANIMATING_STATE, null);
        fsmOwner.SetData<VarAnimator>(DialogStateUtility.CACHED_ANIMATOR, null);
        fsmOwner.SetData<VarString>(DialogStateUtility.CACHED_ANIMATOR_TRIGGER_NAME, string.Empty);
    }

    protected override void OnDestroy(FsmInterface fsmOwner)
    {
        base.OnDestroy(fsmOwner);
    }
}

