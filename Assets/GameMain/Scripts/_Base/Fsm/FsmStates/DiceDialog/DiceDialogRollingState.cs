using UnityEngine;
using GameKit.Fsm;
using GameKit;
using UnityGameKit.Runtime;
using FsmInterface = GameKit.Fsm.IFsm<UI_Dialog>;

/// <summary>
/// 有骰子正在运动时（即尚未出结果）的状态
/// </summary>
public class DiceDialogRollingState : FsmState<UI_Dialog>, IReference
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
        Log.Info("DiceDialogRollingState.");
        fsmMaster = updateFsm.User;
        fsmMaster.RollActiveDices();
    }

    protected override void OnUpdate(FsmInterface fsmOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsmOwner, elapseSeconds, realElapseSeconds);

        if(fsmMaster.CheckIfFinishRolling())
            ChangeState<DiceDialogResetingState>(fsmOwner);
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

