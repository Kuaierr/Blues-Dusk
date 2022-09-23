using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenuSystem : MonoBehaviour
{
    public Button _continueButton;
    public Button _newGameButton;
    public Button _loadGameButton;
    
    public Button _settingButton;
    public Button _quitButton;

    [Space]
    [UnityGameKit.Editor.Scene] public string targetSceneName;
    
    private void Start()
    {
        _newGameButton.onClick.AddListener(StartNewGame);
        _settingButton.onClick.AddListener(OpenSettingPanel);
    }

    private void StartNewGame()
    {
        GameKitCenter.Procedure.ChangeSceneBySelect(targetSceneName);
    }

    private void OpenSettingPanel()
    {
        OpenSettingUIEventArgs args = OpenSettingUIEventArgs.Create(this);
        GameKitCenter.Event.Fire(this, args);
    }
}