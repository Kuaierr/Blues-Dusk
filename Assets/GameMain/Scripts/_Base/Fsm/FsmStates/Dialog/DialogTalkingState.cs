using UnityEngine;
using GameKit.Fsm;
using GameKit;
using GameKit.DataNode;
using UnityGameKit.Runtime;
using FsmInterface = GameKit.Fsm.IFsm<UnityGameKit.Runtime.DialogComponent>;


public class DialogTalkingState : FsmState<DialogComponent>, IReference
{
    private DialogComponent fsmMaster;
    private InformUICallback m_InterruptDialogDisplayCallback;
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
        m_InterruptDialogDisplayCallback += InterruptDialogDisplayCallback;
        fsmOwner.SetData<VarInt32>("Choosen Idenx", 0);
    }

    protected override void OnEnter(FsmInterface fsmOwner)
    {
        base.OnEnter(fsmOwner);
        m_isTextShowing = false;
        int index = fsmOwner.GetData<VarInt32>("Choosen Idenx");
        fsmMaster.ParseNode(index);
        fsmMaster.InformDialogUI(fsmMaster.CurrentDialog.CurrentNode);
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
                    fsmOwner.SetData<VarType>(fsmMaster.AnimatingNextDataName, typeof(DialogChoosingState));
                    ChangeState<DialogAnimatingState>(fsmOwner);
                }
                else
                {
                    fsmMaster.InformDialogUI(m_CachedCurrentNode);
                }

            }
            else
                fsmMaster.InterruptDialogDisplay(m_InterruptDialogDisplayCallback);
        }
        m_DialogStarted = fsmOwner.GetData<VarBoolean>("Dialog Started");

        if (!m_DialogStarted)
        {
            fsmOwner.SetData<VarType>(fsmMaster.AnimatingNextDataName, typeof(DialogIdleState));
            int serialId = fsmOwner.GetData<VarInt32>("UI Dialog Serial Id");
            GameKitCenter.UI.CloseUIForm(serialId);
            ChangeState<DialogAnimatingState>(fsmOwner);
        }
    }

    protected override void OnExit(FsmInterface fsmOwner, bool isShutdown)
    {
        base.OnExit(fsmOwner, isShutdown);
        fsmOwner.SetData<VarInt32>("Choosen Idenx", 0);
    }

    protected override void OnDestroy(FsmInterface fsm)
    {
        base.OnDestroy(fsm);
        m_InterruptDialogDisplayCallback -= InterruptDialogDisplayCallback;
    }

    private void InterruptDialogDisplayCallback()
    {
        m_isTextShowing = false;
    }
}

