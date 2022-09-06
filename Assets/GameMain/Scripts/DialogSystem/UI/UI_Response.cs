using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityGameKit.Runtime;
using System.Linq;
using UnityEngine.Events;
using GameKit.Dialog;

[RequireComponent(typeof(Animator))]
public class UI_Response : UIFormChildBase
{
    public RectTransform SelectorTransform;
    public List<UI_Option> ui_options = new List<UI_Option>();
    public bool isActive = false;
    private List<IDialogOption> currentOptions = new List<IDialogOption>();
    private VerticalLayoutGroup verticalLayoutGroup;
    private Sequence selectorSeq;
    private int currentIndex = 0;
    private float animDistance = 0;
    private Vector2 selectorInitPos = Vector2.zero;
    [SerializeField] private Animator m_MasterAnimator;

    public int CurIndex
    {
        get
        {
            return currentIndex;
        }
    }

    public Animator MasterAnimator
    {
        get
        {
            return m_MasterAnimator;
        }
    }

    public override void OnInit(int parentDepth)
    {
        base.OnInit(parentDepth);
        if (m_MasterAnimator != null)
            m_MasterAnimator = GetComponent<Animator>();
        verticalLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();
        ui_options = GetComponentsInChildren<UI_Option>(true).ToList();
        selectorSeq = DOTween.Sequence();
        for (int i = 0; i < ui_options.Count; i++)
            ui_options[i].OnInit(this);

        selectorInitPos = SelectorTransform.anchoredPosition = ui_options.First().RectTransform.anchoredPosition;
        animDistance = verticalLayoutGroup.spacing + ui_options.First().RectTransform.sizeDelta.y;
        this.gameObject.SetActive(false);
    }
    public override void OnUpdate()
    {
        if (!isActive)
            return;

        foreach (var ui_option in ui_options)
            ui_option.OnUpdate();

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex = (currentIndex + 1) % currentOptions.Count;
            MoveSelector(currentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex = currentIndex - 1 == -1 ? currentOptions.Count - 1 : currentIndex - 1;
            MoveSelector(currentIndex);
        }
    }

    public override void OnShow(UnityAction callback = null)
    {
        m_MasterAnimator.SetTrigger(UIUtility.DO_ANIMATION_NAME);
        m_MasterAnimator.OnComplete(callback: callback);
        currentIndex = 0;
        selectorInitPos = SelectorTransform.anchoredPosition = ui_options.First().RectTransform.anchoredPosition;
        for (int i = 0; i < currentOptions.Count; i++)
        {
            if (i >= ui_options.Count)
            {
                Debug.LogWarning("Too many options.");
                continue;
            }
            ui_options[i].OnReEnable(i);
            ui_options[i].gameObject.SetActive(true);
            ui_options[i].Content.text = currentOptions[i].Text;
        }
    }

    public override void OnHide(UnityAction callback = null)
    {
        Log.Info("Response OnHide");
        m_MasterAnimator.SetTrigger("FadeOut");
        m_MasterAnimator.OnComplete(callback: callback);
        currentOptions.Clear();
        foreach (var ui_option in ui_options)
        {
            ui_option.gameObject.SetActive(false);
        }
    }

    public void OnOptionEnter(UI_Option option)
    {
        currentIndex = option.Index;
        MoveSelector(currentIndex);
    }

    public void OnOptionExit(UI_Option option)
    {

    }

    public void OnOptionDown(UI_Option option)
    {

    }

    private void MoveSelector(int index)
    {
        selectorSeq.Kill();
        selectorSeq.Append(SelectorTransform.DOAnchorPosY(selectorInitPos.y - animDistance * index, 0.1f));
    }

    public void UpdateOptions(IDialogOptionSet optionSet)
    {
        currentOptions = optionSet.Options;
    }
}