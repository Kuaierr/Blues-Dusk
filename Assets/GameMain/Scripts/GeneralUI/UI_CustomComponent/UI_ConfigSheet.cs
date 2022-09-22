using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UI_ConfigSheet : UI_CustomBase
{
	private CanvasGroup _canvas;
	private List<UI_ConfigTag> _tags = new List<UI_ConfigTag>();
	private int _currentIndex;

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

	private UI_ConfigTag InitConfigTag(CustomConfigOptionSet optionSet)
	{
		return Instantiate(_configTag, this.transform).OnInit(optionSet);
	}
	
	public void OnOpen()
	{
		_canvas.alpha = 1;
		_canvas.blocksRaycasts = true;
		_canvas.interactable = true;
		
		Select(0);
	}

	public void OnClose()
	{
		_canvas.alpha = 0;
		_canvas.blocksRaycasts = false;
		_canvas.interactable = false;
	}

	public void Select(int index)
	{
		if(_currentIndex>=0 && _currentIndex<_tags.Count)
			_tags[index].OnReleased();
		if(index>=0 && _currentIndex<_tags.Count)
		{
			_tags[index].OnSelected();
			_currentIndex = index;
		}	
	}
}
