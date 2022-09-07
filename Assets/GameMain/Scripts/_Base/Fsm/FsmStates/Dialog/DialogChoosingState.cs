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
    private bool m_IsDiceChoosing;
    private Dice_Result m_CachedDiceCondition;
    public void Clear()
    {

    }

    protected override void OnInit(FsmInterface fsmOwner)
    {
        base.OnInit(fsmOwner);
        fsmMaster = fsmOwner.User;
        // GameKitCenter.Event.Subscribe(ObtainDialogChoiceEventArgs.EventId, UpdateCurrentChoosenIndex);
    }

    protected override void OnEnter(FsmInterface fsmOwner)
    {
        base.OnEnter(fsmOwner);
        m_IsDiceChoosing = fsmOwner.GetData<VarBoolean>(DialogStateUtility.IS_DICE_CHOOSING);
        Log.Info("DialogChoosingState");
        if(m_IsDiceChoosing)
        {
            m_CachedDiceCondition = fsmMaster.GetFinalResult();
            fsmMaster.UpdateOptionsPoint(m_CachedDiceCondition);
        }
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
        m_CachedDiceCondition = null;
        m_IsDiceChoosing = false;
        // GameKitCenter.Event.Unsubscribe(ObtainDialogChoiceEventArgs.EventId, UpdateCurrentChoosenIndex);
    }

    protected override void OnDestroy(FsmInterface fsm)
    {
        base.OnDestroy(fsm);
    }

}

