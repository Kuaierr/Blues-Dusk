using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InterEvent_GameElementEvent",menuName = "GameMain/InteractEvent/ForGameElementEvent")]
public class GameElementEvent_SO : ScriptableObject
{
	public void SetActiveFalse(string target)
	{
		GameElementBase element = GameObject.Find(target).GetComponent<GameElementBase>();
		if (element != null)
		{
			element.Disable();
		}
		else
		{
			Debug.LogError("Have Not Found Target Element");
		}
	}
}
