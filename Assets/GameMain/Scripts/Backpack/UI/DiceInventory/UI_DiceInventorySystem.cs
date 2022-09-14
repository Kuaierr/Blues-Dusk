using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DiceInventorySystem : UIFormBase
{
	public UI_DiceInventory uI_DiceInventory;
	public UI_DiceInventoryInfo uI_DiceInventoryInfo;

	[Space]
	public RectTransform diceContent;

	public void OnInitFromInventory()
	{
		
	}
	
	public void UpdateDiceInventoryInfo(UI_DiceData_SO data)
	{
		uI_DiceInventoryInfo.UpdateDiceInfoDesplay(data);
	}
}
