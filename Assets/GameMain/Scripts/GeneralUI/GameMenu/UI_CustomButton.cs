using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_CustomButton : MonoBehaviour
{
	public Sprite normalSprite;
	public Sprite selectedSprite;
	//public Sprite clickedSprite;
	[Space]
	public Image image;
	
	private UnityAction onClicked;

	public void OnInit(UnityAction callback)
	{
		onClicked += callback;
		OnReleased();
	}
	
	public void OnClicked()
	{
		onClicked?.Invoke();
		//TODO 这里可以播放按钮动画
	}
	
	public void OnSelected()
	{
		image.sprite = selectedSprite;
	}

	public void OnReleased()
	{
		image.sprite = normalSprite;
	}
}
