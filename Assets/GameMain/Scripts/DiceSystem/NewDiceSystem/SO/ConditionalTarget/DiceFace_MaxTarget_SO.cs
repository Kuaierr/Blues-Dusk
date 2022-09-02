using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Max Suits", menuName = "GameMain/DiceData/ConditionalTarget/MaxSuits")]
public class DiceFace_MaxTarget_SO : UI_DiceConditionalTargetBase_SO
{
	public override List<Dice_SuitType> FindTarget(Dice_Result result)
	{
		List<Dice_SuitType> res = new List<Dice_SuitType>();
		//只有四种花色 直接手撕也可以吧
		int max = -1;
		foreach (Dice_SuitType key in result.sum.Keys)
		{
			if (result.Get(key) > max) max = result.Get(key);
		}

		foreach (Dice_SuitType key in result.sum.Keys)
		{
			if (result.Get(key) == max) res.Add(key);
		}
		
		return res;
	}
}
