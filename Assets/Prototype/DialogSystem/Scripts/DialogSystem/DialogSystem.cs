using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Febucci.UI;
using GameKit.DataStructure;
using UnityEngine.Events;

public class DialogSystem : MonoBehaviour
{
    public static bool IsActive = false;
    public DialogAsset dialogAsset;
    public DialogTree dialogTree;
    public UI_DialogSystem uI_DialogSystem;
    public EntitiesPool entitiesPool;
    [SerializeField] private List<RuntimeAnimatorController> charaAnimators = new List<RuntimeAnimatorController>();
    private bool isOptionShowing = false;
    private TextAnimatorPlayer textAnimatorPlayer;
    private bool isTextShowing = false;

    [Space]
    [Header("Debug")]
    public List<string> lines = new List<string>();
    private void Start()
    {
        IsActive = true;
        textAnimatorPlayer = uI_DialogSystem.textAnimatorPlayer;
        // StartDialog(dialogAsset.contents);
        LoadAnimator();
    }
    public void StartDialog(string dialogText)
    {
        Debug.Log($"Start Dialog");
        isTextShowing = false;
        dialogTree = DialogManager.instance.CreateTree(dialogText, out List<string> slice);
        lines = slice;
        dialogTree.Reset();
        uI_DialogSystem.Show();
        ExcuteTextDisplay();
    }

    private void Update()
    {
        if (IsActive == false || dialogTree == null)
            return;

        if (isOptionShowing)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isOptionShowing = false;
                int choiceIndex = uI_DialogSystem.GetSelection();
                ExcuteTextDisplay(choiceIndex);
                uI_DialogSystem.HideResponse();
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTextShowing == false)
            {
                ExcuteTextDisplay();
            }
            else
                InterruptTextDisplay();
        }
        uI_DialogSystem.indicator.SetActive(!isTextShowing);
    }

    private void UpdateChoiceUI()
    {
        List<Option> options = dialogTree.TryGetOption();
        if (options != null)
        {
            Debug.Log($"Show Choice UI");
            uI_DialogSystem.UpdateOptions(options);
            isOptionShowing = true;
            uI_DialogSystem.ShowResponse();
        }
    }

    private void UpdateUI(Node<Dialog> node)
    {
        Debug.Log($"Update Character UI");
        if (node == null || node.nodeEntity.speaker == "Default")
            return;
        uI_DialogSystem.speakerName.text = node.nodeEntity.speaker;
        uI_DialogSystem.contents.text = node.nodeEntity.contents;
        Character character = entitiesPool.FindCharacter(node.nodeEntity.speaker.Correction());

        RuntimeAnimatorController charaAnimator = FindAnimator(character.idName);
        uI_DialogSystem.character.avatar.sprite = character.GetMood(node.nodeEntity.moodName).avatar;
        uI_DialogSystem.character.animator.runtimeAnimatorController = charaAnimator;
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

    private Node<Dialog> GetNode(int index = 0)
    {
        if (dialogTree.currentNode.IsLeaf || index < 0 || index > dialogTree.currentNode.Sons.Count)
            return null;
        dialogTree.currentNode = dialogTree.currentNode.Sons[index];
        return dialogTree.currentNode as Node<Dialog>;
    }

    private void ExcuteTextDisplay(int index = 0)
    {
        Node<Dialog> nextNode = GetNode(index);
        if (nextNode == null)
        {
            ReachTheEndOfConversation();
            return;
        }

        if (nextNode.nodeEntity.IsFunctional)
        {
            if (nextNode.nodeEntity.IsCompleter)
            {
                for (int j = 0; j < nextNode.nodeEntity.completeConditons.Count; j++)
                {
                    dialogTree.LocalConditions[nextNode.nodeEntity.completeConditons[j]] = true;
                }
            }

            if (nextNode.nodeEntity.IsDivider)
            {
                bool isComplete = true;
                for (int j = 0; j < nextNode.nodeEntity.dividerConditions.Count; j++)
                {
                    if (!dialogTree.LocalConditions[nextNode.nodeEntity.dividerConditions[j]])
                    {
                        isComplete = false;
                        break;
                    }
                }
                
                if (isComplete)
                {
                    PhaseNode(GetNode(0));
                }
                else
                {
                    PhaseNode(GetNode(1));
                }
            }
        }
        else
        {
            if (nextNode.IsBranch)
            {
                PhaseNode(nextNode, UpdateChoiceUI);
            }
            else
            {
                PhaseNode(nextNode);
            }
        }


    }

    private void InterruptTextDisplay()
    {
        textAnimatorPlayer.SkipTypewriter();
        isTextShowing = false;
    }

    private void ReachTheEndOfConversation()
    {
        Debug.Log("Reach the end of conversation.");
        dialogTree.Clear();
        dialogTree = null;
        uI_DialogSystem.Hide();
    }

    private void LoadAnimator()
    {
        if (charaAnimators == null || charaAnimators.Count == 0)
        {
            AddressableManager.instance.GetAssetsAsyn<RuntimeAnimatorController>(new List<string> { "CharacterAnimator" }, callback: (IList<RuntimeAnimatorController> assets) =>
            {
                Debug.Log($"Load Animator");
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