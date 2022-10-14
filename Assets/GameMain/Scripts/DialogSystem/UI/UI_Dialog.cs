using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GameKit;
using GameKit.Fsm;
using GameKit.UI;
using GameKit.Event;
using GameKit.Dialog;
using GameKit.DataNode;
using System.Collections.Generic;
using Febucci.UI;
using UnityEngine.Events;
using UnityGameKit.Runtime;


public class UI_Dialog : UIFormBase
{
    public CharacterPool characterPool;

    [Space]
    [Header("Character Images")]
    public UI_Character uI_NPCImage;

    public UI_Character uI_PlayerImage;

    [Space]
    [Header("Child Components")]
    public UI_Response uI_Response;

    //public UI_Indicator uI_Indicator;
    public UI_SpeakerName uI_SpeakerName;
    public UI_DiceSystem uI_DiceSystem;
    public TextMeshProUGUI t_SpeakerName;
    public TextMeshProUGUI t_Contents;

    [Space]
    [Header("Animators")]
    public TextAnimatorPlayer TextAnimatorPlayer;

    public Animator NPCAnimator;
    public Animator PlayerAnimator;
    public Animator EdgeAnimator;

    private Character m_CurrentCharacter;
    private IFsm<UI_Dialog> fsm;
    private List<FsmState<UI_Dialog>> stateList;
    private bool m_IsDialoging = false;

    private bool m_IsShowingText = true;

    //private List<bool> m_CachedCheckResults = new List<bool>();
    private Dictionary<string, bool> m_CachedCheckResults = new Dictionary<string, bool>();

    public IDialogTree CurrentTree
    {
        get { return GameKitCenter.Dialog.CurrentTree; }
    }

    public bool IsDialoging
    {
        get { return m_IsDialoging; }
    }

    public Animator DiceAnimator
    {
        get { return uI_Response.DiceAnimator; }
    }


    #region Override

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        CreateFsm();
        uI_Response.Init(Depth);
        uI_NPCImage.Init(Depth);
        //uI_Indicator.Init(Depth);
        characterPool = GameKitCenter.Data.GetDataSO<CharacterPool>();
        GameKitCenter.Event.Subscribe(UnityGameKit.Runtime.StartDialogSuccessEventArgs.EventId, OnStartDialogSuccess);
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        CursorSystem.current.Disable();
        m_IsDialoging = true;
        StartFsm();
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        CursorSystem.current.Enable();
        m_IsDialoging = false;
        base.OnClose(isShutdown, userData);
    }

    protected override void OnPause()
    {
        CursorSystem.current.Enable();
        m_IsDialoging = false;
        base.OnPause();

        ReFocusGameMenuEventArgs args = ReFocusGameMenuEventArgs.Create(this);
        GameKitCenter.Event.Fire(this, args);
    }

    protected override void OnResume()
    {
        base.OnResume();
        CursorSystem.current.Disable();
        m_IsDialoging = true;

        GameKitCenter.UI.RefocusUIForm(GetComponent<UIForm>());
    }

    protected override void OnRecycle()
    {
        base.OnRecycle();
    }

    protected override void OnRefocus(object userData)
    {
        base.OnRefocus(userData);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        uI_Response.OnUpdate();
    }

    #endregion

    public void ShowResponse(UnityAction callback = null)
    {
        uI_Response.gameObject.SetActive(true);
        uI_Response.Show(callback);
    }

    public void HideResponse(UnityAction callback = null)
    {
        uI_Response.Hide(callback);
    }

    public void AddTyperWriterListener(UnityAction onTypewriterStart, UnityAction onTextShowed)
    {
        TextAnimatorPlayer.onTypewriterStart.AddListener(onTypewriterStart);
        TextAnimatorPlayer.onTextShowed.AddListener(onTextShowed);
    }

    private void CreateFsm()
    {
        stateList = new List<FsmState<UI_Dialog>>();
        stateList.Add(new DialogIdleState());
        stateList.Add(new DialogTalkingState());
        stateList.Add(new DialogChoosingState());
        stateList.Add(new DialogAnimatingState());

        //可能不需要这么多状态
        stateList.Add(new DiceDialogSelectingState());
        //stateList.Add(new DiceDialogCheckingState());
        stateList.Add(new DiceDialogRollingState());
        stateList.Add(new DiceDialogResetingState());
        //如果对UI_Option进行扩展的话，这个状态可以不需要
        //stateList.Add(new DiceDialogChoosingState());

        fsm = GameKitCenter.Fsm.CreateFsm<UI_Dialog>(gameObject.name, this, stateList);
    }

    private void StartFsm()
    {
        fsm.Start<DialogIdleState>();
    }

    private void DestroyFsm()
    {
        GameKitCenter.Fsm.DestroyFsm(fsm);
        stateList.Clear();
        fsm = null;
    }

    public void PassNode()
    {
        if (GameKitCenter.Dialog.CurrentTree == null)
        {
            Log.Warning("Cached Current Tree is Empty");
            return;
        }

        GameKitCenter.Dialog.CurrentTree.CurrentNode = GameKitCenter.Dialog.CurrentTree.GetChildNode(0);
    }

    public IDataNode GetNextNode(IDataNode currentNode, int index = 0)
    {
        string str = "";
        for (int i = 0; i < currentNode.ChildCount; i++)
        {
            str += currentNode.GetChild(i).GetData<DialogDataNodeVariable>().ToString() + '\n';
        }

        if (currentNode.ChildCount > 1)
        {
            Log.Info(str.RemoveLast());
        }

        if (currentNode.IsLeaf)
        {
            ReachTheEndOfConversation();
            return null;
        }

        IDataNode sonNode = GameKitCenter.Dialog.CurrentTree.GetChildNode(index);
        return sonNode;
    }

    // 每次访问节点都会尝试调用该方法
    public IDataNode TryExecuteNodeFunction(IDataNode sonNode)
    {
        IDataNode nextNode = sonNode;
        DialogDataNodeVariable tempDialogData = sonNode.GetData<DialogDataNodeVariable>();

        // 如果本次对话中有可完成得条件
        if (tempDialogData.IsLocalCompleter)
        {
            for (int j = 0; j < tempDialogData.CompleteConditons.Count; j++)
            {
                GameKitCenter.Dialog.CurrentTree.LocalConditions[tempDialogData.CompleteConditons[j]] = true;
            }
        }

        // 如果全局设置中有可完成得条件
        if (tempDialogData.IsGlobalCompleter)
        {
            for (int j = 0; j < tempDialogData.GlobalCompleteConditons.Count; j++)
            {
                GameSettings.current.SetBool(tempDialogData.GlobalCompleteConditons[j], true);
                // Log.Warning(tempDialogData.GlobalCompleteConditons[j] + " >> " + GameSettings.current.GetBool(tempDialogData.GlobalCompleteConditons[j]));
            }
        }
        // 如果该节点是仓检的选项
        /*if (tempDialogData.IsInventoryCheckOption && tempDialogData.CachedInventoryName != "DiceInventory")
        {
            //Info tempDialogData.CachedStockConditions 有需要检测物品名，物品名与陪标中的name一致 
            //Info tempDialogData.CachedInventoryName 有检测背包的名称
            //Bug 这里没有用处，不会被执行，因为仓检是被标记在选项中的
            Debug.Log("Dialog -- IsInventoryCheckOption --");
            m_CachedCheckResults.Clear();
            bool clear = true;
            for (int i = 0; i < tempDialogData.CachedStockConditions.Count; i++)
            {
                if (GameKitCenter.Inventory.GetStockFromInventory(tempDialogData.CachedInventoryName,
                    tempDialogData.CachedStockConditions[i]) == null)
                {
                    clear = false;
                    break;
                }
                m_CachedCheckResults.Add(tempDialogData.Contents, clear);
                Debug.Log("InventoryName: " + tempDialogData.CachedInventoryName + "\n" +
                          "TargetItemName: " + tempDialogData.CachedStockConditions[i] + "\n" +
                          "Result: " + clear);
            }
        }*/

        // 如果是纯功能节点
        if (tempDialogData.IsFunctional)
        {
            // 如果是基于本次对话条件的纯分支点
            if (tempDialogData.IsLocalDivider)
            {
                bool isComplete = true;
                for (int j = 0; j < tempDialogData.DividerConditions.Count; j++)
                {
                    // foreach (var item in GameKitCenter.Dialog.CurrentTree.LocalConditions)
                    // {
                    //     Log.Info(item.Key + " >> " + item.Value);
                    // }
                    if (!GameKitCenter.Dialog.CurrentTree.LocalConditions[tempDialogData.DividerConditions[j]])
                    {
                        isComplete = false;
                        break;
                    }
                }

                // Log.Info(isComplete);
                // for (int i = 0; i < nextNode.ChildCount; i++)
                // {
                //     Log.Info(nextNode.Name + " > > " + nextNode.GetChild(i).Name);
                // }
                if (isComplete)
                {
                    nextNode = sonNode.GetChild(0);
                }
                else
                {
                    nextNode = sonNode.GetChild(1);
                }
            }

            // 如果是全局条件的纯分支点
            if (tempDialogData.IsGlobalDivider)
            {
                bool isComplete = true;
                for (int j = 0; j < tempDialogData.GlobalDividerConditions.Count; j++)
                {
                    if (!GameSettings.current.GetBool(tempDialogData.GlobalDividerConditions[j]))
                    {
                        isComplete = false;
                        break;
                    }
                }

                if (isComplete)
                {
                    nextNode = sonNode.GetChild(0);
                }
                else
                {
                    nextNode = sonNode.GetChild(1);
                }
            }
        }

        // Debug.Log(nextNode.Name);
        // Debug.Log("IsFunctional: " + nextNode.GetData<DialogDataNodeVariable>().IsFunctional);
        if (nextNode.GetData<DialogDataNodeVariable>().IsFunctional)
        {
            nextNode = TryExecuteNodeFunction(nextNode);
            return nextNode;
        }

        return nextNode;
    }

    private void ReachTheEndOfConversation()
    {
        Log.Info("Reach The End Of Conversation.");
        m_IsDialoging = false;
        // GameKitCenter.Dialog.CurrentTree.Reset();
        DialogSystem.current.StopDialog(CurrentTree.Name);
        GameKitCenter.Dialog.CurrentTree = null;
    }

    public void RefreshDialogUI(IDataNode node, bool useTypeWriter = true, UnityAction callback = null)
    {
        t_SpeakerName.text = string.Empty;
        t_Contents.text = string.Empty;
        NPCAnimator.SetTrigger(UIUtility.FORCE_OFF_ANIMATION_NAME);
        PlayerAnimator.SetTrigger(UIUtility.FORCE_OFF_ANIMATION_NAME);
    }

    public void UpdateDialogUI(IDataNode node, bool useTypeWriter = true, UnityAction callback = null)
    { //Info 在这里修改对话时UI的显示情况
        TextAnimatorPlayer.useTypeWriter = useTypeWriter;
        DialogDataNodeVariable data = node.GetData<DialogDataNodeVariable>();
        data.Speaker = data.Speaker.Correction();
        data.Contents = data.Contents.Correction();

        // Log.Warning(data.Contents);

        if (node == null || data.Speaker == "Default")
            return;

        //Bug SpeakerName的动画没有正常播放
        if (data.Speaker == ">>" || data.Speaker == "")
            t_SpeakerName.text = "";
        else if (data.Speaker == "??")
            t_SpeakerName.text = "未知";
        else
            t_SpeakerName.text = data.Speaker;

        t_Contents.text = data.Contents;
        uI_SpeakerName.SetActive(!(data.Speaker == ">>" || data.Speaker == ""));

        if (data.Speaker != ">>" && data.Speaker != "")
        {
            if (data.Speaker != "伊森")
            {
                Character character = characterPool.GetData<Character>(data.Speaker.Correction());
                if (m_CurrentCharacter != character)
                {
                    m_CurrentCharacter = character;
                    NPCAnimator.SetTrigger(UIUtility.SHOW_ANIMATION_NAME);
                }

                // RuntimeAnimatorController charaAnimator = FindAnimator(character.idName);
                uI_NPCImage.avatar.sprite = character.GetMood(data.MoodName).avatar;
            }
            else
            {
                Character character = characterPool.GetData<Character>(data.Speaker.Correction());
                if (m_CurrentCharacter != character)
                {
                    m_CurrentCharacter = character;
                    PlayerAnimator.SetTrigger(UIUtility.SHOW_ANIMATION_NAME);
                }

                // RuntimeAnimatorController charaAnimator = FindAnimator(character.idName);
                uI_PlayerImage.avatar.sprite = character.GetMood(data.MoodName).avatar;
            }
            // character.animator.runtimeAnimatorController = charaAnimator;
        }
        else
        {
            NPCAnimator.SetTrigger(UIUtility.HIDE_ANIMATION_NAME);
            PlayerAnimator.SetTrigger(UIUtility.HIDE_ANIMATION_NAME);

            uI_SpeakerName.Animator.SetTrigger(UIUtility.FORCE_OFF_ANIMATION_NAME);
        }
    }


    public void UpdateOptionUI(bool isDiceCheck = false, UnityAction callback = null)
    {
        IDialogOptionSet optionSet = GameKitCenter.Dialog.CreateOptionSet(GameKitCenter.Dialog.CurrentTree.CurrentNode);
        if (optionSet != null)
        {
            uI_Response.UpdateOptions(optionSet, isDiceCheck);
            ShowResponse(callback);
        }
    }

    /*public void UpdatePlayerInventoryCheckOptionUI(UnityAction callback = null)
    {
        IDialogOptionSet optionSet = GameKitCenter.Dialog.CreateOptionSet(GameKitCenter.Dialog.CurrentTree.CurrentNode);
        if (optionSet != null)
        {
            uI_Response.UpdateAsPlayerInventoryCheckOptions(optionSet, m_CachedCheckResults);
            ShowResponse(callback);
        }
        
        m_CachedCheckResults.Clear();
    }

    public void UpdateDiceInventoryCheckOptionUI(UnityAction callback = null)
    {
        IDialogOptionSet optionSet = GameKitCenter.Dialog.CreateOptionSet(GameKitCenter.Dialog.CurrentTree.CurrentNode);
        if (optionSet != null)
        {
            uI_Response.UpdateAsDiceInventoryCheckOption(optionSet/*, m_CachedCheckResults#1#);
            ShowResponse(callback);
        }
    }*/

    public void UpdateOptionsPoint(Dice_Result result)
    {
        uI_Response.UpdateOptionsPoint(result);
    }

    public void InternalVisible(bool status)
    {
        base.InternalSetVisible(status);
        MasterAnimator.SetTrigger(status ? UIUtility.SHOW_ANIMATION_NAME : UIUtility.HIDE_ANIMATION_NAME);
        EdgeAnimator.SetTrigger(status ? UIUtility.SHOW_ANIMATION_NAME : UIUtility.HIDE_ANIMATION_NAME);
        NPCAnimator.SetTrigger(status ? UIUtility.SHOW_ANIMATION_NAME : UIUtility.HIDE_ANIMATION_NAME);
    }

    public void ForceVisibleOff()
    {
        MasterAnimator.SetTrigger(UIUtility.FORCE_OFF_ANIMATION_NAME);
        EdgeAnimator.SetTrigger(UIUtility.FORCE_OFF_ANIMATION_NAME);
        NPCAnimator.SetTrigger(UIUtility.FORCE_OFF_ANIMATION_NAME);
    }

    private void OnStartDialogSuccess(object sender, BaseEventArgs e)
    {
        // UnityGameKit.Runtime.StartDialogSuccessEventArgs ne = (UnityGameKit.Runtime.StartDialogSuccessEventArgs)e;
        // GameKitCenter.Dialog.CurrentTree = ne.DialogTree;
        // GameKitCenter.Dialog.CurrentTree.Reset();
        NPCAnimator.ResetTrigger(UIUtility.HIDE_ANIMATION_NAME);
        NPCAnimator.ResetTrigger(UIUtility.SHOW_ANIMATION_NAME);
        PlayerAnimator.ResetTrigger(UIUtility.HIDE_ANIMATION_NAME);
        PlayerAnimator.ResetTrigger(UIUtility.SHOW_ANIMATION_NAME);
        m_CurrentCharacter = null;

        OnResume();
    }

    #region DiceSystem

    public void InitDiceSystem()
    {
        uI_DiceSystem.OnInit();
        // DiceAnimator.SetTrigger(UIUtility.SHOW_ANIMATION_NAME);
    }

    public void RollActiveDices()
    {
        uI_DiceSystem.RollActivedDices();
    }

    public bool CheckIfFinishRolling()
    {
        return uI_DiceSystem.CheckIfFinishRolling();
    }

    public bool CheckIfFinishReseting()
    {
        return uI_DiceSystem.CheckIfFinishReseting();
    }

    public void OnFinishRolling()
    {
        uI_DiceSystem.AddDiceFaceToResultList();
        uI_DiceSystem.ResetDicePosition();
    }

    public Dice_Result GetFinalResult()
    {
        return uI_DiceSystem.CaculateFinalResult();
    }

    public void AddCheckButtonCallback(UnityAction callback)
    {
        uI_DiceSystem.AddStartButtonCallback(callback);
    }

    #endregion


    private void OnDestroy()
    {
        DestroyFsm();
    }

    // private void LoadAnimator()
    // {
    //     if (charaAnimators == null || charaAnimators.Count == 0)
    //     {
    //         AddressableManager.instance.GetAssetsAsyn<RuntimeAnimatorController>(new List<string> { "Character Animator" }, callback: (IList<RuntimeAnimatorController> assets) =>
    //         {
    //             charaAnimators = new List<RuntimeAnimatorController>(assets);
    //         });
    //     }
    // }

    // private RuntimeAnimatorController FindAnimator(string name)
    // {
    //     for (int i = 0; i < charaAnimators.Count; i++)
    //     {
    //         if (charaAnimators[i].name == "AC_" + name)
    //         {
    //             return charaAnimators[i];
    //         }
    //     }
    //     return null;
    // }
}