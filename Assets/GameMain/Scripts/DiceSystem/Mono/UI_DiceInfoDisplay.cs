using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
		OnDepthChanged(2);
	}

	private void OnEnable()
	{
		onDiceMouseEnter.action += DisplayDiceInfo;
		onDiceMouseExit.action += HideDiceInfo;
	}

	private void OnDisable()
	{
		onDiceMouseEnter.action -= DisplayDiceInfo;
		onDiceMouseExit.action -= HideDiceInfo;
	}

	//TODO 后续应该会改为使用动画机控制，临时用Dotween凑合一下
	public void DisplayDiceInfo(UI_DiceData_SO data)
	{ // TODO 可能需要一些延时
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
		_canvas.DOKill();

		_canvas.DOFade(0, 0.5f);
	}
}
