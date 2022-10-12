using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InterEvent_GameSetting",menuName = "GameMain/InteractEvent/ForGameSetting")]
public class GameSettingEvent_SO : ScriptableObject
{
	public void EnableGameSettingBool(string settingName)
	{
		GameKitCenter.Setting.SetBool(settingName,true);
	}

	public void DisableGameSettingBool(string settingName)
	{
		GameKitCenter.Setting.SetBool(settingName,false);
	}

	public void Debug(string content)
	{
		UnityEngine.Debug.Log("Debugger : \n" + content);
	}
}
