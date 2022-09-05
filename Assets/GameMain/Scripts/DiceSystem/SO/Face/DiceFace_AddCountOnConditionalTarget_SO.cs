using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AddConditionalTarget",menuName = "GameMain/DiceData/DiceFace/AddConditionalTarget")]
public class DiceFace_AddCountOnConditionalTarget_SO : UI_DiceFaceBase_SO
{
	public override int Priority => priority;
	public override Dice_SuitType Type => Dice_SuitType.SPECIAL;
	
	[Space]
	public int priority = 1;
	public int amount = 1;

	public UI_DiceConditionalTargetBase_SO conditionalTarget;
	
	public override void Effect(Dice_Result result)
	{
		var targets = conditionalTarget.FindTarget(result);
		foreach (Dice_SuitType type in targets)
		{
			result.Add(type,amount);
		}
	}
}
