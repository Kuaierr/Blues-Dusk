using System.Collections;
using System.Collections.Generic;
using GameKit;
using GameKit.Event;
using UnityEngine;

public class OnOpenDiceInventoryEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(OnOpenDiceInventoryEventArgs).GetHashCode();

    public OnOpenDiceInventoryEventArgs()
    {
        UserData = null;
    }

    public static OnOpenDiceInventoryEventArgs Create(object userData)
    {
        OnOpenDiceInventoryEventArgs args = ReferencePool.Acquire<OnOpenDiceInventoryEventArgs>();
        args.UserData =  userData;
        return args;
    }

    public object UserData { get; private set; }

    public override void Clear()
    {
        UserData = null;
    }

    public override int Id
    {
        get { return EventId; }
    }
}