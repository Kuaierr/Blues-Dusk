using UnityEngine;
using GameKit.Fsm;
using GameKit;
using UnityGameKit.Runtime;
using FsmInterface = GameKit.Fsm.IFsm<UI_Dialog>;


public class DialogIdleState : FsmState<UI_Dialog>, IReference
{
    private UI_Dialog fsmMaster;
    private bool m_DialogStarted;
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
        fsmMaster.InternalVisible(false);
        Log.Info("DialogIdleState");
    }

    protected override void OnUpdate(FsmInterface fsmOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsmOwner, elapseSeconds, realElapseSeconds);
        m_DialogStarted = fsmOwner.GetData<VarBoolean>(DialogStateUtility.DIALOG_START);
        if (m_DialogStarted && fsmMaster.CurrentTree != null)
        {
            fsmMaster.Resume();
            fsmOwner.SetData<VarType>(DialogStateUtility.STATE_AFTER_ANIMATING, typeof(DialogTalkingState));
            fsmOwner.SetData<VarAnimator>(DialogStateUtility.ANIMATOR_FOR_CHECK, fsmMaster.MasterAnimator);
            fsmOwner.SetData<VarBoolean>(DialogStateUtility.DIALOG_FIRST_START, true);
            fsmMaster.ParseNode(fsmMaster.uI_Response.CurIndex);
            fsmMaster.UpdateDialogUI(fsmMaster.CurrentTree.CurrentNode, false);
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

