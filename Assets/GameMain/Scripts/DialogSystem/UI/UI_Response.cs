using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using DG.Tweening;
using UnityGameKit.Runtime;
using System.Linq;
using UnityEngine.Events;
using GameKit.Dialog;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Log = UnityGameKit.Runtime.Log;

[RequireComponent(typeof(Animator))]
public class UI_Response : UIFormChildBase
{
    public List<UI_Option> UIOptions = new List<UI_Option>();
    private List<UI_Option> m_ActiveUIOptions = new List<UI_Option>();
    private List<IDialogOption> m_CurrentOptions = new List<IDialogOption>();
    private int m_CachedDefaultOptionIndex = 0;
    private int m_CurrentIndex = 0;
    private int m_LastIndex = -1;
    private bool m_CachedIsDiceCheck = false;
    private bool m_IsActive = false;
    [SerializeField] private Animator m_MasterAnimator;
    [SerializeField] private Animator m_DiceAnimator;

    public int CurIndex
    {
        get
        {
            return m_CurrentIndex;
        }
    }

    public int DefaultOptionIndex
    {
        get
        {
            return m_CachedDefaultOptionIndex;
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

    public bool AllLocked
    {
        get
        {
            for (int i = 0; i < m_ActiveUIOptions.Count; i++)
            {
                if (!m_ActiveUIOptions[i].IsLocked)
                    return false;
            }
            return true;
        }
    }

    public void Init(int parentDepth)
    {
        base.OnInit(parentDepth);
        if (m_MasterAnimator != null)
            m_MasterAnimator = GetComponent<Animator>();
        UIOptions = GetComponentsInChildren<UI_Option>(true).ToList();
        for (int i = 0; i < UIOptions.Count; i++)
            UIOptions[i].Init(this);
        m_MasterAnimator.SetTrigger(UIUtility.FORCE_OFF_ANIMATION_NAME);
        m_ActiveUIOptions = new List<UI_Option>();
    }

    public override void OnUpdate()
    {
        if (!m_IsActive)
            return;

        bool allOptionLocked = true;
        for (int i = 0; i < m_ActiveUIOptions.Count; i++)
        {
            UIOptions[i].Update();
            allOptionLocked &= UIOptions[i].IsLocked;
        }

        if (allOptionLocked)
            return;

        int deadLoopPreventer = 0;
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            deadLoopPreventer = 0;
            do
            {
                //m_LastIndex = m_CurrentIndex;
                m_CurrentIndex = (m_CurrentIndex + 1) % m_ActiveUIOptions.Count;
                deadLoopPreventer++;
                Log.Info(m_CurrentIndex + " >> " + m_ActiveUIOptions[m_CurrentIndex].IsLocked);
                if (deadLoopPreventer > 20)
                {
                    Log.Warning("Has DeadLoop Down.");
                    break;
                }
            } while (m_ActiveUIOptions[m_CurrentIndex].IsLocked);
            EmphasizeSelectedOption(m_CurrentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            deadLoopPreventer = 0;
            do
            {
                //m_LastIndex = m_CurrentIndex;
                m_CurrentIndex = m_CurrentIndex - 1 == -1 ? m_ActiveUIOptions.Count - 1 : m_CurrentIndex - 1;
                deadLoopPreventer++;
                Log.Info(m_CurrentIndex + " >> " + m_ActiveUIOptions[m_CurrentIndex].IsLocked);
                if (deadLoopPreventer > 20)
                {
                    Log.Warning("Has DeadLoop Up.");
                    break;
                }
            } while (m_ActiveUIOptions[m_CurrentIndex].IsLocked);
            EmphasizeSelectedOption(m_CurrentIndex);
        }
    }

    public void Show(UnityAction callback = null)
    {
        Log.Info("Response OnShow");
        m_IsActive = true;
        m_MasterAnimator.SetTrigger(UIUtility.SHOW_ANIMATION_NAME);
        m_MasterAnimator.OnComplete(callback: () =>
        {
            SetDiceActive(true);
        });
        ResetCurIndex();
        for (int i = 0; i < m_ActiveUIOptions.Count; i++)
        {
            if (i >= m_ActiveUIOptions.Count)
                continue;
            m_ActiveUIOptions[i].Register(i);
            m_ActiveUIOptions[i].Show();
            m_ActiveUIOptions[i].Content.text = m_CurrentOptions[i].Text;
        }
        EmphasizeFirst();
        // EmphasizeSelectedOption(0);
    }

    public void Hide(UnityAction callback = null)
    {
        Log.Info("Response OnHide");
        m_MasterAnimator.SetTrigger(UIUtility.HIDE_ANIMATION_NAME);
        m_MasterAnimator.OnComplete(callback: callback);
        m_CurrentOptions.Clear();
        ResetCurIndex();
        m_IsActive = false;
        m_ActiveUIOptions.Clear();
        for (int i = 0; i < UIOptions.Count; i++)
        {
            UIOptions[i].Hide();
        }
        SetDiceActive(false);
    }

    public void SetDiceActive(bool status)
    {
        if (m_CachedIsDiceCheck)
        {
            //Info 第二次进入鉴定时，保留了Hide的trigger，引发了不具合。此处是应急措施
            m_DiceAnimator.ResetTrigger(UIUtility.SHOW_ANIMATION_NAME);
            m_DiceAnimator.ResetTrigger(UIUtility.HIDE_ANIMATION_NAME);
            m_MasterAnimator.ResetTrigger(UIUtility.ENABLE_ANIMATION_NAME);
            m_MasterAnimator.ResetTrigger(UIUtility.DISABLE_ANIMATION_NAME);
            
            m_DiceAnimator.SetTrigger(status ? UIUtility.SHOW_ANIMATION_NAME : UIUtility.HIDE_ANIMATION_NAME);
            m_MasterAnimator.SetTrigger(status ? UIUtility.ENABLE_ANIMATION_NAME : UIUtility.DISABLE_ANIMATION_NAME);
        }
    }

    public void OnOptionEnter(UI_Option option)
    {
        // Log.Warning("OnOptionEnter");
        // m_LastIndex = m_CurrentIndex;
        // m_CurrentIndex = option.Index;
        // EmphasizeSelectedOption(m_CurrentIndex);
    }

    public void OnOptionExit(UI_Option option)
    {
        // Log.Warning("OnOptionExit");
        // m_LastIndex = m_CurrentIndex;
        // m_CurrentIndex = -1;
        // EmphasizeSelectedOption(m_CurrentIndex);
    }

    public void OnOptionDown(UI_Option option)
    {

    }

    private void EmphasizeSelectedOption(int index)
    {
        if (m_LastIndex >= 0)
            UIOptions[m_LastIndex].SetEmphasize(false);
        if (index >= 0)
        {
            UIOptions[index].SetEmphasize(true);
            m_LastIndex = m_CurrentIndex;
        }
    }

    // 共识： UIOptions 和  m_CurrentOptions 在 Index 上是一一对应的
    public void UpdateOptions(IDialogOptionSet optionSet, bool isDiceCheck = false)
    {
        m_ActiveUIOptions.Clear();
        m_CurrentOptions = optionSet.Options;
        m_CachedIsDiceCheck = isDiceCheck;
        // Log.Warning(optionSet.Options.Count + " >> " + UIOptions.Count);
        // 如果是筛检，则在显示Options时锁定所有选项
        for (int i = 0; i < m_CurrentOptions.Count; i++)
        {
            if (m_CurrentOptions[i].CanShow)
            {
                m_ActiveUIOptions.Add(UIOptions[i]);
                if (m_CachedIsDiceCheck)
                {
                    UIOptions[i].Lock();
                    UIOptions[i].ShowDiceIndicator(m_CurrentOptions[i]);
                }
                else if (optionSet.Options[i].DiceConditions.ContainsKey("result"))
                {
                    if(optionSet.Options[i].DiceConditions["result"] == 1)
                        UIOptions[i].Unlock();
                    else
                        UIOptions[i].Lock();
                }
                /*else if (UIOptions[i].IsLocked)
                    UIOptions[i].Unlock();*/
            }
            else
                m_CachedDefaultOptionIndex = i;
        }
    }

    //TODO 需要将仓检和骰检合并到普通选项的情况中
    /*public void UpdateAsPlayerInventoryCheckOptions(IDialogOptionSet optionSet, Dictionary<string,bool> conditions)
    {
        m_ActiveUIOptions.Clear();
        m_CurrentOptions = optionSet.Options;
        for (int i = 0; i < m_CurrentOptions.Count; i++)
        {
            m_ActiveUIOptions.Add(UIOptions[i]);
            //if (conditions[optionSet.Options[i].Text] == true)
            if(optionSet.Options[i].DiceConditions["result"] == 1)
            {
                UIOptions[i].Unlock();
            }
            else
            {
                UIOptions[i].Lock();
            }
        }
    }*/

    //Info 仅仅是根据CanShow进行的配置，没有单独列出来的必要，也不知道在哪调用它
    /*public void UpdateAsDiceInventoryCheckOption(IDialogOptionSet optionSet/*, List<bool> conditions#1#)
    {
        m_ActiveUIOptions.Clear();
        m_CurrentOptions = optionSet.Options;
        for (int i = 0; i < m_CurrentOptions.Count; i++)
        {
            /*m_ActiveUIOptions.Add(UIOptions[i]);
            if (conditions[i] == true)
            {
                UIOptions[i].SetActive(true);
                UIOptions[i].Unlock();
            }
            else
            {
                UIOptions[i].SetActive(false);
                UIOptions[i].Lock();
            }#1#
            if (optionSet.Options[i].CanShow)
                UIOptions[i].SetActive(true);
            else
                UIOptions[i].SetActive(false);
        }
    }*/

    // 共识： UIOptions 和  m_CurrentOptions 在 Index 上是一一对应的
    /// <summary>
    /// 根据筛检结果解锁不同的选项
    /// </summary>
    public void UpdateOptionsPoint(Dice_Result diceResult)
    {
        Dictionary<string, int> serialResults = diceResult.SerializableSum;
        for (int i = 0; i < m_CurrentOptions.Count; i++)
        {
            if (!m_CurrentOptions[i].CanShow)
                continue;
            // 默认可以解锁
            bool canUnlock = true;
            // 根据筛子结果给Option充电
            UIOptions[i].ChargeDiceIndicator(diceResult);

            // 遍历每一个解锁所需要的的条件
            foreach (var optionCondition in m_CurrentOptions[i].DiceConditions)
            {
                if (!serialResults.ContainsKey(optionCondition.Key))
                {
                    Log.Warning("Dice Result has not condition name '{0}'", optionCondition.Key);
                    Log.Warning(diceResult);
                    continue;
                }
                // 条件集合里只要有一个熟悉没有满足条件就不能解锁
                if (serialResults[optionCondition.Key] < optionCondition.Value)
                    canUnlock = false;
            }
            if (canUnlock)
                UIOptions[i].Unlock();
            else
                UIOptions[i].Lock();
        }
        EmphasizeFirst();
    }

    private void ResetCurIndex()
    {
        m_CurrentIndex = 0;
        m_LastIndex = -1;
    }

    private UI_Option FindFirstValidOption()
    {
        for (int i = 0; i < m_ActiveUIOptions.Count; i++)
        {
            if (!UIOptions[i].IsLocked && m_CurrentOptions[i].CanShow)
            {
                return UIOptions[i];
            }
        }
        return null;
    }

    private void EmphasizeFirst()
    {
        /*UI_Option firstOption = FindFirstValidOption();
        if (firstOption != null)
            firstOption.SetEmphasize(true);*/
        for (int i = 0; i < m_ActiveUIOptions.Count; i++)
        {
            if (!m_ActiveUIOptions[i].IsLocked)
            {
                EmphasizeSelectedOption(i);
                break;
            }
        }
    }
}