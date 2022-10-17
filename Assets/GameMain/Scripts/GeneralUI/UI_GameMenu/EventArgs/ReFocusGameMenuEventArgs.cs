using System.Collections;
using System.Collections.Generic;
using GameKit;
using UnityEngine;

public class ReFocusGameMenuEventArgs : GameKit.Event.GameEventArgs
{
	public static readonly int EventId = typeof(ReFocusGameMenuEventArgs).GetHashCode();

	public ReFocusGameMenuEventArgs()
	{
		UserData = null;
	}
	
	public object UserData { get; private set; }
	
	public override void Clear()
	{
		UserData = null;
	}

	public static ReFocusGameMenuEventArgs Create(object user)
	{
		ReFocusGameMenuEventArgs args = ReferencePool.Acquire<ReFocusGameMenuEventArgs>();
		args.UserData = user;
		return args;
	}

	public override int Id
	{
		get
		{
			return EventId;
		}
	}
}
