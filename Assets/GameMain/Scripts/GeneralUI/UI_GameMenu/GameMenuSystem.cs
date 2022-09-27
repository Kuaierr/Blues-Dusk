using System;
using System.Collections;
using System.Collections.Generic;
using GameKit.Event;
using GameKit.QuickCode;
using UnityEngine;
using UnityGameKit.Runtime;

public class GameMenuSystem : MonoSingletonBase<GameMenuSystem>
{
	private int m_CachedUiId;

	private void Start()
	{
		GameKitCenter.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnGameMenuUIOpenSuccess);
		GameKitCenter.Event.Subscribe(ReFocusGameMenuEventArgs.EventId,OpenGameMenuUIForm);
		//GameKitCenter.UI.TryOpenUIForm("UI_GameMenu", this);
		OpenGameMenuUIForm(this, null);

	}

	private void OpenGameMenuUIForm(object sender, GameEventArgs e)
	{
		var uiFormInstance = GameKitCenter.UI.GetUIForm(m_CachedUiId);
		if (uiFormInstance != null)
		{
			GameKitCenter.UI.RefocusUIForm(uiFormInstance);
		}
		else
		{
			var uiForm = GameKitCenter.UI.TryOpenUIForm("UI_GameMenu",this);
			if(uiForm != null)
				m_CachedUiId = (int)uiForm;
		}
	}

	/*private void Update()
	{
		if (InputManager.instance.GetKeyDown(KeyCode.Escape) || InputManager.instance.GetMouseButtonDown(1))
			GameKitCenter.UI.TryOpenUIForm("UI_GameMenu", this);
	}*/

	private void OnGameMenuUIOpenSuccess(object sender, GameKit.Event.GameEventArgs e)
	{
		OpenUIFormSuccessEventArgs args = (OpenUIFormSuccessEventArgs)e;
		if (args.UserData == null || args.UserData.GetType() != typeof(GameMenuSystem))
			return;

		if (args.UIForm == null)
		{
			Log.Fail("Loaded GameMenu UI Form  is null");
			return;
		}

		UI_GameMenuSystem uI_GameMenu = (UI_GameMenuSystem)args.UIForm.Logic;
		uI_GameMenu.SetChangeKey(KeyCode.Tab);
	}

	/*private void OnPlayerBackpackButtonPressed(object sender, GameKit.Event.GameEventArgs e)
	{
		
	}*/
}
