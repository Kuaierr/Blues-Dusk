using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UI_DiceSystem : MonoBehaviour
{
    [Header("Basic Elements")]
    //在背包里的骰子
    private List<UI_Dice> _negativeDices = new List<UI_Dice>();

    //被选中了的格子 进入投掷状态后将这些骰子的父物体设为this，并投出他们即可
    private List<UI_Dice> _activedDices = new List<UI_Dice>();

    private List<Transform> _negativeDiceSlots = new List<Transform>();

    [SerializeField]
    private List<Transform> _activedDiceSlots = new List<Transform>();

    [SerializeField]
    private UI_Dice _dicePrefab;

    [SerializeField]
    private UnityEngine.UI.Image _diceSlotPrefab;

    [Space]
    [SerializeField]
    private Transform _gridLayoutByTwo;

    [SerializeField]
    private Transform _gridLayoutByOne;

    [Space]
    [Header("ChildComponents")]
    [SerializeField]
    private UI_DiceStartButton _startButton;

    [SerializeField]
    private CanvasGroup _selectPanel;
    
    /*[SerializeField]
    private RectTransform _diceSheet;*/

    [Space]
    [Header("Temp Data")]
    [SerializeField]
    private List<UI_DiceData_SO> _tempDiceList = new List<UI_DiceData_SO>();
    
    [SerializeField]
    private List<RectTransform> _diceSheets = new List<RectTransform>();

    private Dictionary<string, RectTransform> _usedSheets = new Dictionary<string, RectTransform>();

    public void OnInit()
    {
        _startButton.OnInit(OnStartButtonClicked);
        CreateDicesFromInventory();
    }

    private void Start()
    {
        OnInit();
    }

    public void OnDiceClicked(UI_Dice dice)
    {
        if (_negativeDices.Contains(dice))
        {
            if (_activedDices.Count == _activedDiceSlots.Count) return;
            DiceSelected(dice);
        }
        else
        {
            DiceUnSelected(dice);
        }

        if (_activedDices.Count > 0)
            _startButton.Enable();
        else
            _startButton.Disable();
    }

    private void DiceSelected(UI_Dice dice)
    {
        _negativeDices.Remove(dice);
        _activedDices.Add(dice);
        //替换材质，更改父物体
        dice.transform.SetParent(FindEmptyDiceSlot());
        dice.ChangeToDiceUIMaterial();

        //移动到目标格子
        dice.DOComplete();
        dice.transform.DOMove(dice.transform.parent.position, 0.5f);
    }

    private void DiceUnSelected(UI_Dice dice)
    {
        _negativeDices.Add(dice);
        _activedDices.Remove(dice);

        dice.transform.SetParent(_negativeDiceSlots[dice.Index]);
        dice.ChangeToDiceMaskMaterial();
        //回到原本的位置
        dice.DOComplete();
        dice.transform.DOMove(_negativeDiceSlots[dice.Index].position, 0.5f);
    }

    private void OnStartButtonClicked()
    {
        ResetActivedDiceParent();
        ChangeStateToRolling();
    }

    private Transform FindEmptyDiceSlot()
    {
        foreach (Transform slot in _activedDiceSlots)
        {
            if (slot.childCount == 0) return slot;
        }

        return null;
    }

    private void CreateDicesFromInventory()
    {
        Transform targetGrid = null;
        Transform parent = null;
        UI_Dice dice = null;
        //生成骰子
        for (int i = 0; i < _tempDiceList.Count; i++)
        {
            //按照2121进行排列
            if ((i + 1) % 3 == 0)
                targetGrid = _gridLayoutByOne;
            else
                targetGrid = _gridLayoutByTwo;


            parent = Instantiate(_diceSlotPrefab, targetGrid).transform;
            dice = Instantiate(_dicePrefab, parent).OnInit(_tempDiceList[i], i, OnDiceClicked);

            _negativeDices.Add(dice);
            _negativeDiceSlots.Add(parent);
        }
    }

    private void ResetActivedDiceParent()
    {
        foreach (UI_Dice dice in _activedDices)
        {
            dice.transform.SetParent(transform);
        }
    }

    private void ChangeStateToRolling()
    {
        //移动整体UI布局 取消SelectedPanel
        Roll();
        
        for (int i = 0; i < _negativeDices.Count; i++)
        {
            var dice = _negativeDices[0];
            _negativeDices.Remove(dice);
            Destroy(dice);
        }
    }

    private void Roll()
    {
        foreach (UI_Dice dice in _activedDices)
            dice.Roll();

        StartCoroutine("Rolling");
    }

    public bool CheckIfFinishRolling()
    {
        foreach (UI_Dice dice in _activedDices)
        {
            if (!dice.Stopped) return false;
        }

        return true;
    }

    public void ResetDicePosition()
    {
        foreach (UI_Dice dice in _activedDices)
        {
            dice.ResetTransform(_usedSheets[dice.Result]);
        }
    }

    //暂时代替状态机与update
    private IEnumerator Rolling()
    {
        while (!CheckIfFinishRolling())
            yield return 0;
        
        foreach (UI_Dice dice in _activedDices)
            ProvideSheet(dice.GetResult());

        ResetDicePosition();
    }

    private void ProvideSheet(string result)
    {
        if (_usedSheets.ContainsKey(result))
            return;
        else
        {
            if(_diceSheets.Count == 0) 
                Debug.LogError("Lack Of Sheets");
            _usedSheets.Add(result, _diceSheets[0]);
            _diceSheets.RemoveAt(0);
        }
    }
    
    //TODO 结果的存储与输出
}

