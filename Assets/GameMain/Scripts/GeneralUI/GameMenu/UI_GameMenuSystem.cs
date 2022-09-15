using System.Collections;
using System.Collections.Generic;
using GameKit.QuickCode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_GameMenuSystem : UIFormBase
{
    /*public Button diceBackpackButton;
    public Button playerBackpackButton;
    public Button gameSettingButton;*/

    public List<UI_CustomButton> buttons = new List<UI_CustomButton>();

    private int _currentIndex = -1;
    private bool _firstInit = true;
    private KeyCode _changeDisplayKeyCode = KeyCode.None;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        SetChangeKey(KeyCode.Tab);

        InitGameMenuButtons();
    }

    public void InitGameMenuButtons()
    {
        buttons[0].OnInit(OnDiceInventoryButtonPressed);
        buttons[1].OnInit(OnPlayerBackpackButtonPressed);
        //buttons[2].OnInit(settingCallback);
    }

    private void KeybordControlling()
    {
        if (InputManager.instance.GetKeyDown(KeyCode.A))
            Select(_currentIndex - 1);
        if (InputManager.instance.GetKeyDown(KeyCode.D))
            Select(_currentIndex + 1);
        if (InputManager.instance.GetKeyDown(KeyCode.Space))
            buttons[_currentIndex].OnClicked();
    }

    public void SetChangeKey(KeyCode keyCode)
    {
        _changeDisplayKeyCode = keyCode;
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        Select(0);
    }

    protected override void OnPause()
    {
        base.OnPause();
    }

    protected override void OnResume()
    {
        base.OnResume();
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        ChangeDisplayUpdate(_changeDisplayKeyCode);
        if(Visible)
        {
            KeybordControlling();
        }
    }

    protected override void InternalSetVisible(bool visible)
    {
        if (_firstInit)
        {
            Visible = false;
            _firstInit = false;
        }
        else
            base.InternalSetVisible(visible);
    }

    private void Select(int index)
    {
        if (_currentIndex >= 0)
            buttons[_currentIndex].OnReleased();
        if (index >= buttons.Count)
            index = 0;
        else if (index < 0)
            index = buttons.Count - 1;

        //Debug.Log(index);
        buttons[index].OnSelected();
        _currentIndex = index;
    }

    private void OnPlayerBackpackButtonPressed()
    {
        OnOpenPlayerBackpackEventArgs args = OnOpenPlayerBackpackEventArgs.Create(this);
        GameKitCenter.Event.Fire(OnOpenPlayerBackpackEventArgs.EnentId, args);
        
        OnPause();
    }

    private void OnDiceInventoryButtonPressed()
    {
        OnOpenDiceInventoryEventArgs args = OnOpenDiceInventoryEventArgs.Create(this);
        GameKitCenter.Event.Fire(OnOpenDiceInventoryEventArgs.EventId, args);
        
        OnPause();
    }
}