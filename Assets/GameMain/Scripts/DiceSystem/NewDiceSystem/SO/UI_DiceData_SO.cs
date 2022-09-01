using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO 后续应该会接入用excel的配置，因为不清楚背包道具的结构，先临时使用
[CreateAssetMenu(fileName = "New Dice",menuName = "GameMain/DiceData/DiceAsset")]
public class UI_DiceData_SO : ScriptableObject
{
	public new string name;
	public string type;

	[Space]
	public List<UI_DiceFace_SO> faceDatas = new List<UI_DiceFace_SO>();

	/*public UI_DiceFace_SO up;
	public UI_DiceFace_SO down;
	public UI_DiceFace_SO right;
	public UI_DiceFace_SO left;
	public UI_DiceFace_SO forward;
	public UI_DiceFace_SO back;*/
}
