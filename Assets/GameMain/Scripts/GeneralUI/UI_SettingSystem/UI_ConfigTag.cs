using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ConfigTag : MonoBehaviour,IPointerEnterHandler
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _content;
    [SerializeField] private Button _left;
    [SerializeField] private Button _right;

    [Space]
    private Image _image;
    [SerializeField] private Sprite _normalImage;
    [SerializeField] private Sprite _selectedImage;
    
    private CustomConfigOptionSet _config;

    public string CurrentConfigState => _config.CurrentOptionName;
    
    public UI_ConfigTag OnInit(CustomConfigOptionSet config)
    {
        _image = GetComponent<Image>();
        
        _config = config;
        _title.text = config.ConfigName;
        
        _left.onClick.AddListener(LeftClicked);
        _right.onClick.AddListener(RightClicked);
        
        UpdateCurrentConfigDisplay();

        return this;
    }

    public void LeftClicked()
    {
        _config.Minus(); 
        UpdateCurrentConfigDisplay();
    }

    public void RightClicked()
    {
        _config.Add();
        UpdateCurrentConfigDisplay();
    }

    private void UpdateCurrentConfigDisplay()
    {
        _content.text = CurrentConfigState;
    }

    public void OnSelected()
    {
        if (_selectedImage != null)
            _image.sprite = _selectedImage;
    }

    public void OnReleased()
    {
        if (_normalImage != null)
            _image.sprite = _normalImage;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
}
