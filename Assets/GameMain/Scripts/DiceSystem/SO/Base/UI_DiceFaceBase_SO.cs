using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_DiceFaceBase_SO : ScriptableObject
{
	public new string name;
	public Sprite icon;
	[TextArea]
	public string description;

	public abstract int Priority { get; }

	public abstract Dice_SuitType Type { get; }

	public abstract void Effect(Dice_Result result);
}
