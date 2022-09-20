using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CustomSheet : UI_CustomBase
{
	[SerializeField] private CanvasGroup _canvas;
	
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
