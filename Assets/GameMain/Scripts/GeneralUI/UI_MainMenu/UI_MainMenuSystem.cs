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
        GetComponentInParent<Canvas>().worldCamera = Camera.current;
    
        //TODO 如果没有存档，隐藏ContinueButton
        _continueButton.onClick.AddListener(ContinueLastGame);
        _newGameButton.onClick.AddListener(NewGame);
        _loadGameButton.onClick.AddListener(LoadGame);
        _settingButton.onClick.AddListener(OpenSettingPanel);

        saveDataArea.SetActive(false);

        RefreshContinueButton();
        if (!PlayerPrefs.HasKey(CurrentSaveDataKey))
            PlayerPrefs.SetInt(CurrentSaveDataKey, 00);
    }

    private void Update()
    {
        if (InputManager.instance.GetKeyDown(KeyCode.Escape))
        {
            saveDataArea.SetActive(false);
            RefreshContinueButton();
        }
    }

    private void ContinueLastGame()
    {
        int index = PlayerPrefs.GetInt(CurrentSaveDataKey);
        LoadData(index);
    }

    private void NewGame()
    {
        int currentIndex = -1;
        if (PlayerPrefs.HasKey(CurrentSaveDataKey))
            currentIndex = PlayerPrefs.GetInt(CurrentSaveDataKey);
        for (int i = 0; i < saveDataSlots.Count; i++)
            saveDataSlots[i].OnInit(i + 1, OpenUITip);
        if (currentIndex != -1)
            PlayerPrefs.SetInt(CurrentSaveDataKey, currentIndex);

        saveDataArea.SetActive(true);

        void OpenUITip(int index)
        {
            GeneralSystem.current.OpenTipUI("从这里开始吗？", () => { StartNewData(index); });
        }
    }

    private void LoadGame()
    {
        int currentIndex = -1;
        if (PlayerPrefs.HasKey(CurrentSaveDataKey))
            currentIndex = PlayerPrefs.GetInt(CurrentSaveDataKey);
        for (int i = 0; i < saveDataSlots.Count; i++)
            saveDataSlots[i].OnInit(i + 1, OpenUITip);
        if (currentIndex != -1)
            PlayerPrefs.SetInt(CurrentSaveDataKey, currentIndex);

        saveDataArea.SetActive(true);

        void OpenUITip(int index)
        {
            GeneralSystem.current.OpenTipUI("读取这个进度吗？", () => { LoadData(index); });
        }
    }

    private void OpenSettingPanel()
    {
        OpenSettingUIEventArgs args = OpenSettingUIEventArgs.Create(this);
        GameKitCenter.Event.Fire(this, args);
    }

    private void RefreshContinueButton()
    {
        if (PlayerPrefs.HasKey(CurrentSaveDataKey) && PlayerPrefs.GetInt(CurrentSaveDataKey) != 0)
            _continueButton.gameObject.SetActive(true);
        else
            _continueButton.gameObject.SetActive(false);
    }

    public void StartNewData(int index)
    {
        PlayerPrefs.SetInt(CurrentSaveDataKey, index);
        //TODO 通过UI_Tip确认
        //如果有数据，就删除
        if (GameKitCenter.Setting.Load())
            GameKitCenter.Setting.RemoveAllSettings();
        //开始新游戏
        GameKitCenter.Setting.Save();
        GameKitCenter.Procedure.ChangeSceneBySelect(StartSceneName);
    }

    public void LoadData(int index)
    {
        PlayerPrefs.SetInt(CurrentSaveDataKey, index);
        //如果有数据，则读取
        if (!GameKitCenter.Setting.Load())
            return;
        else
        {
            //TODO 读取上次存档时的场景和位置信息，加载场景
            Debug.Log("Load GameData");
            GameKitCenter.Procedure.ChangeSceneBySelect(StartSceneName);
        }
    }
}