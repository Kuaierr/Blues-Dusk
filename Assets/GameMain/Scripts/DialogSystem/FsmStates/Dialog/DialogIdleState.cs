using UnityEngine;
using GameKit.Fsm;
using GameKit;
using UnityGameKit.Runtime;
using FsmInterface = GameKit.Fsm.IFsm<UI_Dialog>;


public class DialogIdleState : FsmState<UI_Dialog>, IReference
{
    private UI_Dialog fsmMaster;
    private bool m_FirstInit;
    public void Clear()
    {

    }

    protected override void OnInit(FsmInterface fsmOwner)
    {
        base.OnInit(fsmOwner);
        fsmMaster = fsmOwner.User;
        m_FirstInit = true;
    }

    protected override void OnEnter(FsmInterface fsmOwner)
    {
        base.OnEnter(fsmOwner);
        Log.Info("DialogIdleState");
        if (m_FirstInit)
            m_FirstInit = false;
        else
            QuickCinemachineCamera.current.ResetFocus();
    }

    protected override void OnUpdate(FsmInterface fsmOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsmOwner, elapseSeconds, realElapseSeconds);
        if (fsmMaster.IsDialoging && fsmMaster.CurrentTree != null)
        {
            fsmOwner.SetData<VarType>(DialogStateUtility.CACHED_AFTER_ANIMATING_STATE, typeof(DialogTalkingState));
            fsmOwner.SetData<VarAnimator>(DialogStateUtility.CACHED_ANIMATOR, fsmMaster.MasterAnimator);
            fsmOwner.SetData<VarBoolean>(DialogStateUtility.FIRST_TALKING, true);
            fsmMaster.PassNode();
            // fsmMaster.UpdateDialogUI(fsmMaster.CurrentTree.CurrentNode, false);
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

