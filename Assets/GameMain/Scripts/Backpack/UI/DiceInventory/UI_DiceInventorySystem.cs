using System.Collections;
using System.Collections.Generic;
using GameKit.QuickCode;
using UnityEngine;

public class UI_DiceInventorySystem : UIFormBase
{
    public UI_DiceInventory uI_DiceInventory;
    public UI_DiceInventoryInfo uI_DiceInventoryInfo;

    private int _currentIndex = -1;

    private KeyCode _changeDisplayKeyCode;
    
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        uI_DiceInventory.InitDiceInventoryUI(Select);
        Select(0);
    }

    protected override void OnResume()
    {
        base.OnResume();
        uI_DiceInventory.InitDiceInventoryUI(Select);
        Select(0);
    }

    protected override void OnPause()
    {
        base.OnPause();
        uI_DiceInventory.ClearDices();

        ReFocusGameMenuEventArgs args = ReFocusGameMenuEventArgs.Create(this);
        GameKitCenter.Event.Fire(this, args);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        if (Visible)
        {
            KeybordControlling();
            ChangeDisplayUpdate(_changeDisplayKeyCode);
        }
    }

    
    
    public void SetChangeDisplayKeyCode(KeyCode key)
    {
        _changeDisplayKeyCode = key;
    }

    private void KeybordControlling()
    {
        if (GetKeyDown(KeyCode.W) || GetKeyDown(KeyCode.A))
            Select(_currentIndex - 1);
        if (GetKeyDown(KeyCode.S) || GetKeyDown(KeyCode.D))
            Select(_currentIndex + 1);
    }

    private void Select(int index)
    {
        var data = uI_DiceInventory.Select(ref _currentIndex,index);
        if(data != null)
            uI_DiceInventoryInfo.UpdateDiceInfoDesplay(data);
    }

    private bool GetKeyDown(KeyCode key)
    {
        return InputManager.instance.GetKeyDown(key);
    }
}