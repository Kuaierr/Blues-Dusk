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
    private List<UI_CustomSheet> _customSheets = new List<UI_CustomSheet>();

    private int _currentIndex = 0;
    private ConfigData _configData;

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        _configData = new ConfigData();
        
        if (_customButtons.Count != _customSheets.Count)
            Debug.LogError("Buttons and Sheets do not Match");
        for (int i = 0; i < _customButtons.Count; i++)
        {
            int temp = i;
            _customButtons[i].OnInit(() => { SelectSheet(temp); });
        }

        for (int i = 0; i < _customSheets.Count; i++)
        {
            _customSheets[i].OnInit(_configData.ConfigOptions[i]);
        }

        SelectSheet(0);
    }

    /*private void Update()
    {
        if(InputManager.instance.GetKeyDown(KeyCode.W))
            Debug.Log("上移");
        if(InputManager.instance.GetKeyDown(KeyCode.S))
            Debug.Log("下移");
        if(InputManager.instance.GetKeyDown(KeyCode.A))
            Debug.Log("切换设置选项");
        if(InputManager.instance.GetKeyDown(KeyCode.D))
            Debug.Log("切换设置选项");
        if(InputManager.instance.GetKeyDown(KeyCode.Space))
            Debug.Log("确认按钮");
        if(InputManager.instance.GetKeyDown(KeyCode.Escape))
            Debug.Log("返回");
    }*/

    private void SelectSheet(int index)
    {
        UnSelectCurrentSheet();
        SelectCurrentSheet(index);
    }

    private void SelectCurrentSheet(int index)
    {
        if (index >= 0 && index < _customButtons.Count)
        {
            _customSheets[index].OnOpen();
            _currentIndex = index;
        }
    }

    private void UnSelectCurrentSheet()
    {
        if (_currentIndex >= 0 && _currentIndex < _customButtons.Count)
        {
            _customButtons[_currentIndex].OnReleased();
            _customSheets[_currentIndex].OnClose();
        }
    }

    /*#region KeybordInput

    private void UpKeyPeressed()
    {
        
    }

    #endregion*/
}