using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractEvent_GetOneDice",menuName = "GameMain/InteractEvent/GetOneDice")]
public class GetDiceEvent_SO : ScriptableObject
{
	public void GetDice(int index)
	{
		UI_DiceDataPool_SO dataPool = (UI_DiceDataPool_SO)GameKitCenter.Data.Data_So[typeof(UI_DiceDataPool_SO)];
		
		GameKitCenter.Inventory.AddToInventory<UI_DiceData_SO>("DiceInventory", index, dataPool.diceData[index].name, dataPool.diceData[index]);
		
	}
}
