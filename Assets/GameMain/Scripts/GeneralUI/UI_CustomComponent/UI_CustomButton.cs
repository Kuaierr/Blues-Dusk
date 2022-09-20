using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CustomButton : UI_CustomBase, IPointerClickHandler
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
		if(selectedSprite != null)
			image.sprite = selectedSprite;
	}

	public void OnReleased()
	{
		if(normalSprite != null)
			image.sprite = normalSprite;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		//TODO 如果通过鼠标操作，需要给管理者发信息，同步目前的选择，防止和键盘手柄的冲突
		//TODO 可以设置一个接口，用于ICustomButtomGroup
		Debug.Log("Clicked");
		OnClicked();
	}
}
