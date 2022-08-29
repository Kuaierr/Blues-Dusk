using System.Collections.Generic;
using UnityEngine;
using Febucci.UI;
using GameKit.DataStructure;
using GameKit;
using GameKit.Fsm;
using UnityEngine.Events;
using GameKit.QuickCode;
using UnityGameKit.Runtime;
[DisallowMultipleComponent]
[AddComponentMenu("GameKit/Dialog System")]
public class DialogSystem : MonoSingletonBase<DialogSystem>
{
    private DialogTree dialogTree;
    private UI_DialogSystem uI_DialogSystem;
    private TextAnimatorPlayer textAnimatorPlayer;
    private Character currentCharacter;
    private CharacterPool characterPool;
    private List<RuntimeAnimatorController> charaAnimators = new List<RuntimeAnimatorController>();
    private bool isOptionShowing = false;
    private bool isInSelection = false;
    private bool isTextShowing = false;

    private IFsm<DialogSystem> fsm;
    private List<FsmState<DialogSystem>> stateList;

    private void Start()
    {
        uI_DialogSystem = UIManager.instance.GetUI<UI_DialogSystem>("UI_DialogSystem");
        textAnimatorPlayer = uI_DialogSystem?.textAnimatorPlayer;
        AddressableManager.instance.GetAssetAsyn<CharacterPool>("Character Pool", (characterPool) =>
        {
            this.characterPool = characterPool;
        });
        LoadAnimator();
    }

    private void CreateFsm()
    {
        stateList.Add(new DialogIdleState());
        stateList.Add(new DialogTalkState());
        stateList.Add(new DialogChoiceState());
        stateList.Add(new DialogChoiceDicerollState());
        fsm = GameKitCenter.Fsm.CreateFsm<DialogSystem>(gameObject.name, this, stateList);
        fsm.Start<DialogIdleState>();
    }

    private void DestroyFsm()
    {
        GameKitCenter.Fsm.DestroyFsm(fsm);
        stateList.Clear();
        fsm = null;
    }

    public void StartDialog(string title, string dialogText)
    {
        Debug.Log($"Start Dialog");
        isTextShowing = false;
        dialogTree = DialogDManager.instance.CreateTree(title, dialogText);
        dialogTree.Reset();
        uI_DialogSystem.Show();
        SelectNextNode();
    }

    private void Update()
    {
        if (dialogTree == null)
            return;

        // 正在选择中 
        if (!isOptionShowing && isInSelection)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                int choiceIndex = uI_DialogSystem.GetSelection();
                Node<Dialog> nextNode = GetNextNode(choiceIndex);
                SelectNextNode();
                uI_DialogSystem.HideResponse();
                isInSelection = false;
                return;
            }
        }

        // 不在选择中
        if (!isInSelection)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isTextShowing == false)
                {
                    SelectNextNode();
                }
                else
                    InterruptTextDisplay();
            }
        }

        uI_DialogSystem.indicator.SetActive(!isTextShowing);
    }

    private void UpdateChoiceUI()
    {
        List<Option> options = dialogTree.GetOptions();
        if (options != null)
        {
            isOptionShowing = true;
            isInSelection = true;
            uI_DialogSystem.UpdateOptions(options);
            uI_DialogSystem.ShowResponse(() =>
            {
                isOptionShowing = false;
            });

        }
    }

    private void UpdateUI(Node<Dialog> node)
    {
        // Debug.Log($"Update Character UI");
        if (node == null || node.nodeEntity.speaker == "Default")
            return;

        if (node.nodeEntity.speaker == ">>")
            uI_DialogSystem.speakerName.text = "";
        else if (node.nodeEntity.speaker == "??")
            uI_DialogSystem.speakerName.text = "未知";
        else
            uI_DialogSystem.speakerName.text = node.nodeEntity.speaker;
        uI_DialogSystem.contents.text = node.nodeEntity.contents;

        if (node.nodeEntity.speaker != ">>")
        {
            Character character = characterPool.FindCharacter(node.nodeEntity.speaker.Correction());
            if (currentCharacter != character)
            {
                currentCharacter = character;
                uI_DialogSystem.speakerAnimator.SetTrigger("FadeIn");
            }
            RuntimeAnimatorController charaAnimator = FindAnimator(character.idName);
            uI_DialogSystem.character.avatar.sprite = character.GetMood(node.nodeEntity.moodName).avatar;
            uI_DialogSystem.character.animator.runtimeAnimatorController = charaAnimator;
        }

    }

    private void PhaseNode(Node<Dialog> dialogNode, UnityAction onTextShowed = null)
    {
        UpdateUI(dialogNode);
        textAnimatorPlayer.onTypewriterStart.AddListener(() =>
        {
            isTextShowing = true;
        });
        textAnimatorPlayer.onTextShowed.AddListener(() =>
        {
            isTextShowing = false;
        });

        if (onTextShowed != null)
            textAnimatorPlayer.onTextShowed.AddListener(onTextShowed);

        textAnimatorPlayer.StartShowingText();
    }

    private Node<Dialog> GetNextNode(int index = 0)
    {
        if (dialogTree.currentNode.IsLeaf || index < 0 || index >= dialogTree.currentNode.Sons.Count)
            return null;
        dialogTree.currentNode = dialogTree.currentNode.Sons[index];
        return dialogTree.currentNode as Node<Dialog>;
    }
    private Node<Dialog> SelectNextNode(Node<Dialog> currentNode)
    {
        Node<Dialog> nextNode = null;
        if (currentNode == null)
        {
            ReachTheEndOfConversation();
            return null;
        }

        if (currentNode.nodeEntity.IsFunctional)
        {
            if (currentNode.nodeEntity.IsCompleter)
            {
                for (int j = 0; j < currentNode.nodeEntity.completeConditons.Count; j++)
                {
                    dialogTree.LocalConditions[currentNode.nodeEntity.completeConditons[j]] = true;
                }
            }

            if (currentNode.nodeEntity.IsDivider)
            {
                bool isComplete = true;
                for (int j = 0; j < currentNode.nodeEntity.dividerConditions.Count; j++)
                {
                    if (!dialogTree.LocalConditions[currentNode.nodeEntity.dividerConditions[j]])
                    {
                        isComplete = false;
                        break;
                    }
                }

                if (isComplete)
                {
                    nextNode = GetNextNode(0);
                    // PhaseNode(GetNextNode(0));
                }
                else
                {
                    nextNode = GetNextNode(1);
                    // PhaseNode(GetNextNode(1));
                }
            }
        }
        else
        {
            if (currentNode.IsBranch)
            {
                nextNode = currentNode;
                // PhaseNode(currentNode, UpdateChoiceUI);
            }
            else
            {
                nextNode = currentNode;
                // PhaseNode(currentNode);
            }
        }
        return nextNode;
    }
    private void SelectNextNode(int index = 0)
    {
        Node<Dialog> nextNode = GetNextNode(index);
        SelectNextNode(nextNode);
    }

    private void InterruptTextDisplay()
    {
        textAnimatorPlayer.SkipTypewriter();
        isTextShowing = false;
    }

    private void ReachTheEndOfConversation()
    {
        Debug.Log("Reach The End Of Conversation.");
        dialogTree.Clear();
        dialogTree = null;
        uI_DialogSystem.Hide();
        DialogDManager.instance.DequeueTree();
    }

    private void LoadAnimator()
    {
        if (charaAnimators == null || charaAnimators.Count == 0)
        {
            AddressableManager.instance.GetAssetsAsyn<RuntimeAnimatorController>(new List<string> { "Character Animator" }, callback: (IList<RuntimeAnimatorController> assets) =>
            {
                charaAnimators = new List<RuntimeAnimatorController>(assets);
            });
        }
    }

    private RuntimeAnimatorController FindAnimator(string name)
    {
        for (int i = 0; i < charaAnimators.Count; i++)
        {
            if (charaAnimators[i].name == "AC_" + name)
            {
                return charaAnimators[i];
            }
        }
        return null;
    }
}
