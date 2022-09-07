using UnityEngine;
using GameKit.Fsm;
using GameKit;
using GameKit.DataNode;
using GameKit.Dialog;
using UnityGameKit.Runtime;
using FsmInterface = GameKit.Fsm.IFsm<UI_Dialog>;


public class DialogTalkingState : FsmState<UI_Dialog>, IReference
{
    private UI_Dialog fsmMaster;
    private bool m_isTextShowing;
    private bool m_DialogStarted;
    private IDataNode m_CachedCurrentNode;
    public void Clear()
    {

    }

    protected override void OnInit(FsmInterface fsmOwner)
    {
        base.OnInit(fsmOwner);
        fsmMaster = fsmOwner.User;
        fsmMaster.AddTyperWriterListener(SetTextShowing, SetTextShown);
    }

    protected override void OnEnter(FsmInterface fsmOwner)
    {
        base.OnEnter(fsmOwner);
        Log.Info("DialogTalkingState");
        SetTextShowing();

        if (fsmOwner.GetData<VarBoolean>(DialogStateUtility.DIALOG_FIRST_START))
        {
            fsmOwner.SetData<VarBoolean>(DialogStateUtility.DIALOG_FIRST_START, false);
            return;
        }
        fsmMaster.ParseNode(fsmMaster.uI_Response.CurIndex);
        fsmMaster.UpdateDialogUI(fsmMaster.CurrentTree.CurrentNode);
    }

    protected override void OnUpdate(FsmInterface fsmOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsmOwner, elapseSeconds, realElapseSeconds);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_isTextShowing == false)
            {
                m_CachedCurrentNode = fsmMaster.ParseNode();
                if (m_CachedCurrentNode == null)
                    return;

                DialogDataNodeVariable nodeData = m_CachedCurrentNode.GetData<DialogDataNodeVariable>();
                if (m_CachedCurrentNode.IsBranch)
                {
                    fsmOwner.SetData<VarAnimator>(DialogStateUtility.ANIMATOR_FOR_CHECK, fsmMaster.uI_Response.MasterAnimator);

                    if (nodeData.IsDiceCheckBranch)
                    {
                        fsmOwner.SetData<VarType>(DialogStateUtility.STATE_AFTER_ANIMATING, typeof(DiceDialogSelectingState));
                        fsmMaster.UpdateOptionUI(isDiceCheck: true);
                    }
                    else
                    {
                        fsmOwner.SetData<VarType>(DialogStateUtility.STATE_AFTER_ANIMATING, typeof(DialogChoosingState));
                        fsmMaster.UpdateOptionUI();
                    }
                    ChangeState<DialogAnimatingState>(fsmOwner);
                }
                else
                {
                    fsmMaster.UpdateDialogUI(m_CachedCurrentNode);
                }
            }
            else
                InterruptDialogDisplayCallback();
        }

        m_DialogStarted = fsmOwner.GetData<VarBoolean>(DialogStateUtility.DIALOG_START);
        if (!m_DialogStarted)
        {
            fsmOwner.SetData<VarType>(DialogStateUtility.STATE_AFTER_ANIMATING, typeof(DialogIdleState));
            fsmOwner.SetData<VarAnimator>(DialogStateUtility.ANIMATOR_FOR_CHECK, fsmMaster.MasterAnimator);
            fsmMaster.Pause();
            ChangeState<DialogAnimatingState>(fsmOwner);
        }
    }

    protected override void OnExit(FsmInterface fsmOwner, bool isShutdown)
    {
        base.OnExit(fsmOwner, isShutdown);
    }

    protected override void OnDestroy(FsmInterface fsm)
    {
        base.OnDestroy(fsm);
    }

    private void InterruptDialogDisplayCallback()
    {
        fsmMaster.TextAnimatorPlayer.SkipTypewriter();
        m_isTextShowing = false;
    }

    private void SetTextShown()
    {
        m_isTextShowing = false;
    }

    private void SetTextShowing()
    {
        m_isTextShowing = true;
    }
}

