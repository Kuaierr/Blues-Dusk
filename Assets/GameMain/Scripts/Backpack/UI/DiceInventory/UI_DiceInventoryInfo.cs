using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_DiceInventoryInfo : UIFormChildBase
{
	public TMP_Text diceName;
	public TMP_Text diceType;
	public TMP_Text diceDescription;
	public UI_Dice diceModel;

	public List<Image> diceFaceIcons = new List<Image>();
	public List<TMP_Text> diceFaceDetails = new List<TMP_Text>();

	public void UpdateDiceInfoDesplay(UI_DiceData_SO data)
	{
		diceName.text = data.DiceName;
		diceType.text = data.type;
		diceDescription.text = data.description;

		diceModel.OnInit(data, -1, null);

		for (int i = 0; i < 6; i++)
		{
			diceFaceIcons[i].sprite = data.faceDatas[i].icon;
			diceFaceDetails[i].text = data.faceDatas[i].description;
		}
	}
}
