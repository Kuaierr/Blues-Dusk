using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GameKit;
using UnityEngine.Events;

public class UI_DialogResponse : UIGroup 
{
    public RectTransform selector;
    public List<UI_DialogOption> ui_options = new List<UI_DialogOption>();
    private List<Option> currentOptions = new List<Option>();
    private VerticalLayoutGroup verticalLayoutGroup;
    private Sequence selectorSeq;
    public bool isActive = false;
    private int currentIndex = 0;
    protected override void Start()
    {
        verticalLayoutGroup =GetComponentInChildren<VerticalLayoutGroup>();
        selectorSeq = DOTween.Sequence();
        foreach (var ui_option in ui_options)
        {
            ui_option.OnStart();
        }
    }
    void Update()
    {
        if(!isActive)
            return;
        foreach (var ui_option in ui_options)
        {
            ui_option.OnTick();
        }
        float distance = verticalLayoutGroup.spacing + ui_options[0].rectTransform.sizeDelta.y;
        Debug.Log(distance);
        if(Input.GetKeyDown(KeyCode.DownArrow) && currentIndex < currentOptions.Count)
        {
            selectorSeq.Kill();
            selectorSeq.Append(selector.DOAnchorPosY(selector.anchoredPosition.y - distance, 0.1f));
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow) && currentIndex < currentOptions.Count)
        {
            selectorSeq.Kill();
            selectorSeq.Append(selector.DOAnchorPosY(selector.anchoredPosition.y + distance, 0.1f));
        }
    }

    public override void Show(UnityAction callback = null)
    {
        base.Show(callback);
        currentIndex = 0;
        selector.anchoredPosition = new Vector2(selector.anchoredPosition.x, ui_options[0].rectTransform.anchoredPosition.y);
        for (int i = 0; i < currentOptions.Count; i++)
        {
            if(i >= ui_options.Count)
            {
                Debug.LogWarning("Too many options.");
                continue;
            }
            ui_options[i].gameObject.SetActive(true);
            ui_options[i].content.text = currentOptions[i].text;
        }
    }

    public override void Hide(UnityAction callback = null)
    {
        base.Hide(callback);
        foreach (var ui_option in ui_options)
        {
            ui_option.gameObject.SetActive(false);
        }
    }

    public void UpdateOptions(List<Option> options) => currentOptions = options;

}
