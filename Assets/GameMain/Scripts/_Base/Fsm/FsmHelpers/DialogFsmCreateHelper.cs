using GameKit.Fsm;
using UnityEngine;
using System.Collections.Generic;
using UnityGameKit.Runtime;

public sealed class DialogFsmCreateHelper : FsmCreateHelperBase<DialogComponent>
{
    public override void CreateFsm(ref IFsm<DialogComponent> fsm, List<FsmState<DialogComponent>> stateList, string fsmName, DialogComponent fsmOwner)
    {
        // stateList.Add(new DialogIdleState());
        // stateList.Add(new DialogTalkingState());
        // stateList.Add(new DialogChoosingState());
        // stateList.Add(new DialogAnimatingState());
        // fsm = GameKitCenter.Fsm.CreateFsm<DialogComponent>(fsmName, fsmOwner, stateList);
    }
    public override void StartFsm(ref IFsm<DialogComponent> fsm)
    {
        // fsm.Start<DialogIdleState>();
    }
    public override void DestroyFsm(ref IFsm<DialogComponent> fsm, List<FsmState<DialogComponent>> stateList)
    {
        // GameKitCenter.Fsm.DestroyFsm(fsm);
        // stateList.Clear();
        // fsm = null;
    }
}

