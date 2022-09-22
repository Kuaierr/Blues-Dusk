using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UI_ConfigSheet : UI_CustomBase
{
    private CanvasGroup _canvas;
    private List<UI_ConfigTag> _tags = new List<UI_ConfigTag>();
    private int _currentIndex = 0;

    [SerializeField]
    private UI_ConfigTag _configTag;

    public void OnInit(List<CustomConfigOptionSet> optionList)
    {
        _canvas = GetComponent<CanvasGroup>();

        foreach (Transform child in transform)
            Destroy(child.gameObject);

        foreach (CustomConfigOptionSet option in optionList)
            _tags.Add(InitConfigTag(option));
    }

    private UI_ConfigTag InitConfigTag(CustomConfigOptionSet optionSet)
    {
        return Instantiate(_configTag, this.transform).OnInit(optionSet);
    }

    public void OnOpen()
    {
        _canvas.alpha = 1;
        _canvas.blocksRaycasts = true;
        _canvas.interactable = true;
    }

    public void OnClose()
    {
        _canvas.alpha = 0;
        _canvas.blocksRaycasts = false;
        _canvas.interactable = false;
    }

    public void Select(int index)
    {
        if (index < 0 || index >= _tags.Count) return;

        if (_currentIndex >= 0 && _currentIndex < _tags.Count)
            _tags[_currentIndex].OnReleased();
        
        _tags[index].OnSelected();
        _currentIndex = index;
    }

    #region KeybordControl

    public void OnUpKeyPeressed()
    {
        Select(_currentIndex - 1);
    }

    public void OnDownKeyPeressed()
    {
        Select(_currentIndex + 1);
    }

    public void OnLeftKeyPeressed()
    {
        _tags[_currentIndex].LeftClicked();
    }

    public void OnRightKeyPeressed()
    {
        _tags[_currentIndex].RightClicked();
    }

    public UI_ConfigSheet OnConfirmKeyPeressed()
    {
        return null;
    }

    public void OnBackKeyPeressed()
    {
        Debug.Log("Back");
    }

    #endregion
}