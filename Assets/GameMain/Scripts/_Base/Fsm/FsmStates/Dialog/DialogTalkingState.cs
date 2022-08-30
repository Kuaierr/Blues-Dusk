using UnityEngine;
using GameKit.Fsm;
using GameKit;
using GameKit.DataNode;
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
        SetTextShowing();
        fsmMaster.ParseNode(fsmMaster.uI_Response.CurIndex);
        fsmMaster.UpdateDialogUI(fsmMaster.CurrentTree.CurrentNode);
        Log.Info("DialogTalkingState");
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

                if (m_CachedCurrentNode.IsBranch)
                {
                    fsmOwner.SetData<VarType>(DialogStateUtility.STATE_AFTER_ANIMATING_ID, typeof(DialogChoosingState));
                    fsmOwner.SetData<VarAnimator>(DialogStateUtility.ANIMATOR_FOR_CHECK_ID, fsmMaster.uI_Response.Animator);
                    fsmMaster.UpdateOptionUI();
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
        m_DialogStarted = fsmOwner.GetData<VarBoolean>(DialogStateUtility.DIALOG_START_ID);

        if (!m_DialogStarted)
        {
            fsmOwner.SetData<VarType>(DialogStateUtility.STATE_AFTER_ANIMATING_ID, typeof(DialogIdleState));
            fsmOwner.SetData<VarAnimator>(DialogStateUtility.ANIMATOR_FOR_CHECK_ID, fsmMaster.dialogAnimator);
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

