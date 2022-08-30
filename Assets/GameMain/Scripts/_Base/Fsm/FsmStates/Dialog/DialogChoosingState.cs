using UnityEngine;
using GameKit.Fsm;
using GameKit;
using GameKit.Event;
using UnityGameKit.Runtime;
using FsmInterface = GameKit.Fsm.IFsm<UnityGameKit.Runtime.DialogComponent>;

public class DialogChoosingState : FsmState<DialogComponent>, IReference
{
    private DialogComponent fsmMaster;
    private int m_CurrentIndex;
    public void Clear()
    {

    }

    protected override void OnInit(FsmInterface fsmOwner)
    {
        base.OnInit(fsmOwner);
        fsmMaster = fsmOwner.User;
        GameKitCenter.Event.Subscribe(ObtainDialogChoiceEventArgs.EventId, UpdateCurrentChoosenIndex);
    }

    protected override void OnEnter(FsmInterface updateFsm)
    {
        base.OnEnter(updateFsm);
        m_CurrentIndex = 0;
        fsmMaster.InformOptionUI();
    }

    protected override void OnUpdate(FsmInterface fsmOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsmOwner, elapseSeconds, realElapseSeconds);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MakeChoice(fsmOwner);
        }
    }

    protected override void OnExit(FsmInterface fsm, bool isShutdown)
    {
        base.OnExit(fsm, isShutdown);
        GameKitCenter.Event.Unsubscribe(ObtainDialogChoiceEventArgs.EventId, UpdateCurrentChoosenIndex);
    }

    protected override void OnDestroy(FsmInterface fsm)
    {
        base.OnDestroy(fsm);
    }

    private void UpdateCurrentChoosenIndex(object sender, GameEventArgs e)
    {
        ObtainDialogChoiceEventArgs eventArg = (ObtainDialogChoiceEventArgs)e;
        m_CurrentIndex = eventArg.ChoosenIndex;
    }

    public void MakeChoice(FsmInterface fsmOwner)
    {
        fsmOwner.SetData<VarType>(fsmMaster.AnimatingNextDataName, typeof(DialogTalkingState));
        fsmOwner.SetData<VarInt32>("Choosen Idenx", m_CurrentIndex);
        ChangeState<DialogAnimatingState>(fsmOwner);
    }
}

