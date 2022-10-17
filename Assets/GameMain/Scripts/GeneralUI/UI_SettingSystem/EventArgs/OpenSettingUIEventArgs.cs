using System.Collections;
using System.Collections.Generic;
using GameKit.Event;
using UnityEngine;

public class OpenSettingUIEventArgs : GameEventArgs
{
	public static readonly int EventId = typeof(OpenSettingUIEventArgs).GetHashCode();

	public OpenSettingUIEventArgs()
	{
		UserData = null;
	}
	
	public object UserData { get; private set; }
	
	public override void Clear()
	{
		UserData = null;
	}

	public static OpenSettingUIEventArgs Create(object userData)
	{
		var args =  GameKit.ReferencePool.Acquire<OpenSettingUIEventArgs>();
		args.UserData = userData;
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
