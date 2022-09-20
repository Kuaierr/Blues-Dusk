using System;
using System.Collections;
using System.Collections.Generic;
using GameKit.QuickCode;
using UnityEngine;

public class UI_SheetSystem : MonoBehaviour
{
	[SerializeField] private List<UI_CustomButton> _customButtons = new List<UI_CustomButton>();
	[SerializeField] private List<UI_CustomSheet> _customSheets = new List<UI_CustomSheet>();

	private int _currentIndex = 0;

	private void Start()
	{
		OnInit();
	}

	public void OnInit()
	{
		if(_customButtons.Count != _customSheets.Count)
			Debug.LogError("Buttons and Sheets do not Match");
		for (int i = 0; i < _customButtons.Count; i++)
			_customButtons[i].OnInit(_customSheets[i].OnOpen);

		Select(0);
	}

	/*private void Update()
	{
		if(InputManager.instance.GetKeyDown(KeyCode.W))
			Debug.Log("上移");
		if(InputManager.instance.GetKeyDown(KeyCode.S))
			Debug.Log("下移");
		if(InputManager.instance.GetKeyDown(KeyCode.A))
			Debug.Log("切换设置选项");
		if(InputManager.instance.GetKeyDown(KeyCode.D))
			Debug.Log("切换设置选项");
		if(InputManager.instance.GetKeyDown(KeyCode.Space))
			Debug.Log("确认按钮");
		if(InputManager.instance.GetKeyDown(KeyCode.Escape))
			Debug.Log("返回");
	}*/

	private void Select(int index)
	{
		if(_currentIndex>=0 && _currentIndex<_customButtons.Count)
		{
			_customButtons[_currentIndex].OnReleased();
			_customSheets[_currentIndex].OnClose();
		}
		
		if(index>=0 && index<_customButtons.Count)
			_customButtons[index].OnClicked();
	}

	/*#region KeybordInput

	private void UpKeyPeressed()
	{
		
	}

	#endregion*/
}
