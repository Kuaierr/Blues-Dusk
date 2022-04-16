using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameKit.DataStructure;

public delegate void CMD<T>(Node<T> nodeA, Node<T> nodeB) where T : NodeEntity;
public delegate void CMD(INode nodeA, INode nodeB);
public class LinkCommand<T> : Command where T : NodeEntity
{
    public Node<T> nodeA;
    public Node<T> nodeB;
    public CMD<T> command;

    public LinkCommand(Node<T> nodeA, Node<T> nodeB, CMD<T> command)
    {
        this.command = command;
        this.nodeA = nodeA;
        this.nodeB = nodeB;
    }

    public void Excute()
    {
        command.Invoke(nodeA, nodeB);
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
        currentNode = this.rootNode;
        branchNodes = new List<INode>();
        declaredNodes = new List<INode>();
        linkBuffer = new Queue<Command>();
    }

    public void AddFromLast<T>(Node<T> node) where T : NodeEntity
    {
        AddFrom(node, currentNode as Node<T>);
        currentNode = node;
    }
    // public void AddFromBranch<T>(Node<T> node) where T : NodeEntity
    // {
    //     AddFrom(node, linkBuffer.Peek());
    // }

    public void AddFrom<T>(Node<T> node, Node<T> parent) where T : NodeEntity
    {
        parent.Sons.Add(node);
        // if (parent.Sons.Count > 1)
        // {
        //     foreach (INode sibling in parent.Sons)
        //     {
        //         sibling.Siblings.Add(node);
        //     }
        // }
    }

    public void AddTo<T>(Node<T> node, Node<T> son) where T : NodeEntity
    {
        node.Sons.Add(son);
        // if (node.Sons.Count > 1)
        // {
        //     foreach (INode sibling in node.Sons)
        //     {
        //         sibling.Siblings.Add(son);
        //     }
        // }
    }

    public void RecordBranch<T>(Node<T> node) where T : NodeEntity
    {
        if (!branchNodes.Contains(node))
            branchNodes.Add(node);

        // if (!linkBuffer.Contains(node))
        //     linkBuffer.Push(node);
    }

    public void RecordDeclaredNode<T>(Node<T> node) where T : NodeEntity
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

    public bool LinkToDeclared<T>(Node<T> srcnode, string name) where T : NodeEntity
    {
        foreach (var node in declaredNodes)
        {
            if (node.Id == name)
            {
                LinkCommand<T> command = new LinkCommand<T>(srcnode, (node as Node<T>), AddTo);
                linkBuffer.Enqueue(command);
                // AddTo(srcnode, (node as Node<T>));
                return true;
            }
        }
        return false;
    }

    public bool LinkFromDeclared<T>(Node<T> srcnode, string name) where T : NodeEntity
    {
        foreach (var node in declaredNodes)
        {
            if (node.Id == name)
            {
                LinkCommand<T> command = new LinkCommand<T>(srcnode, (node as Node<T>), AddFrom);
                linkBuffer.Enqueue(command);
                // AddFrom(srcnode, (node as Node<T>));
                return true;
            }
        }
        return false;
    }

    public void ExcuteAllBufferCommand<T>() where T : NodeEntity
    {
        foreach (var command in linkBuffer)
        {
            (command as LinkCommand<T>).Excute();
        }
        linkBuffer.Clear();
    }

    public void OnBuildEnd()
    {
        currentNode = rootNode;
    }

    public void ExcuteCommandBuffer()
    {
        // linkBuffer.Peek().Invoke();
    }

}