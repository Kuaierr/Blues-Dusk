using UnityEngine;
using System.Collections;
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
    private IDataNode m_SelectedChildNode;

    private bool _waitForBranch = false;
    
    public void Clear()
    {

    }

    protected override void OnInit(FsmInterface fsmOwner)
    {
        base.OnInit(fsmOwner);
        fsmMaster = fsmOwner.User;
        fsmMaster.AddTyperWriterListener(SetTextShowing, SetTextShown);
        fsmOwner.SetData<VarBoolean>(DialogStateUtility.FIRST_TALKING, false);
    }

    protected override void OnEnter(FsmInterface fsmOwner)
    {
        base.OnEnter(fsmOwner);
        Log.Info("DialogTalkingState");
        HandleCurrentNode(fsmOwner);
        if (fsmOwner.GetData<VarBoolean>(DialogStateUtility.FIRST_TALKING) == true)
        {
            fsmOwner.SetData<VarBoolean>(DialogStateUtility.FIRST_TALKING, false);
            return;
        }

        // HandleCurrentNode(fsmOwner);
        // // 如果是纯功能性节点，则自动处理并跳过
        // fsmMaster.CurrentTree.CurrentNode = fsmMaster.TryExecuteNodeFunction(fsmMaster.CurrentTree.CurrentNode);

        // fsmMaster.UpdateDialogUI(fsmMaster.CurrentTree.CurrentNode);
        // SetTextShowing();
    }

    protected override void OnUpdate(FsmInterface fsmOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsmOwner, elapseSeconds, realElapseSeconds);

        if (_waitForBranch) return;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (m_isTextShowing == false)
            {
                if (fsmMaster.CurrentTree != null)
                {
                    IDataNode tmpSonNode = fsmMaster.GetNextNode(GameKitCenter.Dialog.CurrentTree.CurrentNode, 0);
                    if (tmpSonNode == null)
                        return;

                    GameKitCenter.Dialog.CurrentTree.CurrentNode = tmpSonNode;
                    HandleCurrentNode(fsmOwner);
                }
            }
            else
                InterruptDialogDisplayCallback();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            fsmOwner.SetData<VarType>(DialogStateUtility.CACHED_AFTER_ANIMATING_STATE,typeof(DialogLogState));
            fsmOwner.SetData<VarString>(DialogStateUtility.CACHED_ANIMATOR_TRIGGER_NAME, UIUtility.SHOW_ANIMATION_NAME);
            fsmOwner.SetData<VarAnimator>(DialogStateUtility.CACHED_ANIMATOR, fsmMaster.TalkHistoryAnimatior);
            DialogStateUtility.In_CHOOSING = false;
            
            ChangeState<DialogAnimatingState>(fsmOwner);
        }

        if (!fsmMaster.IsDialoging)
        {
            fsmOwner.SetData<VarType>(DialogStateUtility.CACHED_AFTER_ANIMATING_STATE, typeof(DialogIdleState));
            fsmOwner.SetData<VarAnimator>(DialogStateUtility.CACHED_ANIMATOR, fsmMaster.MasterAnimator);
            fsmMaster.Pause();
            // fsmOwner.SetData<VarString>(DialogStateUtility.CACHED_ANIMATOR_TRIGGER_NAME, UIUtility.HIDE_ANIMATION_NAME);
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

    private IEnumerator ParseBranch(FsmInterface fsmOwner)
    {
        while (m_isTextShowing == true)
            yield return null;

        _waitForBranch = true;
        
        while (true)
        {
            if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                break;
            yield return 0;
        }

        _waitForBranch = false;
        
        DialogDataNodeVariable tmpSonNodeData = GameKitCenter.Dialog.CurrentTree.CurrentNode.GetData<DialogDataNodeVariable>();
        if (tmpSonNodeData.IsDiceCheckBranch)
        {
            fsmOwner.SetData<VarType>(DialogStateUtility.CACHED_AFTER_ANIMATING_STATE, typeof(DiceDialogSelectingState));
            fsmOwner.SetData<VarString>(DialogStateUtility.CACHED_ANIMATOR_TRIGGER_NAME, UIUtility.SHOW_ANIMATION_NAME);
            fsmOwner.SetData<VarAnimator>(DialogStateUtility.CACHED_ANIMATOR, fsmMaster.AppraisalAnimator);
            fsmMaster.UpdateOptionUI(isDiceCheck: true);
        }
        else
        {
            fsmOwner.SetData<VarAnimator>(DialogStateUtility.CACHED_ANIMATOR, fsmMaster.uI_Response.MasterAnimator);
            fsmOwner.SetData<VarString>(DialogStateUtility.CACHED_ANIMATOR_TRIGGER_NAME, UIUtility.SHOW_ANIMATION_NAME);
            fsmOwner.SetData<VarType>(DialogStateUtility.CACHED_AFTER_ANIMATING_STATE, typeof(DialogChoosingState));
            fsmMaster.UpdateOptionUI();
        }
        ChangeState<DialogAnimatingState>(fsmOwner);
    }

    private void HandleCurrentNode(FsmInterface fsmOwner)
    {
        // 如果是功能节点，就跳过
        GameKitCenter.Dialog.CurrentTree.CurrentNode = fsmMaster.TryExecuteNodeFunction(GameKitCenter.Dialog.CurrentTree.CurrentNode);
        if (GameKitCenter.Dialog.CurrentTree.CurrentNode.IsBranch)
        {
            foreach(var item in GameKitCenter.Dialog.CurrentTree.CurrentNode.GetAllChild())
            {
                Log.Warning(item);
            }
            
            // 如果下一个节点是选择节点，则先显示对话
            fsmMaster.UpdateDialogUI(GameKitCenter.Dialog.CurrentTree.CurrentNode);
            SetTextShowing();
            // 然后判断选择类型，转换状态
            MonoManager.instance.StartCoroutine(ParseBranch(fsmOwner));
        }
        else
        {
            // 如果下一个节点是普通节点，则直接显示对话
            fsmMaster.UpdateDialogUI(GameKitCenter.Dialog.CurrentTree.CurrentNode);
            SetTextShowing();
        }
    }
}

