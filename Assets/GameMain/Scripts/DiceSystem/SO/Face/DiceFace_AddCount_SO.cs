using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//最为基础的点数面，优先级默认为最高。内部逻辑为加法
[CreateAssetMenu(fileName = "New AddFace",menuName = "GameMain/DiceData/DiceFace/AddCount")]
public class DiceFace_AddCount_SO : UI_DiceFaceBase_SO
{
	public override int Priority => -1;
	public override Dice_SuitType Type => type;
	public Dice_SuitType type;

	public int amount = 0;
	public override void Effect(Dice_Result result)
	{
		if (type == Dice_SuitType.SPECIAL) return;
		result.Add(Type,amount);
	}
}
