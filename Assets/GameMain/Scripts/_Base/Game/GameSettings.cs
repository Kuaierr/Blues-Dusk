using System.Collections.Generic;
using GameKit.Event;
using GameKit;
using UnityGameKit.Runtime;

public class GameSettings : MonoSingletonBase<GameSettings>
{
    private const string Prefix = "GameSettings.";
    private void Start()
    {
        GameKitCenter.Event.Subscribe(LoadSettingsEventArgs.EventId, OnLoad);
    }

    public void OnLoad(object sender, BaseEventArgs e)
    {
        Log.Success("GameSettings Loaded");
        bool hasGameSettings = GameKitCenter.Setting.GetBool("GameSettings", false);
        if (!hasGameSettings)
        {
            GameKitCenter.Setting.SetBool("GameSettings", true);
            string[] configs = GameKitCenter.Data.GameConfigTable.Data.ToString().RemoveBrackets().RemoveEmptySpaceLine().RemoveLast().Split(',');
            for (int i = 0; i < configs.Length; i++)
            {
                string[] pair = configs[i].Split(':');
                string configName = pair[0];
                string configValue = pair[1];
                GameKitCenter.Setting.SetString(string.Format("{0}.{1}", Prefix, configName), configValue);
            }
        }
    }

    public bool GetBool(string settingName)
    {
        return GameKitCenter.Setting.GetBool(string.Format("{0}.{1}", Prefix, settingName));
    }
    public int GetInt(string settingName)
    {
        return GameKitCenter.Setting.GetInt(string.Format("{0}.{1}", Prefix, settingName));
    }
    public float GetFloat(string settingName)
    {
        return GameKitCenter.Setting.GetFloat(string.Format("{0}.{1}", Prefix, settingName));
    }
    public string GetString(string settingName)
    {
        return GameKitCenter.Setting.GetString(string.Format("{0}.{1}", Prefix, settingName));
    }

    public void SetBool(string settingName, bool value)
    {
        GameKitCenter.Setting.SetBool(string.Format("{0}.{1}", Prefix, settingName), value);
    }
    public void SetInt(string settingName, int value)
    {
        GameKitCenter.Setting.SetInt(string.Format("{0}.{1}", Prefix, settingName), value);
    }
    public void SetFloat(string settingName, float value)
    {
        GameKitCenter.Setting.SetFloat(string.Format("{0}.{1}", Prefix, settingName), value);
    }
    public void SetString(string settingName, string value)
    {
        GameKitCenter.Setting.SetString(string.Format("{0}.{1}", Prefix, settingName), value);
    }
}