using System.Collections;
using System.Collections.Generic;
using GameKit;
using GameKit.Event;
using UnityEngine;

public class OnOpenPlayerBackpackEventArgs : GameEventArgs
{
	public static readonly int EnentId = typeof(OnOpenPlayerBackpackEventArgs).GetHashCode();

	public OnOpenPlayerBackpackEventArgs()
	{
		UserData = null;
	}

	public object UserData
	{
		get;
		private set;
	}

	public override int Id
	{
		get
		{
			return EnentId;
		}
	}

	public static OnOpenPlayerBackpackEventArgs Create(object userData)
	{
		OnOpenPlayerBackpackEventArgs onOpenPlayerBackpackEventArgs =
			ReferencePool.Acquire<OnOpenPlayerBackpackEventArgs>();
		onOpenPlayerBackpackEventArgs.UserData = userData;
		return onOpenPlayerBackpackEventArgs;
	}

	public override void Clear()
	{
		UserData = null;
	}
}
