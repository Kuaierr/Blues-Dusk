using UnityEngine;
using GameKit.Fsm;
using GameKit;
using UnityGameKit.Runtime;
using FsmInterface = GameKit.Fsm.IFsm<UnityGameKit.Runtime.DialogComponent>;


public class DialogIdleState : FsmState<DialogComponent>, IReference
{
    private DialogComponent fsmMaster;
    private bool m_DialogStarted;
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
        fsmOwner.SetData<VarBoolean>("Dialog Started", false);
    }

    protected override void OnUpdate(FsmInterface fsmOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsmOwner, elapseSeconds, realElapseSeconds);
        m_DialogStarted = fsmOwner.GetData<VarBoolean>("Dialog Started");
        if(m_DialogStarted)
        {
            int? uiSerialId = GameKitCenter.UI.TryOpenUIForm("UI_Dialog");
            fsmOwner.SetData<VarType>(fsmMaster.AnimatingNextDataName, typeof(DialogTalkingState));
            fsmOwner.SetData<VarInt32>("UI Dialog Serial Id", uiSerialId);
            ChangeState<DialogAnimatingState>(fsmOwner);
        } 
    }

    protected override void OnExit(FsmInterface fsm, bool isShutdown)
    {
        base.OnExit(fsm, isShutdown);
    }

    protected override void OnDestroy(FsmInterface fsm)
    {
        base.OnDestroy(fsm);
    }

    
}

