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
    public UI_Character uI_Character;
    public UI_Response uI_Response;
    public UI_Indicator uI_Indicator;
    public UI_DiceSystem uI_DiceSystem;
    public TextMeshProUGUI t_SpeakerName;
    public TextMeshProUGUI t_Contents;
    public TextAnimatorPlayer TextAnimatorPlayer;
    public Animator dialogAnimator;
    public Animator speakerAnimator;
    public Animator edgeAnimator;
    public Animator diceAnimator;

    private Character m_CurrentCharacter;
    private IFsm<UI_Dialog> fsm;
    private List<FsmState<UI_Dialog>> stateList;

    public IDialogTree CurrentTree
    {
        get
        {
            return GameKitCenter.Dialog.CurrentTree;
        }
    }


    #region Override
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        GameKitCenter.Event.Subscribe(UnityGameKit.Runtime.StartDialogSuccessEventArgs.EventId, OnStartDialogSuccess);
        CreateFsm();
        fsm.SetData<VarBoolean>(DialogStateUtility.DIALOG_START_ID, true);
        uI_Response.OnInit(Depth);
        uI_Character.OnInit(Depth);
        uI_Indicator.OnInit(Depth);
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        CursorSystem.current.Disable();
        StartFsm();
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
        CursorSystem.current.Enable();
        dialogAnimator.SetTrigger("FadeOut");
        edgeAnimator.SetTrigger("FadeOut");
        speakerAnimator.SetTrigger("FadeOut");

    }

    protected override void OnPause()
    {
        // base.OnPause();
        CursorSystem.current.Enable();
        dialogAnimator.SetTrigger("FadeOut");
        edgeAnimator.SetTrigger("FadeOut");
        speakerAnimator.SetTrigger("FadeOut");
    }

    protected override void OnResume()
    {
        base.OnResume();
        Log.Warning("OnResume");
        CursorSystem.current.Disable();
        dialogAnimator.SetTrigger("FadeIn");
        dialogAnimator.SetTrigger("FadeIn");
        edgeAnimator.SetTrigger("FadeIn");
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
        uI_Response.isActive = true;
        uI_Response.gameObject.SetActive(true);
        uI_Response.OnShow(callback);
    }

    public void HideResponse(UnityAction callback = null)
    {
        uI_Response.OnHide(callback);
    }

    public void MakeChoice()
    {
        HideResponse();
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
        stateList.Add(new DiceDialogCheckingState());
        stateList.Add(new DiceDialogRollingState());
        stateList.Add(new DiceDialogResetingState());
        //如果对UI_Option进行扩展的话，这个状态可以不需要
        stateList.Add(new DiceDialogChoosingState());

        fsm = GameKitCenter.Fsm.CreateFsm<UI_Dialog>(gameObject.name, this, stateList);
    }

    private void StartFsm()
    {
        fsm.Start<DialogIdleState>();
        fsm.SetData<VarBoolean>(DialogStateUtility.DIALOG_START_ID, true);
    }

    private void DestroyFsm()
    {
        GameKitCenter.Fsm.DestroyFsm(fsm);
        stateList.Clear();
        fsm = null;
    }

    public IDataNode ParseNode(int index = 0)
    {
        if (GameKitCenter.Dialog.CurrentTree == null)
        {
            Log.Warning("Cached Current Tree is Empty");
            return null;
        }
        IDataNode nextNode = GameKitCenter.Dialog.CurrentTree.GetChildNode(index);
        return ParseNode(nextNode);
    }

    public IDataNode ParseNode(IDataNode currentNode)
    {
        if (GameKitCenter.Dialog.CurrentTree == null)
        {
            Log.Warning("Cached Current Tree is Empty");
            return null;
        }

        IDataNode nextNode = null;
        if (currentNode == null)
        {
            ReachTheEndOfConversation();
            return null;
        }

        DialogDataNodeVariable tempDialogData = currentNode.GetData<DialogDataNodeVariable>();
        if (tempDialogData.IsFunctional)
        {
            if (tempDialogData.IsCompleter)
            {
                for (int j = 0; j < tempDialogData.CompleteConditons.Count; j++)
                {
                    GameKitCenter.Dialog.CurrentTree.LocalConditions[tempDialogData.CompleteConditons[j]] = true;
                }
            }

            if (tempDialogData.IsDivider)
            {
                bool isComplete = true;
                for (int j = 0; j < tempDialogData.DividerConditions.Count; j++)
                {
                    if (!GameKitCenter.Dialog.CurrentTree.LocalConditions[tempDialogData.DividerConditions[j]])
                    {
                        isComplete = false;
                        break;
                    }
                }

                if (isComplete)
                {
                    nextNode = GameKitCenter.Dialog.CurrentTree.GetChildNode(0);
                }
                else
                {
                    nextNode = GameKitCenter.Dialog.CurrentTree.GetChildNode(1);
                }
            }
        }
        else
        {
            if (currentNode.IsBranch)
            {
                nextNode = currentNode;
            }
            else
            {
                nextNode = currentNode;
            }
        }
        return nextNode;
    }

    private void ReachTheEndOfConversation()
    {
        Log.Info("Reach The End Of Conversation.");
        fsm.SetData<VarBoolean>(DialogStateUtility.DIALOG_START_ID, false);
        GameKitCenter.Dialog.CurrentTree = null;
    }

    public void UpdateDialogUI(IDataNode node, bool useTypeWriter = true, UnityAction callback = null)
    {

        TextAnimatorPlayer.useTypeWriter = useTypeWriter;
        DialogDataNodeVariable data = node.GetData<DialogDataNodeVariable>();
        if (node == null || data.Speaker == "Default")
            return;

        if (data.Speaker == ">>")
            t_SpeakerName.text = "";
        else if (data.Speaker == "??")
            t_SpeakerName.text = "未知";
        else
            t_SpeakerName.text = data.Speaker;
        t_Contents.text = data.Contents;

        if (data.Speaker != ">>")
        {
            Character character = characterPool.GetData<Character>(data.Speaker.Correction());
            if (m_CurrentCharacter != character)
            {
                m_CurrentCharacter = character;
                speakerAnimator.SetTrigger("FadeIn");
            }
            // RuntimeAnimatorController charaAnimator = FindAnimator(character.idName);
            uI_Character.avatar.sprite = character.GetMood(data.MoodName).avatar;
            // character.animator.runtimeAnimatorController = charaAnimator;
        }
    }


    public void UpdateOptionUI(UnityAction callback = null)
    {
        IDialogOptionSet optionSet = GameKitCenter.Dialog.CreateOptionSet(GameKitCenter.Dialog.CurrentTree.CurrentNode);
        if (optionSet != null)
        {
            uI_Response.UpdateOptions(optionSet);
            ShowResponse(callback);
        }
    }

    public void Resume()
    {
        OnResume();

    }

    public void Pause()
    {
        OnPause();
    }

    public void InternalVisible(bool status)
    {
        base.InternalSetVisible(status);
    }

    private void OnStartDialogSuccess(object sender, GameEventArgs e)
    {
        // Resume();
        fsm.SetData<VarBoolean>(DialogStateUtility.DIALOG_START_ID, true);
    }

    #region DiceSystem

    public void InitDiceSystem()
    {
        uI_DiceSystem.OnInit();
        diceAnimator.SetTrigger("FadeIn");
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
        uI_DiceSystem.ResetDicePosition();
        uI_DiceSystem.AddDiceFaceToResultList();
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
