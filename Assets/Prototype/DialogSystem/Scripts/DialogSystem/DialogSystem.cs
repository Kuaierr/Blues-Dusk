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

    private bool isPaused = false;
    private TextAnimatorPlayer textAnimatorPlayer;
    private bool isTextShowing = false;

    [Space]
    [Header("Debug")]
    public List<string> lines = new List<string>();
    private void Start()
    {
        IsActive = true;
        isTextShowing = false;
        textAnimatorPlayer = uI_DialogSystem.textAnimatorPlayer;

        dialogTree = DialogManager.instance.CreateTree(dialogAsset.contents, out List<string> slice);
        lines = slice;
        dialogTree.Reset();
        ExcuteTextDisplay();
    }

    private void Update()
    {
        if (IsActive == false)
            return;

        if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isPaused = false;
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
    }

    private void UpdateChoiceUI()
    {

        Debug.Log($"Call UpdateChoiceUI");
        List<Option> options = dialogTree.TryGetOption();
        if (options != null)
        {
            Debug.Log($"Show Choice UI");
            uI_DialogSystem.UpdateOptions(options);
            isPaused = true;
            uI_DialogSystem.ShowResponse();
        }
    }

    private void UpdateUI(Node<Dialog> node)
    {
        if (node == null || node.nodeEntity.speaker == "Default")
            return;
        uI_DialogSystem.speakerName.text = node.nodeEntity.speaker;
        uI_DialogSystem.contents.text = node.nodeEntity.contents;
        uI_DialogSystem.avatar.sprite = entitiesPool.FindCharacter(node.nodeEntity.speaker).moods.FirstOrDefault().avatar;
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

        if (nextNode.IsBranch)
        {
            Debug.Log($"Branch Point");
            PhaseNode(nextNode, UpdateChoiceUI);
        }
        else
        {
            PhaseNode(nextNode);
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
    }
}
