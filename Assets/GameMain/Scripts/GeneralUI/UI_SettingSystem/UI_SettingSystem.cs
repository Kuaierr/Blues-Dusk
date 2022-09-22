using System;
using System.Collections;
using System.Collections.Generic;
using GameKit.QuickCode;
using UnityEngine;
using UnityEngine.UI;

public class UI_SettingSystem : MonoBehaviour
{
    public UI_SheetSystem ui_SheetSystem;

    private bool _isOnSheet = false;
    private UI_ConfigSheet _currentSheet;

    public ConfigData ConfigData { get; private set; }

    private void Start()
    {
        ConfigData = new ConfigData();
        ui_SheetSystem.OnInit(ConfigData);
        //OnConfirmKeyPeressed();
    }

    private void Update()
    {
        if (InputManager.instance.GetKeyDown(KeyCode.W))
            OnUpKeyPeressed();
        if (InputManager.instance.GetKeyDown(KeyCode.S))
            OnDownKeyPeressed();
        if (InputManager.instance.GetKeyDown(KeyCode.A))
            OnLeftKeyPeressed();
        if (InputManager.instance.GetKeyDown(KeyCode.D))
            OnRightKeyPeressed();
        if (InputManager.instance.GetKeyDown(KeyCode.Space))
            OnConfirmKeyPeressed();
        if (InputManager.instance.GetKeyDown(KeyCode.Escape))
            OnBackKeyPeressed();
    }

    #region KeybordInput

    private void OnUpKeyPeressed()
    {
        if (_isOnSheet)
            _currentSheet.OnUpKeyPeressed();
        else
            ui_SheetSystem.OnUpKeyPeressed();
    }

    private void OnDownKeyPeressed()
    {
        if (_isOnSheet)
            _currentSheet.OnDownKeyPeressed();
        else
            ui_SheetSystem.OnDownKeyPeressed();
    }

    private void OnLeftKeyPeressed()
    {
        if (_isOnSheet)
            _currentSheet.OnLeftKeyPeressed();
        else
            ui_SheetSystem.OnLeftKeyPeressed();
    }

    private void OnRightKeyPeressed()
    {
        if (_isOnSheet)
            _currentSheet.OnRightKeyPeressed();
        else
            ui_SheetSystem.OnRightKeyPeressed();
    }

    private void OnConfirmKeyPeressed()
    {
        if (!_isOnSheet)
        {
            _isOnSheet = true;
            _currentSheet = ui_SheetSystem.OnConfirmKeyPeressed();
            _currentSheet.Select(0);
        }
    }

    private void OnBackKeyPeressed()
    {
        if (_isOnSheet)
        {
            _isOnSheet = false;
            ui_SheetSystem.OnBackKeyPeressed();
            _currentSheet = null;
        }
    }

    #endregion
}