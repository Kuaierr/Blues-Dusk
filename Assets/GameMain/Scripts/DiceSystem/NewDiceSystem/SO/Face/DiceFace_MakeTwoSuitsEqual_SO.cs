using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MakeEqual",menuName = "GameMain/DiceData/DiceFace/MakeEqual")]
public class DiceFace_MakeTwoSuitsEqual_SO : UI_DiceFaceBase_SO
{
	public int priority = 1;
	public override int Priority => priority;
	public override Dice_SuitType Type => Dice_SuitType.SPECIAL;

	public UI_DiceConditionalTargetBase_SO target;
	public UI_DiceConditionalTargetBase_SO source;
	public override void Effect(Dice_Result result)
	{
		var targets = target.FindTarget(result);
		Dice_SuitType suitType = source.FindTarget(result)[0];

		foreach (Dice_SuitType type in targets)
		{
			result.Set(type,result.Get(suitType));
		}
	}
}
