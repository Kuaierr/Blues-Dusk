using System.Collections;
using System.Collections.Generic;
using GameKit.Fsm;
using UnityEngine;

public class Card_DuelInstance : MonoBehaviour
{
	private IFsm<Card_DuelInstance> _fsm;
	private List<FsmState<Card_DuelInstance>> _stateList;

	private void CreateFsm()
	{
		_stateList = new List<FsmState<Card_DuelInstance>>();
		_stateList.Add(new Card_StartPhaseState());

		_fsm = GameKitCenter.Fsm.CreateFsm<Card_DuelInstance>(gameObject.name, this, _stateList);
	}

	private void StartFsm()
	{
		
	}

	private void DestroyFsm()
	{
		GameKitCenter.Fsm.DestroyFsm(_fsm);
		_stateList.Clear();
		_fsm = null;
	}
}
