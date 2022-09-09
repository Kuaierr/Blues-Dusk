using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using DG.Tweening;
using UnityGameKit.Runtime;
using System.Linq;
using UnityEngine.Events;
using GameKit.Dialog;

[RequireComponent(typeof(Animator))]
public class UI_Response : UIFormChildBase
{
    public List<UI_Option> UIOptions = new List<UI_Option>();
    public bool isActive = false;
    private List<IDialogOption> m_CurrentOptions = new List<IDialogOption>();
    private VerticalLayoutGroup verticalLayoutGroup;
    private int m_CurrentIndex = 0;
    private int m_LastIndex = -1;
    private bool m_CachedIsDiceCheck = false;
    [SerializeField] private Animator m_MasterAnimator;
    [SerializeField] private Animator m_DiceAnimator;

    public int CurIndex
    {
        get
        {
            return m_CurrentIndex;
        }
    }

    public Animator MasterAnimator
    {
        get
        {
            return m_MasterAnimator;
        }
    }

    public Animator DiceAnimator
    {
        get
        {
            return m_DiceAnimator;
        }
    }

    public override void OnInit(int parentDepth)
    {
        base.OnInit(parentDepth);
        if (m_MasterAnimator != null)
            m_MasterAnimator = GetComponent<Animator>();
        verticalLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();
        UIOptions = GetComponentsInChildren<UI_Option>(true).ToList();
        for (int i = 0; i < UIOptions.Count; i++)
            UIOptions[i].OnInit(this);
        m_MasterAnimator.SetTrigger(UIUtility.FORCE_OFF_ANIMATION_NAME);
    }
    
    public override void OnUpdate()
    {
        if (!isActive)
            return;

        foreach (var ui_option in UIOptions)
            ui_option.OnUpdate();

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_LastIndex = m_CurrentIndex;
            m_CurrentIndex = (m_CurrentIndex + 1) % m_CurrentOptions.Count;
            MoveSelector(m_CurrentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_LastIndex = m_CurrentIndex;
            m_CurrentIndex = m_CurrentIndex - 1 == -1 ? m_CurrentOptions.Count - 1 : m_CurrentIndex - 1;
            MoveSelector(m_CurrentIndex);
        }
    }

    public override void OnShow(UnityAction callback = null)
    {
        Log.Info("Response OnShow");
        m_MasterAnimator.SetTrigger(UIUtility.SHOW_ANIMATION_NAME);
        m_MasterAnimator.OnComplete(callback: () =>
        {
            SetDiceActive(true);
        });
        isActive = true;
        ResetCurIndex();
        for (int i = 0; i < m_CurrentOptions.Count; i++)
        {
            if (i >= UIOptions.Count)
            {
                Log.Fatal("Too many options.");
                continue;
            }
            UIOptions[i].OnReEnable(i);
            UIOptions[i].OnShow();
            UIOptions[i].Content.text = m_CurrentOptions[i].Text;
        }


        Log.Info(m_CachedIsDiceCheck);

        // MoveSelector(0);
    }

    public override void OnHide(UnityAction callback = null)
    {
        Log.Info("Response OnHide");
        m_MasterAnimator.SetTrigger(UIUtility.HIDE_ANIMATION_NAME);
        m_MasterAnimator.OnComplete(callback: callback);
        m_CurrentOptions.Clear();
        ResetCurIndex();
        isActive = false;
        for (int i = 0; i < UIOptions.Count; i++)
        {
            UIOptions[i].OnHide();
        }

        SetDiceActive(false);
    }

    public void SetDiceActive(bool status)
    {
        if (m_CachedIsDiceCheck)
        {
            m_DiceAnimator.SetTrigger(status ? UIUtility.SHOW_ANIMATION_NAME : UIUtility.HIDE_ANIMATION_NAME);
            m_MasterAnimator.SetTrigger(status ? UIUtility.ENABLE_ANIMATION_NAME : UIUtility.DISABLE_ANIMATION_NAME);
        }
    }

    public void OnOptionEnter(UI_Option option)
    {
        // Log.Warning("OnOptionEnter");
        // m_LastIndex = m_CurrentIndex;
        // m_CurrentIndex = option.Index;
        // MoveSelector(m_CurrentIndex);
    }

    public void OnOptionExit(UI_Option option)
    {
        // Log.Warning("OnOptionExit");
        // m_LastIndex = m_CurrentIndex;
        // m_CurrentIndex = -1;
        // MoveSelector(m_CurrentIndex);
    }

    public void OnOptionDown(UI_Option option)
    {

    }

    private void MoveSelector(int index)
    {
        if (m_LastIndex >= 0)
            UIOptions[m_LastIndex].SetEmphasize(false);
        if (index >= 0)
            UIOptions[index].SetEmphasize(true);
    }

    // 共识： UIOptions 和  m_CurrentOptions 在 Index 上是一一对应的
    public void UpdateOptions(IDialogOptionSet optionSet, bool isDiceCheck = false)
    {
        m_CurrentOptions = optionSet.Options;
        m_CachedIsDiceCheck = isDiceCheck;
        if (m_CachedIsDiceCheck)
        {
            // Log.Warning(optionSet.Options.Count + " >> " + UIOptions.Count);
            // 如果是筛检，则在显示Options时锁定所有选项
            for (int i = 0; i < optionSet.Options.Count; i++)
            {
                UIOptions[i].Lock();
                UIOptions[i].ShowDiceIndicator(optionSet.Options[i]);
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

    private void UpdateOptionCharger()
    {

    }

    private void ResetCurIndex()
    {
        m_CurrentIndex = 0;
        m_LastIndex = -1;
    }
}