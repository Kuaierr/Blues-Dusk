using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DiceInventory : UIFormChildBase
{
    [SerializeField]
    private int _basicOffset = 80;
    [SerializeField]
    private int _diceOffset = 175;
    [Space]
    [SerializeField]
    private ScrollRect _scrollRect;
    [SerializeField]
    private RectTransform _contentSize;
    [SerializeField]
    private RectTransform _layoutGroup;

    [SerializeField]
    private UI_Dice _dicePrefab;
    
    private List<UI_Dice> _uIDices = new List<UI_Dice>();

    
    public void InitDiceInventoryUI()
    {
        var data = GameKitCenter.Inventory.GetInventory(DiceInventory.current.Name);
        for (int i = 0; i < data.StockMap.Length; i++)
        {
            if(data.StockMap[i] == null) continue;
            var dice = Instantiate(_dicePrefab, _layoutGroup).OnInit((UI_DiceData_SO)data.StockMap[i].Data, i, null);
            _uIDices.Add(dice);
        }

        float height = _basicOffset + _uIDices.Count * _diceOffset;
        _contentSize.sizeDelta = new Vector2(_contentSize.sizeDelta.x, height);

    }

    public void ClearDices()
    {
        foreach (Transform trans in _layoutGroup.transform)
        {
            Destroy(trans.gameObject);
        }
        _uIDices.Clear();
    }
    
    public UI_DiceData_SO Select(ref int _currentIndex,int index)
    {
        if (index < 0 || index >= _uIDices.Count) return null;

        if (_currentIndex >= 0 && _currentIndex < _uIDices.Count)
            _uIDices[_currentIndex].OnDisSelected();

        _uIDices[index].OnSelected();
        _currentIndex = index;

        //Debug.Log(index + ";" + _uIDices.Count);
        float normalizedHeight = (1 - (float)(index) / (float)_uIDices.Count);
        //Debug.Log(normalizedHeight);
        _scrollRect.normalizedPosition = new Vector2(0, normalizedHeight);
        
        return _uIDices[index].Data;
    }
}