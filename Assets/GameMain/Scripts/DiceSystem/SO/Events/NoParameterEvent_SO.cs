using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New UnityAction",menuName = "GameMain/EventAsset/UnityAction")]
public class NoParameterEvent_SO : ScriptableObject
{
	public event UnityAction action;

	public void Raise()
	{
		action?.Invoke();
	}
}
