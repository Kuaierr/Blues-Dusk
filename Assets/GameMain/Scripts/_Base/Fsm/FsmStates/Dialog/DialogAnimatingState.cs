using System;
using UnityEngine;
using GameKit.Fsm;
using GameKit;
using UnityGameKit.Runtime;
using FsmInterface = GameKit.Fsm.IFsm<UI_Dialog>;


public class DialogAnimatingState : FsmState<UI_Dialog>, IReference
{
    private UI_Dialog fsmMaster;
    private Type m_CachedNextStateType;
    private Animator m_CachedAnimator;
    private bool m_isAnimating;
    public void Clear()
    {

    }

    protected override void OnInit(FsmInterface fsmOwner)
    {
        base.OnInit(fsmOwner);
        fsmMaster = fsmOwner.User;
    }

    protected override void OnEnter(FsmInterface fsmOwner)
    {
        base.OnEnter(fsmOwner);
        m_CachedNextStateType = fsmOwner.GetData<VarType>(DialogStateUtility.STATE_AFTER_ANIMATING);
        m_CachedAnimator = fsmOwner.GetData<VarAnimator>(DialogStateUtility.ANIMATOR_FOR_CHECK);
        Log.Info("DialogAnimatingState");
    }

    protected override void OnUpdate(FsmInterface fsmOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsmOwner, elapseSeconds, realElapseSeconds);
        
        if (m_CachedNextStateType == null)
            ChangeState<DialogIdleState>(fsmOwner);

        if (m_CachedAnimator.IsComplete())
            ChangeState(fsmOwner, m_CachedNextStateType);
    }

    protected override void OnExit(FsmInterface fsmOwner, bool isShutdown)
    {
        base.OnExit(fsmOwner, isShutdown);
        fsmOwner.SetData<VarType>(DialogStateUtility.STATE_AFTER_ANIMATING, null);
        m_CachedNextStateType = null;
    }

    protected override void OnDestroy(FsmInterface fsmOwner)
    {
        base.OnDestroy(fsmOwner);
    }
}

