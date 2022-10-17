using System;
using System.Collections;
using System.Collections.Generic;
using GameKit.QuickCode;
using UnityEngine;
using UnityEngine.UI;

public class UI_ConfigSystem : UIFormBase
{
    public UI_SheetSystem ui_SheetSystem;

    private bool _isOnSheet = false;
    private UI_ConfigSheet _currentSheet;

    public ConfigData ConfigData { get; private set; }

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);

        ConfigData = new ConfigData();
        ui_SheetSystem.OnInit(ConfigData);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        KeybordControlling();
    }

    public void ResetConfigData()
    {
        ConfigData = new ConfigData();
        ui_SheetSystem.OnInit(ConfigData);
    }

    #region KeybordInput

    private void KeybordControlling()
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
        else
        {
            Visible = false;
            ReFocusGameMenuEventArgs args = ReFocusGameMenuEventArgs.Create(this);
            GameKitCenter.Event.Fire(this, args);
        }
    }

    #endregion

    protected override void OnPause()
    {
        base.OnPause();
    }
}