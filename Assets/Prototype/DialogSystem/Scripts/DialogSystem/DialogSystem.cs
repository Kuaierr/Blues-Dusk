using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GameKit.DataStructure;

public class DialogSystem : MonoBehaviour
{
    public DialogAsset dialogAsset;
    public DialogTree dialogTree;
    public List<string> lines;
    public UI_DialogSystem uI_DialogSystem;
    public EntitiesPool entitiesPool;
    public bool isActive = false;
    private bool isPaused = false;
    private void Start()
    {
        isActive = true;
        dialogTree = DialogManager.instance.CreateTree(dialogAsset.contents);
        VisitTree();
        dialogTree.Reset();
    }

    private void Update()
    {
        if (isActive == false)
            return;

        if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isPaused = false;
                int choiceIndex = uI_DialogSystem.GetSelection();
                Node<Dialog> node = dialogTree.PhaseNext(choiceIndex);
                uI_DialogSystem.HideResponse();
                UpdateUI(node);
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogTree.currentNode.IsBranch)
            {
                List<Option> options = dialogTree.TryGetOption();
                if (options != null)
                {
                    uI_DialogSystem.UpdateOptions(options);
                    uI_DialogSystem.ShowResponse();
                    isPaused = true;
                }
            }
            else
            {
                Node<Dialog> node = dialogTree.PhaseNext();
                UpdateUI(node);
            }
        }
    }


    private void UpdateUI(Node<Dialog> node)
    {
        if (node == null)
            return;
        uI_DialogSystem.speakerName.text = node.nodeEntity.speaker;
        uI_DialogSystem.contents.text = node.nodeEntity.contents;
        uI_DialogSystem.avatar.sprite = entitiesPool.FindCharacter(node.nodeEntity.speaker).moods.FirstOrDefault().avatar;
    }

    private void BuildTree()
    {
        lines = new List<string>(dialogAsset.contents.Split('\n'));
        lines = lines.Distinct().ToList();
        lines.RemoveAt(0);

        foreach (var line in lines)
        {
            Node<Dialog> node = new Node<Dialog>(dialogTree);
            node.nodeEntity = new Dialog();
            DialogPhaser.PhaseNode(node, line);
        }
        dialogTree.ExcuteAllBufferCommand<Dialog>();
        dialogTree.OnBuildEnd();
    }

    private void VisitTree()
    {
        int deadloopPreventer = 0;
        while (true)
        {
            if (deadloopPreventer >= 1000)
            {
                Debug.Log("reach deadloop");
                break;
            }
            deadloopPreventer++;

            if (dialogTree.currentNode.IsLeaf)
                return;
            Debug.Log(dialogTree.currentNode + " and son counts: " + dialogTree.currentNode.Sons.Count);
            if(dialogTree.currentNode.Sons.Count>1)
            {
                foreach (var son in dialogTree.currentNode.Sons)
                {
                    Debug.Log("----The son: " + son);   
                }
            }
            dialogTree.currentNode = dialogTree.currentNode.Sons.FirstOrDefault();
        }
    }
}
