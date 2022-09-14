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

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);

        var data = GameKitCenter.Inventory.GetInventory(DiceInventory.current.Name);
        for (int i = 0; i < data.Count; i++)
        {
            var dice = Instantiate(dicePrefab, diceContent).OnInit((UI_DiceData_SO)data.StockMap[i].Data, i, null);
            _uIDices.Add(dice);
        }
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        StartCoroutine("KeybordControlling");
    }

    protected override void OnResume()
    {
        base.OnResume();
        StartCoroutine("KeybordControlling");
    }

    protected override void OnPause()
    {
        base.OnPause();
        StopCoroutine("KeybordControlling");
    }

    private IEnumerator KeybordControlling()
    {
        yield return 0;
        Select(0);
        while (true)
        {
            if (GetKeyDown(KeyCode.W) || GetKeyDown(KeyCode.A))
                Select(_currentIndex - 1);
            if (GetKeyDown(KeyCode.S) || GetKeyDown(KeyCode.D))
                Select(_currentIndex + 1);
            yield return 0;
        }
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