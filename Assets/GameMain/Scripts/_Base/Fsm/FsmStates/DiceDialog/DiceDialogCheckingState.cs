using UnityEngine;
using GameKit.Fsm;
using GameKit;
using UnityGameKit.Runtime;
using FsmInterface = GameKit.Fsm.IFsm<UI_Dialog>;

/// <summary>
/// 长按「鉴定」按钮的状态
/// 如果放开按钮，就切换回到Selecting状态
/// 这个状态目前不是很必要，先用作进入鉴定系统的过渡
/// </summary>
public class DiceDialogCheckingState : FsmState<UI_Dialog>, IReference
{
    private UI_Dialog fsmMaster;
    public void Clear()
    {

    }

    protected override void OnInit(FsmInterface fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(FsmInterface updateFsm)
    {
        base.OnEnter(updateFsm);
        Debug.Log("Enter ChoiceDiceroll State.");
        fsmMaster = updateFsm.User;
        
        fsmMaster.InitDiceSystem();
        
        fsmMaster.InitDiceSystem();
        updateFsm.SetData<VarType>(DialogStateUtility.STATE_AFTER_ANIMATING,typeof(DiceDialogSelectingState));
        updateFsm.SetData<VarAnimator>(DialogStateUtility.ANIMATOR_FOR_CHECK,fsmMaster.DiceAnimator);
        ChangeState<DialogAnimatingState>(updateFsm);
    }

    protected override void OnUpdate(FsmInterface fsmOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsmOwner, elapseSeconds, realElapseSeconds);
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

