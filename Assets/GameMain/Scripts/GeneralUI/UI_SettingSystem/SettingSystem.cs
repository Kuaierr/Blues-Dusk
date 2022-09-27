using System;
using System.Collections;
using System.Collections.Generic;
using GameKit.Event;
using UnityEngine;
using UnityGameKit.Runtime;

public class SettingSystem : MonoSingletonBase<SettingSystem>
{	
	private int m_CachedUiId;

	private void Start()
	{
		GameKitCenter.Event.Subscribe(OpenSettingUIEventArgs.EventId,OpenSettingUI);
	}

	private void OpenSettingUI(object sender, GameEventArgs e)
	{
		UIForm uiFormInstanse = GameKitCenter.UI.GetUIForm(m_CachedUiId);
		if(uiFormInstanse != null)
		{
			//GameKitCenter.UI.RefocusUIForm(uiFormInstanse);
			GameKitCenter.UI.RefocusUIForm(uiFormInstanse);
			uiFormInstanse.OnResume();
		}
		else
		{
			var uiForm = GameKitCenter.UI.TryOpenUIForm("UI_SettingSystem", this);
			if (uiForm != null)
				m_CachedUiId = (int)uiForm;
		}
	}
}
