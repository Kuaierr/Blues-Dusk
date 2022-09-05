using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Min Suits", menuName = "GameMain/DiceData/ConditionalTarget/MinSuits")]
public class DiceFace_MinTarget_SO : UI_DiceConditionalTargetBase_SO
{
	public override List<Dice_SuitType> FindTarget(Dice_Result result)
	{
		List<Dice_SuitType> res = new List<Dice_SuitType>();
		//只有四种花色 直接手撕也可以吧
		int min = 10;
		foreach (Dice_SuitType key in result.sum.Keys)
		{
			if (result.Get(key) < min && result.sum[key] > 0) min = result.Get(key);
		}

		foreach (Dice_SuitType key in result.sum.Keys)
		{
			if (result.Get(key) == min) res.Add(key);
		}
		
		return res;
	}
}
