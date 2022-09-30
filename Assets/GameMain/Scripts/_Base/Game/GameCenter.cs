using GameKit.Event;
using GameKit;
using UnityEngine;
using UnityGameKit.Runtime;

public class GameCenter : MonoSingletonBase<GameCenter>
{
    public int CurrentDay;
    public int CurrentStage;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        GameKitCenter.Event.Subscribe(LoadSettingsEventArgs.EventId, OnLoad);
        GameKitCenter.Event.Subscribe(SaveSettingsEventArgs.EventId, OnSave);
        GameKitCenter.Event.Subscribe(MoveToNextDayEventArgs.EventId, OnMoveToNextDay);
        GameKitCenter.Event.Subscribe(MoveToNextStageEventArgs.EventId, OnMoveToNextStage);
    }

    public void OnLoad(object sender, GameEventArgs e)
    {
        Log.Success("GameCenter Loaded");
        CurrentDay = GameKitCenter.Setting.GetInt("GameSettings.CurrentDay", 1);
        CurrentStage = GameKitCenter.Setting.GetInt("GameSettings.CurrentStage", 3);
        // Log.Success(CurrentDay);
        // Log.Success(CurrentStage);
    }

    public void OnSave(object sender, GameEventArgs e)
    {
        Log.Success("GameCenter Saved");
        GameKitCenter.Setting.SetInt("GameSettings.CurrentDay", CurrentDay);
        GameKitCenter.Setting.SetInt("GameSettings.CurrentStage", CurrentStage);
    }

    public void OnMoveToNextDay(object sender, GameEventArgs e)
    {
        CurrentDay += 1;
        CurrentStage = 1;
    }

    public void OnMoveToNextStage(object sender, GameEventArgs e)
    {
        CurrentStage += 1;
    }
}