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
    public List<UI_Option> UIOptions = new List<UI_Option>();
    public bool isActive = false;
    private List<IDialogOption> m_CurrentOptions = new List<IDialogOption>();
    private VerticalLayoutGroup verticalLayoutGroup;
    private Sequence selectorSeq;
    private int currentIndex = 0;
    private float animDistance = 0;
    private Vector2 selectorInitPos = Vector2.zero;
    private bool m_CachedIsDiceCheck = false;
    [SerializeField] private Animator m_MasterAnimator;
    [SerializeField] private Animator m_DiceAnimator;

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
        UIOptions = GetComponentsInChildren<UI_Option>(true).ToList();
        selectorSeq = DOTween.Sequence();
        for (int i = 0; i < UIOptions.Count; i++)
            UIOptions[i].OnInit(this);

        selectorInitPos = SelectorTransform.anchoredPosition = UIOptions.First().RectTransform.anchoredPosition;
        animDistance = verticalLayoutGroup.spacing + UIOptions.First().RectTransform.sizeDelta.y;
        this.gameObject.SetActive(false);
    }
    public override void OnUpdate()
    {
        if (!isActive)
            return;

        foreach (var ui_option in UIOptions)
            ui_option.OnUpdate();

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex = (currentIndex + 1) % m_CurrentOptions.Count;
            MoveSelector(currentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex = currentIndex - 1 == -1 ? m_CurrentOptions.Count - 1 : currentIndex - 1;
            MoveSelector(currentIndex);
        }
    }

    public override void OnShow(UnityAction callback = null)
    {
        m_MasterAnimator.SetTrigger(UIUtility.DO_ANIMATION_NAME);
        m_MasterAnimator.OnComplete(callback: callback);
        currentIndex = 0;
        selectorInitPos = SelectorTransform.anchoredPosition = UIOptions.First().RectTransform.anchoredPosition;
        for (int i = 0; i < m_CurrentOptions.Count; i++)
        {
            if (i >= UIOptions.Count)
            {
                Log.Fatal("Too many options.");
                continue;
            }
            UIOptions[i].OnReEnable(i);
            UIOptions[i].gameObject.SetActive(true);
            UIOptions[i].Content.text = m_CurrentOptions[i].Text;



        }

        if (m_CachedIsDiceCheck)
        {
            m_DiceAnimator.SetTrigger(UIUtility.DO_ANIMATION_NAME);
        }
    }

    public override void OnHide(UnityAction callback = null)
    {
        Log.Info("Response OnHide");
        m_MasterAnimator.SetTrigger(UIUtility.UNDO_ANIMATION_NAME);
        m_MasterAnimator.OnComplete(callback: callback);
        m_CurrentOptions.Clear();
        foreach (var ui_option in UIOptions)
        {
            ui_option.gameObject.SetActive(false);
        }

        if (m_CachedIsDiceCheck)
        {
            m_DiceAnimator.SetTrigger(UIUtility.UNDO_ANIMATION_NAME);
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

    public void UpdateOptions(IDialogOptionSet optionSet, bool isDiceCheck = false)
    {
        m_CurrentOptions = optionSet.Options;
        m_CachedIsDiceCheck = isDiceCheck;
        if(m_CachedIsDiceCheck)
        {
            // 如果是筛检，则在现实Options时锁定所有选项
            for (int i = 0; i < UIOptions.Count; i++)
            {
                UIOptions[i].Lock();
            }
        }
    }

    // 共识： UIOptions 和  m_CurrentOptions 在 Index 上是一一对应的
    /// <summary>
    /// 根据筛检结果解锁不同的选项
    /// </summary>
    public void UpdateOptionsPoint(Dice_Result diceResult)
    {
        Dictionary<string, int> result = diceResult.SerializableSum;
        for (int i = 0; i < m_CurrentOptions.Count; i++)
        {
            bool canUnlock = true;
            foreach (var condition in m_CurrentOptions[i].DiceConditions)
            {
                if (!result.ContainsKey(condition.Key))
                {
                    Log.Warning("Dice Result has not condition name '{0}'", condition.Key);
                    Log.Warning(diceResult);
                    continue;
                }
                // 条件集合里只要有一个熟悉没有满足条件就不能解锁
                if (result[condition.Key] < condition.Value)
                    canUnlock = false;
            }
            if (canUnlock)
                UIOptions[i].Unlock();
            else
                UIOptions[i].Lock();
        }
        // m_CachedIsDiceCheck = result == null ? false : true;
    }
}