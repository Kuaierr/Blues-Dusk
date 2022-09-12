using System.Collections;
using System.Collections.Generic;
using GameKit;
using GameKit.Fsm;
using UnityEngine;
using FsmInterface = GameKit.Fsm.IFsm<Card_DuelInstance>;

public class Card_StateBase : FsmState<Card_DuelInstance>, IReference
{
    protected Card_DuelInstance _fsmMaster;

    public void Clear() { }

    protected override void OnEnter(FsmInterface fsm)
    {
        base.OnEnter(fsm);
        Debug.Log(this);
        _fsmMaster = fsm.User;
    }
}