using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionPointerElement : ElementBase
{
	private GameObject _pointer;
	public override void OnInit()
	{
		_pointer = transform.GetChild(0).gameObject;
		_pointer.SetActive(false);
	}
	
	private void OnDestroy()
	{
		GameKitCenter.Element.RemoveElement(this);
	}

	public override void OnHighlightEnter()
	{
		base.OnHighlightEnter();
		_pointer.SetActive(true);
	}

	public override void OnHighlightExit()
	{
		base.OnHighlightExit();
		_pointer.SetActive(false);
	}
}
