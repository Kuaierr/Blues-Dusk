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
    private void Start()
    {
        // Tree tree = new Tree();
        // tree.ShowCurrentNode();
        // tree.AddToCurrent(new Node<HubNode>());
        // tree.Test();
        // DialogPhaser.Phase(testString);
        // dialogTree = new DialogTree();
        // BuildTree();
        // VisitTree();
        isActive = true;
        dialogTree = DialogManager.instance.CreateTree(dialogAsset.contents);

        // Node<Dialog> node = dialogTree.startNode as Node<Dialog>;
        // uI_DialogSystem.speakerName.text = node.nodeEntity.speaker;
        // uI_DialogSystem.contents.text = node.nodeEntity.contents;
        // uI_DialogSystem.avatar.sprite = entitiesPool.FindCharacter(node.nodeEntity.speaker).moods.FirstOrDefault().avatar;
    }

    private void Update()
    {
        if (isActive == false)
            return;
        // VisitTree();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(dialogTree.currentNode.IsBranch)
            {
                List<Option> options = dialogTree.TryGetOption();
                if(options!=null)
                {
                    // uI_DialogSystem.UpdateOptions(options);
                }
            }

            Node<Dialog> node = dialogTree.PhaseNext();
            if (node == null)
                return;
            uI_DialogSystem.speakerName.text = node.nodeEntity.speaker;
            uI_DialogSystem.contents.text = node.nodeEntity.contents;
            uI_DialogSystem.avatar.sprite = entitiesPool.FindCharacter(node.nodeEntity.speaker).moods.FirstOrDefault().avatar;
        }
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
            Debug.Log(dialogTree.currentNode);
            dialogTree.currentNode = dialogTree.currentNode.Sons.FirstOrDefault();
        }
    }
}
