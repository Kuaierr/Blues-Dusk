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
        if (fsmOwner.GetData<VarBoolean>(DialogStateUtility.FIRST_TALKING) == true)
        {
            fsmOwner.SetData<VarBoolean>(DialogStateUtility.FIRST_TALKING, false);
            return;
        }

        // 如果是纯功能性节点，则自动处理并跳过
        fsmMaster.CurrentTree.CurrentNode = fsmMaster.TryExecuteNodeFunction(fsmMaster.CurrentTree.CurrentNode);
        fsmMaster.UpdateDialogUI(fsmMaster.CurrentTree.CurrentNode);
        SetTextShowing();
    }

    protected override void OnUpdate(FsmInterface fsmOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsmOwner, elapseSeconds, realElapseSeconds);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_isTextShowing == false)
            {
                if (fsmMaster.CurrentTree != null)
                {
                    Log.Info("ParseNext {0}", fsmMaster.uI_Response.CurIndex);
                    IDataNode tmpSonNode = fsmMaster.GetNextNode(GameKitCenter.Dialog.CurrentTree.CurrentNode, 0);
                    if (tmpSonNode == null)
                        return;

                    GameKitCenter.Dialog.CurrentTree.CurrentNode = tmpSonNode;
                    // 如果是功能节点，就跳过
                    GameKitCenter.Dialog.CurrentTree.CurrentNode = fsmMaster.TryExecuteNodeFunction(GameKitCenter.Dialog.CurrentTree.CurrentNode);
                    if (GameKitCenter.Dialog.CurrentTree.CurrentNode.IsBranch)
                    {
                        // 如果下一个节点是选择节点，则先显示对话
                        fsmMaster.UpdateDialogUI(GameKitCenter.Dialog.CurrentTree.CurrentNode);
                        SetTextShowing();
                        // 然后判断选择类型，转换状态
                        MonoManager.instance.StartCoroutine(ParseBranch(fsmOwner));
                        return;
                    }



                    // 如果下一个节点是普通节点，则直接显示对话
                    fsmMaster.UpdateDialogUI(GameKitCenter.Dialog.CurrentTree.CurrentNode);
                    SetTextShowing();
                }
            }
            else
                InterruptDialogDisplayCallback();
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
        
        DialogDataNodeVariable tmpSonNodeData = GameKitCenter.Dialog.CurrentTree.CurrentNode.GetData<DialogDataNodeVariable>();
        fsmOwner.SetData<VarAnimator>(DialogStateUtility.CACHED_ANIMATOR, fsmMaster.uI_Response.MasterAnimator);
        if (tmpSonNodeData.IsDiceCheckBranch)
        {
            fsmOwner.SetData<VarType>(DialogStateUtility.CACHED_AFTER_ANIMATING_STATE, typeof(DiceDialogSelectingState));
            fsmMaster.UpdateOptionUI(isDiceCheck: true);
        }
        else if (tmpSonNodeData.IsInventoryCheckOption)
        {
            fsmOwner.SetData<VarType>(DialogStateUtility.CACHED_AFTER_ANIMATING_STATE, typeof(DialogChoosingState));
            if (tmpSonNodeData.CachedInventoryName == "PlayerBackpack")
            {
                fsmMaster.UpdatePlayerInventoryCheckOptionUI();
            }
            else if (tmpSonNodeData.CachedInventoryName == "DiceInventory")
            {
                fsmMaster.UpdateDiceInventoryCheckOptionUI();
            }
            else
            {
                Debug.LogError("Inventory not exist");
            }
        }
        else
        {
            fsmOwner.SetData<VarType>(DialogStateUtility.CACHED_AFTER_ANIMATING_STATE, typeof(DialogChoosingState));
            fsmMaster.UpdateOptionUI();
        }
        ChangeState<DialogAnimatingState>(fsmOwner);
    }
}

