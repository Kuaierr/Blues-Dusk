using UnityEngine;
using GameKit.Fsm;
using GameKit;
using FsmInterface = GameKit.Fsm.IFsm<DialogSystem>;


public class DialogTalkState : FsmState<DialogSystem>, IReference
{
    private DialogSystem masterFsm;
    public void Clear()
    {

    }

    protected override void OnEnter(FsmInterface updateFsm)
    {
        base.OnEnter(updateFsm);
        Debug.Log("Enter Talk State.");
        masterFsm = updateFsm.User;
    }

    protected override void OnUpdate(FsmInterface ifsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(ifsm, elapseSeconds, realElapseSeconds);
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

