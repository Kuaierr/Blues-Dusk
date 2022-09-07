using UnityEngine;
using GameKit.Fsm;
using GameKit;
using UnityGameKit.Runtime;
using FsmInterface = GameKit.Fsm.IFsm<UI_Dialog>;

/// <summary>
/// 选择要使用的骰子的阶段
/// </summary>
public class DiceDialogSelectingState : FsmState<UI_Dialog>, IReference
{
    private UI_Dialog fsmMaster;
    private FsmInterface _fsm;

    public void Clear() { }

    protected override void OnInit(FsmInterface fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(FsmInterface updateFsm)
    {
        base.OnEnter(updateFsm);
        Debug.Log("Enter ChoiceDiceroll State.");
        fsmMaster = updateFsm.User;

        _fsm = updateFsm;
        // updateFsm.SetData<VarType>(DialogStateUtility.STATE_AFTER_ANIMATING,typeof(DiceDialogRollingState));
        // updateFsm.SetData<VarAnimator>(DialogStateUtility.ANIMATOR_FOR_CHECK,fsmMaster.DiceAnimator);
        fsmMaster.AddCheckButtonCallback(FadeToRollingState);
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
    
    private void FadeToRollingState()
    {
        ChangeState<DiceDialogRollingState>(_fsm);
    }
}