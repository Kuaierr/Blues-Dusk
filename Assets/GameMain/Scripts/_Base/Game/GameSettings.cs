using System.Collections.Generic;
using GameKit.Event;
using UnityGameKit.Runtime;

public class GameSettings : MonoSingletonBase<GameSettings>
{
    public LubanConfig.DataTable.GameConfig RuntimeGameConfig;
    private void Start()
    {
        GameKitCenter.Event.Subscribe(SaveSettingsEventArgs.EventId, OnSave);
        GameKitCenter.Event.Subscribe(LoadSettingsEventArgs.EventId, OnLoad);
    }

    public void OnLoad(object sender, GameEventArgs e)
    {
        // bool b_active = GameKitCenter.Setting.GetBool(string.Format("{0}({1})", Name, "Is Active"), true);
    }

    public void OnSave(object sender, GameEventArgs e)
    {
        // GameKitCenter.Setting.SetBool(string.Format("{0}({1})", Name, "Is Active"), gameObject.activeSelf);
    }


    private void InitRuntimeSetting()
    {
        RuntimeGameConfig = (LubanConfig.DataTable.GameConfig)GameKitCenter.Data.GameConfigTable.Data.Clone();
    }
}