using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameMenuSystem : UIFormBase
{
	public Button diceBackpackButton;
	public Button playerBackpackButton;
	public Button gameSettingButton;
	
	private KeyCode _changeDisplayKeyCode = KeyCode.None;
	private bool _firstInit = true;
	
	protected override void OnInit(object userData)
	{
		base.OnInit(userData);
		SetChangeKey(KeyCode.Escape);
	}

	public void SetChangeKey(KeyCode keyCode)
	{
		Debug.Log(keyCode);
		_changeDisplayKeyCode = keyCode;
	}

	protected override void OnOpen(object userData)
	{
		base.OnOpen(userData);
	}

	protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
	{
		base.OnUpdate(elapseSeconds, realElapseSeconds);
		ChangeDisplayUpdate(_changeDisplayKeyCode);
	}

	protected override void InternalSetVisible(bool visible)
	{
		if (_firstInit)
		{
			Visible = false;
			_firstInit = false;
		}
		else
			base.InternalSetVisible(visible);
	}
}
