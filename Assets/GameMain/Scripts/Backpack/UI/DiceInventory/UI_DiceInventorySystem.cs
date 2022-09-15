using System.Collections;
using System.Collections.Generic;
using GameKit.QuickCode;
using UnityEngine;

public class UI_DiceInventorySystem : UIFormBase
{
    public UI_DiceInventory uI_DiceInventory;
    public UI_DiceInventoryInfo uI_DiceInventoryInfo;

    [Space]
    public RectTransform diceContent;

    public UI_Dice dicePrefab;

    private int _currentIndex = -1;
    private List<UI_Dice> _uIDices = new List<UI_Dice>();

    private KeyCode _changeDisplayKeyCode;
    
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        InitDiceInventoryUI();
    }

    protected override void OnResume()
    {
        base.OnResume();
        InitDiceInventoryUI();
    }

    protected override void OnPause()
    {
        base.OnPause();
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

    private void InitDiceInventoryUI()
    {
        ClearDices();
        
        var data = GameKitCenter.Inventory.GetInventory(DiceInventory.current.Name);
        for (int i = 0; i < data.StockMap.Length; i++)
        {
            if(data.StockMap[i] == null) continue;
            var dice = Instantiate(dicePrefab, diceContent).OnInit((UI_DiceData_SO)data.StockMap[i].Data, i, null);
            _uIDices.Add(dice);
        }

        Select(0);
    }

    private void ClearDices()
    {
        foreach (Transform trans in diceContent.transform)
        {
            Destroy(trans.gameObject);
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
        if (index < 0 || index >= _uIDices.Count) return;

        if (_currentIndex >= 0 && _currentIndex < _uIDices.Count)
            _uIDices[_currentIndex].OnDisSelected();

        _uIDices[index].OnSelected();
        _currentIndex = index;
        UpdateDiceInventoryInfo(_uIDices[index].Data);
    }

    private void UpdateDiceInventoryInfo(UI_DiceData_SO data)
    {
        uI_DiceInventoryInfo.UpdateDiceInfoDesplay(data);
    }

    private bool GetKeyDown(KeyCode key)
    {
        return InputManager.instance.GetKeyDown(key);
    }
}