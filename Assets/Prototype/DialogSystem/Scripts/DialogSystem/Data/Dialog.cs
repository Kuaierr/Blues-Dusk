using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameKit.DataStructure;
public delegate void NodeEvent<in T>(T obj);
public delegate void NodeEvent();
public enum SpritePos
{
    Left,
    Right
}
public sealed class Dialog : NodeType
{
    public Sprite sprite;
    public SpritePos pos;
    public string speaker;
    public string contents;
    public string nodeName;
    public string conditionaName;
    public string branchOrder;

    public NodeEvent onEnter, onUpdate, onFinish, onWait, onExit;
    public Dialog()
    {
        this.speaker = "Default";
        this.contents = "Default";
    }

    public Dialog(string speaker, string contents)
    {
        this.speaker = speaker;
        this.contents = contents;
    }

    public void ClearEvents()
    {
        onEnter = (NodeEvent)System.Delegate.RemoveAll(onEnter, onEnter);
    }

    public override void OnEnter()
    {
        onEnter?.Invoke();
    }

    public override void OnUpdate()
    {
        onUpdate?.Invoke();
    }
    public override void OnFinish()
    {
        onFinish?.Invoke();
    }

    public override void OnWait()
    {
        onWait?.Invoke();
    }

    public override void OnExit()
    {
        onExit?.Invoke();
    }

    public override string ToString()
    {
        return speaker + ": " + contents;
    }


}
