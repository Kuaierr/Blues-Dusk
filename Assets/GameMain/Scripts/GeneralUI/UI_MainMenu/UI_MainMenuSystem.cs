using System;
using System.Collections.Generic;
using GameKit.QuickCode;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenuSystem : MonoBehaviour
{
    public Button _continueButton;
    public Button _newGameButton;
    public Button _loadGameButton;

    public Button _settingButton;
    public Button _quitButton;

    public GameObject saveDataArea;
    public List<UI_SaveDataSlot> saveDataSlots = new List<UI_SaveDataSlot>();

    [Space]
    [Scene]
    public string StartSceneName;

    public static string CurrentSaveDataKey = "CurrentSaveDataIndex";

    private void OnEnable()
    {
        //TODO 如果没有存档，隐藏ContinueButton
        _continueButton.onClick.AddListener(ContinueLastGame);
        _newGameButton.onClick.AddListener(NewGame);
        _loadGameButton.onClick.AddListener(LoadGame);
        _settingButton.onClick.AddListener(OpenSettingPanel);
        
        saveDataArea.SetActive(false);
        _continueButton.gameObject.SetActive(PlayerPrefs.HasKey(CurrentSaveDataKey));
        
        if(PlayerPrefs.HasKey(CurrentSaveDataKey))
            PlayerPrefs.SetInt(CurrentSaveDataKey,-1);
    }

    private void Update()
    {
        if(InputManager.instance.GetKeyDown(KeyCode.Escape))
            saveDataArea.SetActive(false);
    }

    private void ContinueLastGame()
    {
        int index = PlayerPrefs.GetInt(CurrentSaveDataKey);
    }

    private void NewGame()
    {
        for (int i = 0; i < saveDataSlots.Count; i++)
            saveDataSlots[i].OnInit(i, StartNewData);

        saveDataArea.SetActive(true);
    }

    private void LoadGame()
    {
        for (int i = 0; i < saveDataSlots.Count; i++)
            saveDataSlots[i].OnInit(i, LoadData);
        
        saveDataArea.SetActive(true);
    }

    private void OpenSettingPanel()
    {
        OpenSettingUIEventArgs args = OpenSettingUIEventArgs.Create(this);
        GameKitCenter.Event.Fire(this, args);
    }

    public void StartNewData(int index)
    {
        PlayerPrefs.SetInt(CurrentSaveDataKey, index);
        //TODO 通过UI_Tip确认
        //如果有数据，就删除
        if(GameKitCenter.Setting.Load())
            GameKitCenter.Setting.RemoveAllSettings();
        //开始新游戏
        GameKitCenter.Setting.Save();
        GameKitCenter.Procedure.ChangeSceneBySelect(StartSceneName);
    }

    public void LoadData(int index)
    {
        PlayerPrefs.SetInt(CurrentSaveDataKey, index);
        //如果有数据，则读取
        if(!GameKitCenter.Setting.Load())
            return;
        else 
            Debug.Log("");
            
        
    }
}