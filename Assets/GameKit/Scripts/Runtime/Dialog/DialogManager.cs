using System.Collections;
using System.Collections.Generic;
using GameKit;
using System.Linq;
using GameKit.DataStructure;
public class DialogDManager : SingletonBase<DialogDManager>
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

    public DialogTree AcquireActiveTree()
    {
        if (dialogTrees.Count > 0)
            return dialogTrees.Peek();
        return null;
    }

    public DialogTree AcquireTree(string title)
    {
        if (dialogTrees.Count > 0)
        {
            foreach (var tree in dialogTrees)
            {
                if (tree.title == title)
                {
                    return tree;
                }
            }
        }
        return null;
    }

    public void ClearTree()
    {
        if (dialogTrees.Count > 0)
        {
            dialogTrees.Clear();
        }
    }

    public DialogTree CreateTree(string title, string text)
    {
        DialogTree dialogTree = new DialogTree(title);
        List<string> lines = new List<string>(text.Replace(((char)13).ToString(), "").Replace("\t", "").Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries));
        foreach (var line in lines)
        {
            Node<Dialog> node = new Node<Dialog>(dialogTree);
            node.nodeEntity = new Dialog();
            DialogPhaser.PhaseNode(node, line);
        }
        dialogTree.ExcuteAllBufferCommand<Dialog>();
        dialogTree.OnBuildEnd();
        EnqueueTree(dialogTree);
        return dialogTree;
    }
}