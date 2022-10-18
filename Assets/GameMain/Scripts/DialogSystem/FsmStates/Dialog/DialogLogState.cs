using System.Collections;
using System.Collections.Generic;
using GameKit;
using GameKit.Fsm;
using UnityEngine;
using UnityGameKit.Runtime;
using FsmInterface = GameKit.Fsm.IFsm<UI_Dialog>;

public class DialogLogState : FsmState<UI_Dialog>, IReference
{
	private UI_Dialog fsmMaster;
	private FsmInterface fsmInterface;
	
	public void Clear()
	{
		
	}

	protected override void OnInit(FsmInterface fsm)
	{
		base.OnInit(fsm);
		fsmMaster = fsm.User;
		fsmInterface = fsm;
	}

	protected override void OnUpdate(FsmInterface fsm, float elapseSeconds, float realElapseSeconds)
	{
		base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

		if (GameKit.QuickCode.InputManager.instance.GetKeyDown(KeyCode.S))
		{
			if(DialogStateUtility.In_CHOOSING)
				fsm.SetData<VarType>(DialogStateUtility.CACHED_AFTER_ANIMATING_STATE,typeof(DialogChoosingState));
			else 
				fsm.SetData<VarType>(DialogStateUtility.CACHED_AFTER_ANIMATING_STATE,typeof(DialogTalkingState));
			fsm.SetData<VarString>(DialogStateUtility.CACHED_ANIMATOR_TRIGGER_NAME, UIUtility.HIDE_ANIMATION_NAME);
			fsm.SetData<VarAnimator>(DialogStateUtility.CACHED_ANIMATOR,fsmMaster.TalkHistoryAnimatior);
			
			ChangeState<DialogAnimatingState>(fsm);
		}
	}

	public void Exit()
	{
		if(DialogStateUtility.In_CHOOSING)
			fsmInterface.SetData<VarType>(DialogStateUtility.CACHED_AFTER_ANIMATING_STATE,typeof(DialogChoosingState));
		else 
			fsmInterface.SetData<VarType>(DialogStateUtility.CACHED_AFTER_ANIMATING_STATE,typeof(DialogTalkingState));
		fsmInterface.SetData<VarAnimator>(DialogStateUtility.CACHED_ANIMATOR,fsmMaster.TalkHistoryAnimatior);
			
		ChangeState<DialogAnimatingState>(fsmInterface);
	}
}
