using System;
using System.Collections;
using System.Collections.Generic;
using GameKit.QuickCode;
using UnityEngine;

public class UI_SheetSystem : MonoBehaviour
{
    [SerializeField]
    private List<UI_CustomButton> _customButtons = new List<UI_CustomButton>();

    [SerializeField]
    private List<UI_ConfigSheet> _customSheets = new List<UI_ConfigSheet>();

    private int _currentButtonIndex = 0;
    private int _currentSheetIndex = 0;
    private ConfigData _configData;

    public void OnInit(ConfigData configData)
    {
        _configData = configData;

        if (_customButtons.Count != _customSheets.Count)
            Debug.LogError("Buttons and Sheets do not Match");
        for (int i = 0; i < _customButtons.Count; i++)
        {
            int temp = i;
            _customButtons[i].OnInit(i, () => { SelectSheet(temp); }, SetCurrentIndex);
        }

        for (int i = 0; i < _customSheets.Count; i++)
        {
            _customSheets[i].OnInit(_configData.ConfigOptions[i]);
        }

        SelectSheet(0);
        SelectButton(0);
    }

    private void SelectSheet(int index)
    {
        if(index<0 || index >= _customSheets.Count) return;
        
        UnSelectCurrentSheet();
        SwitchCurrentSheet(index);
    }

    private void SwitchCurrentSheet(int index)
    {
        if (index >= 0 && index < _customSheets.Count)
        {
            _customSheets[index].OnOpen();
            _currentSheetIndex = index;
        }
    }

    private void UnSelectCurrentSheet()
    {
        if (_currentSheetIndex >= 0 && _currentSheetIndex < _customSheets.Count)
        {
            _customButtons[_currentSheetIndex].OnReleased();
            _customSheets[_currentSheetIndex].OnClose();
        }
    }

    private void SetCurrentIndex(int index)
    {
        SelectButton(index);
    }

    private void SelectButton(int index)
    {
        if(index < 0 || index >= _customButtons.Count) return;
        
        ReleaseCurrentButton();
        SelectCurrentButton(index);
    }

    private void ReleaseCurrentButton()
    {
        if (_currentButtonIndex >= 0 && _currentButtonIndex < _customButtons.Count)
            _customButtons[_currentButtonIndex].OnReleased();
    }

    private void SelectCurrentButton(int index)
    {
        if (index >= 0 && index < _customButtons.Count)
        {
            _customButtons[index].OnSelected();
            _currentButtonIndex = index;
        }
    }

    #region KeybordInput

    public void OnUpKeyPeressed()
    {
        SelectButton(_currentButtonIndex - 1);
    }

    public void OnDownKeyPeressed()
    {
        SelectButton(_currentButtonIndex + 1);
    }

    public void OnLeftKeyPeressed() { }
    public void OnRightKeyPeressed() { }

    public UI_ConfigSheet OnConfirmKeyPeressed()
    {
        SelectSheet(_currentButtonIndex);
        ReleaseCurrentButton();

        return _customSheets[_currentSheetIndex];
    }

    public void OnBackKeyPeressed()
    {
        SelectButton(_currentButtonIndex);
    }

    #endregion
}