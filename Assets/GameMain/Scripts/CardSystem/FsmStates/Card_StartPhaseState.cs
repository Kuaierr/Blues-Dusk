using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FsmInterface = GameKit.Fsm.IFsm<Card_DuelInstance>;

public class Card_StartPhaseState : Card_StateBase
{
	protected override void OnEnter(FsmInterface fsm)
	{
		base.OnEnter(fsm);
	}
}
