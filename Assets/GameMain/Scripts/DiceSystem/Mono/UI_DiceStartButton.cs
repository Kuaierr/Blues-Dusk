using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_DiceStartButton : UIFormChildBase, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private UnityAction onClick;

    [SerializeField]
    private float _holdTimer = 1.5f;

    [SerializeField]
    private Image _buttonImage;

    [SerializeField]
    private Image _holdSlider;

    [SerializeField]
    private TMP_Text _buttonText;

    [Space(15)]
    [SerializeField]
    private Sprite _disabledSprite;

    [SerializeField]
    private Sprite _enabledSprite;

    private new bool enabled = false;

    public void OnInit(UnityAction onClickCallback)
    {
        Disable();
        onClick += onClickCallback;
    }

    protected override void Start()
    {
        base.Start();
        OnDepthChanged(100);
    }

    public void Clear()
    {
        onClick = null;
    }

    public void AddCallBack(UnityAction onClickCallback)
    {
        OnDisable();
        onClick += onClickCallback;
    }

    public void Disable()
    {
        //TODO 字体颜色、外发光相关的调整
        _buttonText.text = "至少放置一个";
        _buttonImage.sprite = _disabledSprite;

        enabled = false;
    }

    public void Enable()
    {
        _buttonText.text = "鉴定";
        _buttonImage.sprite = _enabledSprite;

        enabled = true;
    }

    public void OnButtonPressed()
    {
        if (enabled)
            StartCoroutine("HoldStartButton");
    }

    public void OnButtonReleased()
    {
        StopCoroutine("HoldStartButton");
        _holdSlider.fillAmount = 0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnButtonPressed();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //if (enabled)
        {
            OnButtonReleased();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //if (enabled)
        {
            OnButtonReleased();
        }
    }

    private IEnumerator HoldStartButton()
    {
        float timer = 0;
        while (timer < _holdTimer)
        {
            timer += Time.deltaTime;
            _holdSlider.fillAmount = timer / _holdTimer;
            yield return 0;
        }
        onClick.Invoke();
    }
}