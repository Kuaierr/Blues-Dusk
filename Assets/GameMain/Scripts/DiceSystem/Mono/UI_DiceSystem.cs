using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using GameKit.QuickCode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum Dice_SuitType
{
    SWORD,
    GRAIL,
    STARCOIN,
    WAND,
    SPECIAL
}

public class UI_DiceSystem : UIFormChildBase
{
    [Header("Basic Elements")]
    private List<UI_Dice> _negativeDices = new List<UI_Dice>();
    private Dictionary<int, UI_Dice> _activedDices = new Dictionary<int, UI_Dice>();

    private List<Transform> _sheets = new List<Transform>();
    private List<UI_CustomButton> _tabs = new List<UI_CustomButton>();

    public Transform swapPos;
        
    [Space]
    [SerializeField]
    private Transform _sheetPos;

    [SerializeField]
    private Transform _pageContent;

    [Space]
    [Header("Prefabs")]
    [SerializeField]
    private UI_Dice _dicePrefab;

    [SerializeField]
    private UnityEngine.UI.Image _diceSheetPrefab;

    [SerializeField]
    private UI_CustomButton _tabButtonPrefab;

    [Space]
    [SerializeField]
    private UI_DiceStartButton _startButton;

    public Dice_Result Result { get; private set; } = new Dice_Result(); //保存结果的类

    #region KeybordSelecter

    private int _currentDiceIndex = -1;
    private List<UI_Dice> _currentList;

    private UI_Dice _currentDice;

    #endregion

    public void OnInit()
    {
        _startButton.OnInit(OnStartButtonClickedCallback);
        CreateDicesFromInventory();
        SelectDice(0, _negativeDices);

        EnablePlayerInput();
    }

    public void EnablePlayerInput()
    {
        StartCoroutine("KeybordInputCheck");
    }

    public void Clear()
    {
        //清空投出的骰子
        for (int i = 0; i < _negativeDices.Count; i++)
        {
            if (_activedDices.ContainsKey(i))
            {
                Destroy(_activedDices[i].gameObject);
                _activedDices.Remove(i);
            }
        }

        //TODO 清空可以选的骰子 和 对应分页
        for (int i = 0; i < _negativeDices.Count; i++)
        {
            Destroy(_negativeDices[i].gameObject);
            _negativeDices.RemoveAt(i);
            i--;
        }

        for (int i = 0; i < _sheets.Count; i++)
        {
            Destroy(_sheets[i].gameObject);
            Destroy(_tabs[i].gameObject);
            _sheets.RemoveAt(i);
            _tabs.RemoveAt(i);
            i--;
        }

        /*_negativeDices.Clear();
        _activedDices.Clear();*/
        Result.Clear();
        _startButton.Clear();
        _currentDice = null;

        GetComponent<CanvasGroup>().blocksRaycasts = true;
        StopCoroutine("KeybordInputCheck");
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Clear();
    }

    private void CreateDicesFromInventory()
    {
        var inventory = GameKitCenter.Inventory.GetInventory(DiceInventory.current.Name);

        UI_Dice dice = null;
        Transform parent = null;
        List<UI_DiceData_SO> datas = new List<UI_DiceData_SO>();

        for (int i = 0; i < inventory.StockMap.Length; i++)
        {
            if (inventory.StockMap[i] == null) continue;
            datas.Add((UI_DiceData_SO)inventory.StockMap[i].Data);
        }

        for (int i = 0; i < datas.Count; i++)
        {
            if ((i + 1) % 8 == 1)
            {
                int j = i / 8;
                
                parent = Instantiate(_diceSheetPrefab, _diceSheetPrefab.transform.parent).transform;
                _sheets.Add(parent);
                
                var tab = Instantiate(_tabButtonPrefab, _tabButtonPrefab.transform.parent).OnInit(j, () =>
                {
                    foreach (Transform sheet in _sheets)
                        sheet.gameObject.SetActive(false);
                    foreach (UI_CustomButton button in _tabs)
                        button.OnReleased();
                    _tabs[j].OnSelected();
                    _sheets[j].gameObject.SetActive(true);
                }, null);
                _tabs.Add(tab);
                tab.gameObject.SetActive(true);
            }

            dice = Instantiate(_dicePrefab, parent).OnInit(datas[i], i, OnConfirmKeyPressed, OnPointerEnterDice);
            _negativeDices.Add(dice);
        }

        _sheets[0].gameObject.SetActive(true);
        _tabs[0].OnSelected();
    }


    #region InSelectingState

    //不需要
    public void OnDiceClicked(UI_Dice dice)
    {
        if (!_activedDices.ContainsValue(dice))
        {
            DiceSelected(dice);
        }
        else
        {
            DiceUnSelected(dice);
        }

        /*bool canCheck = false;
        
        foreach (UI_Dice uiDice in _activedDices.Values)
        {
            if (uiDice != null)
            {
                canCheck = true;
                break;
            }
        }*/

        if (_activedDices.Values.Count > 0)
            _startButton.Enable();
        else
            _startButton.Disable();
    }

    private void OnPointerEnterDice(int index)
    {
        UI_Dice targetDice = _negativeDices[index];
        if (targetDice == _currentDice) return;
        if (_activedDices.ContainsValue(targetDice))
        {
            //SelectDice(_activedDices.IndexOf(targetDice), _activedDices);
        }
        else
        {
            SelectDice(index, _negativeDices);
        }
    }

    private void DiceSelected(UI_Dice dice)
    {
        //TODO 随机坐标
        UI_Dice clone = Instantiate(dice, swapPos);
        clone.InitAsRollingDice();
        _activedDices.Add(dice.Index,clone);
    }

    private void DiceUnSelected(UI_Dice dice)
    {
        Destroy(_activedDices[dice.Index]);
        _activedDices.Remove(dice.Index);
    }

    #endregion

    #region InRollingState

    //Info 按下按钮时调用的
    private void OnStartButtonClickedCallback()
    {
        
    }

    public void RollActivedDices()
    {
        //await Task.Delay(500);
        //TODO 这一行完全可以直接在动画中控制
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        _currentDice = null;

        foreach (UI_Dice dice in _activedDices.Values)
        {
            if (dice == null) continue;
            dice.Roll();
        }

        GetComponent<CanvasGroup>().blocksRaycasts = false;
        StopCoroutine("KeybordInputCheck");
    }

    public bool CheckIfFinishRolling()
    {
        foreach (UI_Dice dice in _activedDices.Values)
        {
            if (dice == null) continue;
            if (!dice.Stopped) return false;
        }

        return true;
    }

    public bool CheckIfFinishReseting()
    {
        foreach (UI_Dice dice in _activedDices.Values)
        {
            if (dice == null) continue;
            if (!dice.IsComplete) return false;
        }

        return true;
    }

    #endregion

    #region AfterRolling

    //先将各个结果的数据存储起来，以备结算
    public void AddDiceFaceToResultList()
    {
        foreach (UI_Dice dice in _activedDices.Values)
        {
            if (dice == null) continue;
            Result.Push(dice.GetResult());
        }
    }

    public Dice_Result CaculateFinalResult()
    {
        Result.EffectsProcess();
        Debug.Log(Result.ToString());
        return Result;
    }

    #endregion

    #region Keybord Input

    //键盘输入检测的接口
    private IEnumerator KeybordInputCheck()
    {
        while (true)
        {
            /*if (diceAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name != ("An_DiceSystem_On"))
            {
                yield return 0;
                continue;
            }*/

            if (InputManager.instance.GetKeyDown(KeyCode.LeftShift))
                OnStartKeyPressed();
            if (InputManager.instance.GetKeyUp(KeyCode.LeftShift))
                OnStartKeyReleased();

            if (!InputManager.instance.GetKey(KeyCode.LeftShift))
            {
                if (InputManager.instance.GetKeyDown(KeyCode.W))
                    OnUpKeyPressed();
                if (InputManager.instance.GetKeyDown(KeyCode.S))
                    OnDownKeyPressed();
                if (InputManager.instance.GetKeyDown(KeyCode.A))
                    OnLeftKeyPressed();
                if (InputManager.instance.GetKeyDown(KeyCode.D))
                    OnRightKeyPressed();
                if (InputManager.instance.GetKeyDown(KeyCode.Space))
                    OnConfirmKeyPressed();
            }

            yield return 0;
        }
    }

    //Info CurrentList & CurrentIndex 均在这里改变
    private void SelectDice(int index, List<UI_Dice> list)
    {
        //Debug.Log(index + ", " + list.Count);

        if (_currentDice != null)
            _currentDice.OnDisSelected();

        _currentDice = list[index];
        _currentDice.OnSelected();

        _currentDiceIndex = index;
        _currentList = list;
    }

    //TODO 优化逻辑，重复的代码块较多
    private void OnUpKeyPressed() { }

    private void OnDownKeyPressed() { }

    private void OnRightKeyPressed() { }

    private void OnLeftKeyPressed() { }

    private void OnConfirmKeyPressed(UI_Dice dice = null) { OnDiceClicked(_currentDice);}

    private void OnStartKeyPressed() => _startButton.OnButtonPressed();
    private void OnStartKeyReleased() => _startButton.OnButtonReleased();

    #endregion

    public void AddStartButtonCallback(UnityAction callback)
    {
        _startButton.AddCallBack(callback);
    }
}

//用于存储与输出结果，同时承担了结算的作用
public class Dice_Result
{
    public Dictionary<Dice_SuitType, int> sum { get; private set; } = null;

    private Dictionary<string, int> m_SerializableSum = null;
    private List<Dice_SuitType> m_CachedAttributeTypes;

    public Dictionary<string, int> SerializableSum
    {
        get
        {
            if (m_SerializableSum == null)
                m_SerializableSum = new Dictionary<string, int>();

            if (m_SerializableSum.Count == 0)
            {
                foreach (var diceResult in sum)
                {
                    m_SerializableSum.Add(System.Enum.GetName(typeof(Dice_SuitType), diceResult.Key), diceResult.Value);
                }
            }

            return m_SerializableSum;
        }
    }

    //通过对这个列表进行操作，达到终止后续效果处理的效果
    public List<UI_DiceFaceBase_SO> results;

    private bool breakOut = false;

    public Dice_Result()
    {
        sum = new Dictionary<Dice_SuitType, int>()
        { { Dice_SuitType.SWORD, 0 },
          { Dice_SuitType.GRAIL, 0 },
          { Dice_SuitType.STARCOIN, 0 },
          { Dice_SuitType.WAND, 0 } };
        results = new List<UI_DiceFaceBase_SO>();
        m_CachedAttributeTypes = new List<Dice_SuitType>()
        { Dice_SuitType.SWORD, Dice_SuitType.GRAIL, Dice_SuitType.STARCOIN, Dice_SuitType.WAND };
    }

    public void Push(UI_DiceFaceBase_SO face)
    {
        results.Add(face);

        //给优先级排序 此处后续可优化
        results.Sort((x, y) => { return x.Priority.CompareTo(y.Priority); });
    }

    //目前这种做法，如果有相同优先级的效果，似乎会按照选择时的顺序触发
    public void EffectsProcess()
    {
        for (int i = 0; i < results.Count; i++)
        {
            results[i].Effect(this);
            if (breakOut)
            {
                breakOut = false;
                break;
            }
        }

        // Debug.Log(this.ToString());
        results.Clear(); //防止重复调用时结果出错
    }

    public void BreakOut() => breakOut = true;

    public int Get(Dice_SuitType type) => sum[type] > 0 ? sum[type] : 0;

    public void Add(Dice_SuitType type, int amount) => sum[type] += amount;

    public void Set(Dice_SuitType type, int amount) => sum[type] = amount;

    public override string ToString()
    {
        return "Sword: " + sum[Dice_SuitType.SWORD].ToString() + "\n"
               + "Grail: " + sum[Dice_SuitType.GRAIL].ToString() + "\n"
               + "Starcoin: " + sum[Dice_SuitType.STARCOIN].ToString() + "\n"
               + "Wand: " + sum[Dice_SuitType.WAND].ToString() + "\n";
    }

    public void Clear()
    {
        for (int i = 0; i < m_CachedAttributeTypes.Count; i++)
        {
            sum[m_CachedAttributeTypes[i]] = 0;
        }

        if (m_SerializableSum != null)
            m_SerializableSum.Clear();
    }
}