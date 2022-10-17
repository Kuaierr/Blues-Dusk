using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UI_CustomButton : UI_CustomBase,IPointerEnterHandler ,IPointerClickHandler
{
	public int Index { get; private set; }
	
	public Sprite normalSprite;
	public Sprite selectedSprite;
	//public Sprite clickedSprite;
	private Image _image;
	private UnityAction onClicked;
	/// <summary>
	/// Info 鼠标进入时触发
	/// </summary>
	private UnityAction<int> onSelected;

	public void OnInit(int index ,UnityAction onClickedCallback, UnityAction<int> onSelectedCallback)
	{
		Index = index;
		
		_image = GetComponent<Image>();

		onClicked += onClickedCallback;
		onSelected += onSelectedCallback;
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
			_image.sprite = selectedSprite;
	}

	public void OnReleased()
	{
		if(normalSprite != null)
			_image.sprite = normalSprite;
	}

	//TODO 如果通过鼠标操作，需要给管理者发信息，同步目前的选择，防止和键盘手柄的冲突

	public void OnPointerClick(PointerEventData eventData)
	{
		OnClicked();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		onSelected?.Invoke(Index);
	}
}
