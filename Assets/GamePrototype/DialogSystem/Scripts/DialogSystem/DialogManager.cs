using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameKit;
using System.Linq;
using GameKit.DataStructure;
public class DialogManager : SingletonBase<DialogManager>
{
    private Queue<DialogTree> dialogTrees = new Queue<DialogTree>();
    public void EnqueueTree(DialogTree tree)
    {
        if (!dialogTrees.Contains(tree))
        {
            dialogTrees.Enqueue(tree);
        }
    }

    public void DequeueTree()
    {
        if (dialogTrees.Count > 0)
        {
            dialogTrees.Dequeue();
        }
    }

    public DialogTree GetActiveTree()
    {
        if (dialogTrees.Count > 0)
            return dialogTrees.Peek();
        return null;
    }

    public DialogTree CreateTree(string text, out List<string> slice)
    {
        Debug.Log($"Create Tree");
        DialogTree dialogTree = new DialogTree();
        List<string> lines = new List<string>(text.Replace(((char)13).ToString(), "").Replace("\t", "").Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries));
        foreach (var line in lines)
        {
            Node<Dialog> node = new Node<Dialog>(dialogTree);
            node.nodeEntity = new Dialog();
            DialogPhaser.PhaseNode(node, line);
        }
        slice = lines;
        dialogTree.ExcuteAllBufferCommand<Dialog>();
        dialogTree.OnBuildEnd();
        EnqueueTree(dialogTree);
        return dialogTree;
    }

    public void TraverseTree()
    {
        DialogTree dialogTree = GetActiveTree();
        if (dialogTree == null)
        {
            Debug.LogWarning("Traverse failed, no active tree in queue.");
            return;
        }
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

            if (dialogTree.currentNode.Sons.Count > 1)
            {

            }
            else if (dialogTree.currentNode.Sons.Count == 1)
            {
                dialogTree.currentNode = dialogTree.currentNode.Sons.FirstOrDefault();
                // dialogTree.currentNode.OnEnter();
            }
        }
    }

    public void PhaseNext1()
    {
        DialogTree currentTree = GetActiveTree();
        if (currentTree == null)
            return;

        if (currentTree.currentNode.Sons.Count > 1)
        {
            List<Option> options = DialogSelection.CreateSelection(currentTree.currentNode.Sons);
        }
        else
        {
            Node<Dialog> nextNode = currentTree.currentNode.Next as Node<Dialog>;

        }
    }
}