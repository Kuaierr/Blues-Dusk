using System.Collections;
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
    private bool m_AllOptionLocked;
    public void Clear()
    {

    }

    protected override void OnInit(FsmInterface fsmOwner)
    {
        base.OnInit(fsmOwner);
        fsmMaster = fsmOwner.User;
        m_AllOptionLocked = false;
        // GameKitCenter.Event.Subscribe(ObtainDialogChoiceEventArgs.EventId, UpdateCurrentChoosenIndex);
    }

    protected override void OnEnter(FsmInterface fsmOwner)
    {
        Log.Info("DialogChoosingState");
        base.OnEnter(fsmOwner);
        m_IsDiceChoosing = fsmOwner.GetData<VarBoolean>(DialogStateUtility.IS_DICE_CHOOSING);
        if (m_IsDiceChoosing)
        {
            m_CachedDiceCondition = fsmMaster.GetFinalResult();
            fsmMaster.UpdateOptionsPoint(m_CachedDiceCondition);
        }
        m_AllOptionLocked = fsmMaster.uI_Response.AllLocked;
        if (m_AllOptionLocked)
        {
            MonoManager.instance.StartCoroutine(GoToDefaultNode(fsmOwner));
        }
    }

    protected override void OnUpdate(FsmInterface fsmOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsmOwner, elapseSeconds, realElapseSeconds);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !m_AllOptionLocked)
        {
            fsmOwner.SetData<VarType>(DialogStateUtility.CACHED_AFTER_ANIMATING_STATE, typeof(DialogTalkingState));
            fsmOwner.SetData<VarAnimator>(DialogStateUtility.CACHED_ANIMATOR, fsmMaster.uI_Response.MasterAnimator);
            GameKitCenter.Dialog.CurrentTree.CurrentNode = fsmMaster.GetNextNode(GameKitCenter.Dialog.CurrentTree.CurrentNode, fsmMaster.uI_Response.CurIndex);
            // fsmOwner.SetData<VarString>(DialogStateUtility.CACHED_ANIMATOR_TRIGGER_NAME, UIUtility.HIDE_ANIMATION_NAME);
            fsmMaster.HideResponse();
            ChangeState<DialogAnimatingState>(fsmOwner);
        }
    }

    protected override void OnExit(FsmInterface fsm, bool isShutdown)
    {
        base.OnExit(fsm, isShutdown);

        if (m_IsDiceChoosing)
            fsmMaster.uI_DiceSystem.Clear();

        m_CachedDiceCondition = null;
        m_IsDiceChoosing = false;
        // GameKitCenter.Event.Unsubscribe(ObtainDialogChoiceEventArgs.EventId, UpdateCurrentChoosenIndex);
    }

    protected override void OnDestroy(FsmInterface fsm)
    {
        base.OnDestroy(fsm);
    }

    IEnumerator GoToDefaultNode(FsmInterface fsmOwner)
    {
        yield return new WaitForSeconds(1f);
        Log.Warning(">>>> " + fsmMaster.uI_Response.DefaultOptionIndex);
        fsmOwner.SetData<VarType>(DialogStateUtility.CACHED_AFTER_ANIMATING_STATE, typeof(DialogTalkingState));
        fsmOwner.SetData<VarAnimator>(DialogStateUtility.CACHED_ANIMATOR, fsmMaster.uI_Response.MasterAnimator);
        GameKitCenter.Dialog.CurrentTree.CurrentNode = fsmMaster.GetNextNode(GameKitCenter.Dialog.CurrentTree.CurrentNode, fsmMaster.uI_Response.DefaultOptionIndex);
        fsmMaster.HideResponse();
        ChangeState<DialogAnimatingState>(fsmOwner);
    }
}

