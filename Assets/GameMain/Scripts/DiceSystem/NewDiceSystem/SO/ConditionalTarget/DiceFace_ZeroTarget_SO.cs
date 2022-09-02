using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equal To Zero", menuName = "GameMain/DiceData/ConditionalTarget/EqualToZero")]
public class DiceFace_ZeroTarget_SO : UI_DiceConditionalTargetBase_SO
{
	public override List<Dice_SuitType> FindTarget(Dice_Result result)
	{
		List<Dice_SuitType> res = new List<Dice_SuitType>();
		//只有四种花色 直接手撕也可以吧
		foreach (Dice_SuitType key in result.sum.Keys)
		{
			if(result.Get(key) == 0) res.Add(key);
		}

		return res;
	}
}
