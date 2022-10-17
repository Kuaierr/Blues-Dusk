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
	private Animator _animator;
	[Space][Header("Basic Elements")]
	[SerializeField] 
	private TMP_Text _diceName;
	[SerializeField] 
	private TMP_Text _diceType;
	[Space]
	[SerializeField]
	private List<Image> _detailImages;
	
	[Space(15)][Header("Events")]
	public DiceFaceEvent_SO onDiceMouseEnter;
	public NoParameterEvent_SO onDiceMouseExit;

	protected override void Start() {
		OnDepthChanged(100);
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
		//_animator.SetTrigger("Show");
		_animator.Play("SHOW");
		
		_diceName.text = data.DiceName;
		_diceType.text = data.type;

		for (int i = 0; i < 6; i++)
			_detailImages[i].sprite = data.faceDatas[i].icon;
	}

	public void HideDiceInfo()
	{
		_animator.Play("HIDE");
	}
}
