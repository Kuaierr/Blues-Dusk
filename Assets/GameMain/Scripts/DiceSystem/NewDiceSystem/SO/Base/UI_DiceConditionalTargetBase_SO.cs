using System.Collections.Generic;
using UnityEngine;

public abstract class UI_DiceConditionalTargetBase_SO : ScriptableObject
{
	public abstract List<Dice_SuitType> FindTarget(Dice_Result result);
}
