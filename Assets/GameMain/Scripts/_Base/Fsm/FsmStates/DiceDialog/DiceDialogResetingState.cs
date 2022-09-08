using UnityEngine;
using GameKit.Fsm;
using GameKit;
using UnityGameKit.Runtime;
using FsmInterface = GameKit.Fsm.IFsm<UI_Dialog>;

/// <summary>
/// 骰子均静止后，将其按照规定的顺序排列
/// 排序完成后进入Choosing
/// </summary>
public class DiceDialogResetingState : FsmState<UI_Dialog>, IReference
{
    private UI_Dialog fsmMaster;
    public void Clear()
    {

    }

    protected override void OnInit(FsmInterface fsm)
    {
        base.OnInit(fsm);
        fsm.SetData<VarBoolean>(DialogStateUtility.IS_DICE_CHOOSING, false);
    }

    protected override void OnEnter(FsmInterface updateFsm)
    {
        base.OnEnter(updateFsm);
        Log.Info("DiceDialogResetingState.");
        fsmMaster = updateFsm.User;
        fsmMaster.OnFinishRolling();
    }

    protected override void OnUpdate(FsmInterface fsmOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsmOwner, elapseSeconds, realElapseSeconds);
        
        if(fsmMaster.CheckIfFinishReseting())
        {
            fsmOwner.SetData<VarBoolean>(DialogStateUtility.IS_DICE_CHOOSING, true);
            ChangeState<DiceDialogChoosingState>(fsmOwner);
        }
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

