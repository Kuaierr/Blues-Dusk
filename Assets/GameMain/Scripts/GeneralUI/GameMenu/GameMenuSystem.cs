using System;
using System.Collections;
using System.Collections.Generic;
using GameKit.QuickCode;
using UnityEngine;
using UnityGameKit.Runtime;

public class GameMenuSystem : MonoSingletonBase<GameMenuSystem>
{
	private void Start()
	{
		GameKitCenter.UI.TryOpenUIForm("UI_GameMenu", this);
	}

	/*private void Update()
	{
		if (InputManager.instance.GetKeyDown(KeyCode.Escape) || InputManager.instance.GetMouseButtonDown(1))
			GameKitCenter.UI.TryOpenUIForm("UI_GameMenu", this);
	}*/

	/*private void OnGameMenuUIOpenSuccess(object sender, GameKit.Event.GameEventArgs e)
	{
		OpenUIFormSuccessEventArgs args = (OpenUIFormSuccessEventArgs)e;
		if (args.UserData == null || args.UserData.GetType() != typeof(PlayerBackpack))
			return;

		if (args.UIForm == null)
		{
			Log.Fail("Loaded GameMenu UI Form  is null");
			return;
		}

		UI_GameMenuSystem uI_GameMenu = (UI_GameMenuSystem)args.UIForm.Logic;
		uI_GameMenu.SetChangeKey(KeyCode.Escape);
	}*/

	/*private void OnPlayerBackpackButtonPressed(object sender, GameKit.Event.GameEventArgs e)
	{
		
	}*/
}
