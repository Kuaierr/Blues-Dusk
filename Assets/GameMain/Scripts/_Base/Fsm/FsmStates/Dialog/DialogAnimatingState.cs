using System;
using UnityEngine;
using GameKit.Fsm;
using GameKit;
using UnityGameKit.Runtime;
using FsmInterface = GameKit.Fsm.IFsm<UnityGameKit.Runtime.DialogComponent>;


public class DialogAnimatingState : FsmState<DialogComponent>, IReference
{
    private DialogComponent fsmMaster;
    private Type m_CachedNextStateType;
    private bool m_isAnimating;
    public void Clear()
    {

    }

    protected override void OnInit(FsmInterface fsmOwner)
    {
        base.OnInit(fsmOwner);
        base.OnInit(fsmOwner);
        fsmMaster = fsmOwner.User;
    }

    protected override void OnEnter(FsmInterface fsmOwner)
    {
        base.OnEnter(fsmOwner);
        m_CachedNextStateType = fsmOwner.GetData<VarType>(fsmMaster.AnimatingNextDataName);
    }

    protected override void OnUpdate(FsmInterface fsmOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsmOwner, elapseSeconds, realElapseSeconds);
        if (m_CachedNextStateType == null)
            ChangeState<DialogIdleState>(fsmOwner);


        ChangeState(fsmOwner, m_CachedNextStateType);
    }

    protected override void OnExit(FsmInterface fsmOwner, bool isShutdown)
    {
        base.OnExit(fsmOwner, isShutdown);
        fsmOwner.SetData<VarType>(fsmMaster.AnimatingNextDataName, null);
        m_CachedNextStateType = null;
    }

    protected override void OnDestroy(FsmInterface fsmOwner)
    {
        base.OnDestroy(fsmOwner);
    }
}

