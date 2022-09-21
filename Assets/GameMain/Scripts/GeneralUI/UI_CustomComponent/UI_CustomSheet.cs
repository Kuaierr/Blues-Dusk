using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UI_CustomSheet : UI_CustomBase
{
	private CanvasGroup _canvas;
	
	[SerializeField]
	private UI_ConfigTag _configTag;
	
	public void OnInit(List<CustomConfigOptionSet> optionList)
	{
		_canvas = GetComponent<CanvasGroup>();

		foreach (Transform child in transform)
			Destroy(child.gameObject);
		
		foreach (CustomConfigOptionSet option in optionList)
			InitConfigTag(option);
		
	}

	private void InitConfigTag(CustomConfigOptionSet optionSet)
	{
		Instantiate(_configTag, this.transform).OnInit(optionSet);
	}
	
	public void OnOpen()
	{
		_canvas.alpha = 1;
		_canvas.blocksRaycasts = true;
		_canvas.interactable = true;
	}

	public void OnClose()
	{
		_canvas.alpha = 0;
		_canvas.blocksRaycasts = false;
		_canvas.interactable = false;
	}
}
