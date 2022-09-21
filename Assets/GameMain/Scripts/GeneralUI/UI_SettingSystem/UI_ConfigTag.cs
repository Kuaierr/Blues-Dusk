using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_ConfigTag : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _content;
    [SerializeField] private Button _left;
    [SerializeField] private Button _right;

    private CustomConfigOptionSet _config;

    public string CurrentConfigState => _config.CurrentOptionName;
    
    public void OnInit(CustomConfigOptionSet config)
    {
        _config = config;
        _title.text = config.ConfigName;
        
        _left.onClick.AddListener(() =>
        {
            config.Minus(); 
            UpdateCurrentConfigDisplay();
            Debug.Log("Left");
        });
        _right.onClick.AddListener(() =>
        {
            config.Add();
            UpdateCurrentConfigDisplay();
            Debug.Log("Right");
        });
        
        UpdateCurrentConfigDisplay();
    }

    private void UpdateCurrentConfigDisplay()
    {
        _content.text = CurrentConfigState;
    }
}
