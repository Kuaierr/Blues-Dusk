using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameKit.DataStructure;
using DialogNode = GameKit.DataStructure.Node<Dialog>;
public delegate void PosteriorLink<T>(Node<T> nodeA, string nodeB) where T : NodeType;
public class LinkCommand<T> : Command where T : NodeType
{
    public Node<T> nodeA;
    public string targetNode;
    public PosteriorLink<T> command;

    public LinkCommand(Node<T> nodeA, string targetNode, PosteriorLink<T> command)
    {
        this.command = command;
        this.nodeA = nodeA;
        this.targetNode = targetNode;
    }

    public void Excute()
    {
        command.Invoke(nodeA, targetNode);
    }
}

public class DialogTree : ITree
{
    public List<INode> declaredNodes;
    public List<INode> branchNodes;
    public Queue<Command> linkBuffer;
    public INode rootNode;
    public INode currentNode;
    public INode startNode;
    public DialogTree(INode rootNode)
    {
        this.rootNode = rootNode;
        (rootNode as Node<Dialog>).nodeEntity = new Dialog();
        currentNode = this.rootNode;
        branchNodes = new List<INode>();
        declaredNodes = new List<INode>();
        linkBuffer = new Queue<Command>();
    }

    public DialogTree()
    {
        this.rootNode = new Node<Dialog>(this, true);
        (rootNode as Node<Dialog>).nodeEntity = new Dialog();
        startNode = this.rootNode;
        currentNode = this.rootNode;
        branchNodes = new List<INode>();
        declaredNodes = new List<INode>();
        linkBuffer = new Queue<Command>();
    }

    public void AddFromLast<T>(Node<T> node) where T : NodeType
    {
        AddFrom(node, currentNode as Node<T>);
    }

    public void RecordBranch<T>(Node<T> node) where T : NodeType
    {
        if (!branchNodes.Contains(node))
            branchNodes.Add(node);
    }

    public void RecordDeclaredNode<T>(Node<T> node) where T : NodeType
    {
        if (!declaredNodes.Contains(node))
            declaredNodes.Add(node);
    }

    public bool ContainsDeclaredNode(string name)
    {
        foreach (var node in declaredNodes)
        {
            if (node.Id == name)
                return true;
        }
        return false;
    }

    public void CachedLinkToDeclared<T>(Node<T> srcnode, string name) where T : NodeType
    {
        LinkCommand<T> command = new LinkCommand<T>(srcnode, name, LinkToDeclared);
        linkBuffer.Enqueue(command);
    }

    public void CachedLinkFromDeclared<T>(Node<T> srcnode, string name) where T : NodeType
    {
        LinkCommand<T> command = new LinkCommand<T>(srcnode, name, LinkFromDeclared);
        linkBuffer.Enqueue(command);
    }

    public void LinkToDeclared<T>(Node<T> srcnode, string name) where T : NodeType
    {
        foreach (var node in declaredNodes)
        {
            if (node.Id == name)
            {
                AddTo(srcnode, (node as Node<T>));
                return;
            }
        }
    }

    public void LinkFromDeclared<T>(Node<T> srcnode, string name) where T : NodeType
    {
        foreach (var node in declaredNodes)
        {
            if (node.Id == name)
            {
                AddFrom(srcnode, (node as Node<T>));
                return;
            }
        }
    }

    
    public void AddFrom<T>(Node<T> target, Node<T> parent) where T : NodeType
    {
        if (parent.Sons.Count > 0)
        {
            foreach (Node<T> sibling in parent.Sons)
            {
                sibling.Siblings.Add(target);
            }
        }
        parent.Sons.Add(target);
    }

    public void AddTo<T>(Node<T> target, Node<T> son) where T : NodeType
    {
        if (target.Sons.Count > 0)
        {
            foreach (Node<T> sibling in target.Sons)
            {
                sibling.Siblings.Add(son);
            }
        }
        target.Sons.Add(son);
    }

    public void ExcuteAllBufferCommand<T>() where T : NodeType
    {
        foreach (var command in linkBuffer)
        {
            (command as LinkCommand<T>).Excute();
        }
        linkBuffer.Clear();
    }

    public void OnBuildEnd()
    {
        currentNode = startNode;
    }

    public void Reset()
    {
        currentNode = startNode;
    }

    public List<Option> TryGetOption()
    {
        if (currentNode.Sons.Count > 1)
        {
            List<Option> options = DialogSelection.CreateSelection(currentNode.Sons);
            return options;
        }
        return null;
    }

    public Node<Dialog> PhaseNext(int index = 0)
    {
        Debug.Log(currentNode + ", and Son Counts: " + currentNode.Sons.Count);
        if (currentNode.Sons.Count == 0 || index < 0 || index > currentNode.Sons.Count)
            return null;

        currentNode = currentNode.Sons[index] as Node<Dialog>;
        return currentNode as Node<Dialog>;        
    }
}