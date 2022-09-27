using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using GameKit.Event;
using GameKit;
using UnityGameKit.Runtime;

public class GameSettings : MonoSingletonBase<GameSettings>
{
    private const string GamePrefix = "GameSettings";
    private const string ScenePrefix = "SceneSettings";
    private void Start()
    {
        GameKitCenter.Event.Subscribe(LoadSettingsEventArgs.EventId, OnLoad);
    }

    public void OnLoad(object sender, GameEventArgs e)
    {
        Log.Success("GameSettings Loaded");
        bool hasGameSettings = GameKitCenter.Setting.GetBool("GameSettings", false);
        // 如果之前没有存档，从默认配置中生成存档
        if (!hasGameSettings)
        {
            GameKitCenter.Setting.SetBool("GameSettings", true);
            string[] configs = GameKitCenter.Data.GameConfigTable.Data.ToString().RemoveBrackets().RemoveEmptySpaceLine().RemoveLast().Split(',');
            for (int i = 0; i < configs.Length; i++)
            {
                string[] pair = configs[i].Split(':');
                string configName = pair[0];
                string configValue = pair[1];
                GameKitCenter.Setting.SetString(string.Format("{0}.{1}", GamePrefix, configName), configValue);
            }

            foreach (var config in GameKitCenter.Data.SceneConfigTable.DataList)
            {
                string configName = config.SceneAssetName.Correction();
                string configValue = config.IsUnlocked.Correction();
                Log.Warning(string.Format("{0}.{1}", ScenePrefix, configName) + " >> " + configValue);
                GameKitCenter.Setting.SetString(string.Format("{0}.{1}", ScenePrefix, configName), configValue);
            }
        }
    }

    public bool GetSceneState(string sceneName)
    {
        return GameKitCenter.Setting.GetBool(string.Format("{0}.{1}", ScenePrefix, sceneName));
    }

    public void SetSceneState(string sceneName, bool value)
    {
        GameKitCenter.Setting.SetBool(string.Format("{0}.{1}", ScenePrefix, sceneName), value);
    }

    public bool GetBool(string settingName)
    {
        // Log.Warning(string.Format("Get Bool {0}.{1}", GamePrefix, settingName) + " >> " + GameKitCenter.Setting.GetBool(string.Format("{0}.{1}", GamePrefix, settingName)));
        return GameKitCenter.Setting.GetBool(string.Format("{0}.{1}", GamePrefix, settingName));
    }
    public int GetInt(string settingName)
    {
        return GameKitCenter.Setting.GetInt(string.Format("{0}.{1}", GamePrefix, settingName));
    }
    public float GetFloat(string settingName)
    {
        return GameKitCenter.Setting.GetFloat(string.Format("{0}.{1}", GamePrefix, settingName));
    }
    public string GetString(string settingName)
    {
        return GameKitCenter.Setting.GetString(string.Format("{0}.{1}", GamePrefix, settingName));
    }

    public void SetBool(string settingName, bool value)
    {
        // Log.Warning(string.Format("Set Bool {0}.{1}", GamePrefix, settingName) + " >> " + value);
        GameKitCenter.Setting.SetBool(string.Format("{0}.{1}", GamePrefix, settingName), value);
    }
    public void SetInt(string settingName, int value)
    {
        GameKitCenter.Setting.SetInt(string.Format("{0}.{1}", GamePrefix, settingName), value);
    }
    public void SetFloat(string settingName, float value)
    {
        GameKitCenter.Setting.SetFloat(string.Format("{0}.{1}", GamePrefix, settingName), value);
    }
    public void SetString(string settingName, string value)
    {
        GameKitCenter.Setting.SetString(string.Format("{0}.{1}", GamePrefix, settingName), value);
    }
}