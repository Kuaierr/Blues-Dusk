using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UI_CustomButton : UI_CustomBase, IPointerEnterHandler, IPointerClickHandler
{
    public int Index { get; private set; }

    public Sprite normalSprite;
    public Sprite selectedSprite;
    public Color normalColor = Color.white;
    public Color selectedColor = Color.white;

    //public Sprite clickedSprite;
    private Image _image;
    private UnityAction onClicked;

    /// <summary>
    /// Info 鼠标进入时触发
    /// </summary>
    private UnityAction<int> onSelected;

    public UI_CustomButton OnInit(int index, UnityAction onClickedCallback, UnityAction<int> onSelectedCallback)
    {
        Index = index;

        _image = GetComponent<Image>();

        if (onClickedCallback != null)
            onClicked += onClickedCallback;
        if (onSelectedCallback != null)
            onSelected += onSelectedCallback;
        OnReleased();

        return this;
    }

    public void OnClicked()
    {
        onClicked?.Invoke();
        //TODO 这里可以播放按钮动画
    }

    public void OnSelected()
    {
        if (selectedSprite != null)
            _image.sprite = selectedSprite;

        _image.color = selectedColor;
    }

    public void OnReleased()
    {
        if (normalSprite != null)
            _image.sprite = normalSprite;

        _image.color = normalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onSelected?.Invoke(Index);
    }

    public void Clear()
    {
        onClicked = null;
        onSelected = null;
    }
}