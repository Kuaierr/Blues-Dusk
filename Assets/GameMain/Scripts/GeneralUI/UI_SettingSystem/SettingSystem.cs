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
		Debug.Log("OpenSettingSystem");
		if (GameKitCenter.UI.GetUIForm(m_CachedUiId))
		{
			//GameKitCenter.UI.TryGetUIForm("UI_Backpack").Resume();
			//GameKitCenter.UI.GetUIForm(m_CachedUiId).OnResume();
			GameKitCenter.UI.RefocusUIForm(GameKitCenter.UI.GetUIForm(m_CachedUiId));
		}
        
		var uiForm = GameKitCenter.UI.TryOpenUIForm("UI_SettingSystem", userData: this);
		if (uiForm != null)
			m_CachedUiId = (int)uiForm;
	}
}
