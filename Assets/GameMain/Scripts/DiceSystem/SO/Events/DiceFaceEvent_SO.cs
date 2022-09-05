using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UnityAction(UIDiceDataSO)",menuName = "GameMain/EventAsset/UnityAction(UIDiceDataSO)")]
public class DiceFaceEvent_SO : ScriptableObject
{
	public event UnityAction<UI_DiceData_SO> action;

	public void Raise(UI_DiceData_SO data)
	{
		action?.Invoke(data);
	}
}
