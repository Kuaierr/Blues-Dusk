using UnityEngine;
using GameKit.Fsm;
using GameKit;
using GameKit.Event;
using UnityGameKit.Runtime;
using FsmInterface = GameKit.Fsm.IFsm<UI_Dialog>;

public class DialogChoosingState : FsmState<UI_Dialog>, IReference
{
    private UI_Dialog fsmMaster;
    private int m_CurrentIndex;
    public void Clear()
    {

    }

    protected override void OnInit(FsmInterface fsmOwner)
    {
        base.OnInit(fsmOwner);
        fsmMaster = fsmOwner.User;
        // GameKitCenter.Event.Subscribe(ObtainDialogChoiceEventArgs.EventId, UpdateCurrentChoosenIndex);
    }

    protected override void OnEnter(FsmInterface updateFsm)
    {
        base.OnEnter(updateFsm);
        Log.Info("DialogChoosingState");
    }

    protected override void OnUpdate(FsmInterface fsmOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsmOwner, elapseSeconds, realElapseSeconds);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fsmMaster.MakeChoice();
            fsmOwner.SetData<VarType>(DialogStateUtility.STATE_AFTER_ANIMATING, typeof(DialogTalkingState));
            fsmOwner.SetData<VarAnimator>(DialogStateUtility.ANIMATOR_FOR_CHECK, fsmMaster.uI_Response.MasterAnimator);
            ChangeState<DialogAnimatingState>(fsmOwner);
        }
    }

    protected override void OnExit(FsmInterface fsm, bool isShutdown)
    {
        base.OnExit(fsm, isShutdown);
        // GameKitCenter.Event.Unsubscribe(ObtainDialogChoiceEventArgs.EventId, UpdateCurrentChoosenIndex);
    }

    protected override void OnDestroy(FsmInterface fsm)
    {
        base.OnDestroy(fsm);
    }

}

