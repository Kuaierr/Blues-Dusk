using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UpdateInfo 在当前的调整下，该面板不再需要关闭/隐藏
/// </summary>
public class UI_DiceInfoDisplay : UIFormChildBase
{
	[SerializeField]
	private CanvasGroup _canvas;
	[Space]
	[SerializeField] 
	private TMP_Text _diceName;
	[SerializeField] 
	private TMP_Text _diceType;
	[Space]
	[SerializeField]
	private List<Image> _detailImages;

	[Space(15)]
	public DiceFaceEvent_SO onDiceMouseEnter;
	public NoParameterEvent_SO onDiceMouseExit;

	protected override void Start() {
		OnDepthChanged(10);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		onDiceMouseEnter.action += DisplayDiceInfo;
		onDiceMouseExit.action += HideDiceInfo;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		onDiceMouseEnter.action -= DisplayDiceInfo;
		onDiceMouseExit.action -= HideDiceInfo;
	}

	public void DisplayDiceInfo(UI_DiceData_SO data)
	{ 
		_canvas.DOComplete();

		_diceName.text = data.name;
		_diceType.text = data.type;

		for (int i = 0; i < 6; i++)
		{
			_detailImages[i].sprite = data.faceDatas[i].icon;
		}
		
		_canvas.DOFade(1, 0.5f);
	}

	public void HideDiceInfo()
	{
		//updateinfo 不再需要 但考虑到信息表现不应该生硬的切换，增加动效时需要用到Hide方法
		/*_canvas.DOKill();

		_canvas.DOFade(0, 0.5f);*/
	}
}
