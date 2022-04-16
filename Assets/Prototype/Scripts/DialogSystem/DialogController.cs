using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GameKit.DataStructure;

public class DialogController : MonoBehaviour
{
    public DialogAsset dialogAsset;
    public DialogTree dialogTree;
    public List<string> lines;
    private void Start()
    {
        // Tree tree = new Tree();
        // tree.ShowCurrentNode();
        // tree.AddToCurrent(new Node<HubNode>());
        // tree.Test();
        // DialogPhaser.Phase(testString);
        dialogTree = new DialogTree();
        BuildTree();
        VisitTree();
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
        while (!dialogTree.currentNode.IsLeaf)
        {
            if (deadloopPreventer >= 1000)
            {
                Debug.Log("reach deadloop");
                break;
            }
            Debug.Log(dialogTree.currentNode);
            dialogTree.currentNode = dialogTree.currentNode.Sons.FirstOrDefault();
            deadloopPreventer++;
        }
    }
}
