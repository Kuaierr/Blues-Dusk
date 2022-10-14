using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityGameKit.Runtime;
using UnityEngine.EventSystems;
using GameKit.Dialog;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class UI_Option : UIFormChildBase, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public List<UI_OptionIndicator> OptionIndicators;
    private TextMeshProUGUI m_Content;
    private UI_Response m_Response;
    private int m_Index = 0;
    private int m_CurrentIndicatorIndex = 0;
    private int m_CachedActiveIndicatorNum = 0;
    private bool m_IsLocked;

    public TextMeshProUGUI Content
    {
        get { return m_Content; }
    }

    public int Index
    {
        get { return m_Index; }
    }

    public bool IsLocked
    {
        get { return m_IsLocked; }
    }

    public void Init(UI_Response response)
    {
        base.OnInit(response.Depth);
        this.m_Response = response;
        OnDepthChanged(1);
        m_Content = GetComponentInChildren<TextMeshProUGUI>();
        for (int i = 0; i < OptionIndicators.Count; i++)
        {
            OptionIndicators[i].Init(Depth);
        }
    }

    public void Hide(UnityAction callback = null)
    {
        base.OnHide(callback);
        ResetOptionIndicator();

        Unlock();
        //SetActive(true);
    }

    public void Show(UnityAction callback = null)
    {
        base.OnShow(callback);
    }

    public void Register(int Index)
    {
        this.m_Index = Index;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!m_IsLocked)
            m_Response.OnOptionDown(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!m_IsLocked)
            m_Response.OnOptionEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //m_Response.OnOptionExit(this);
    }

    public void ShowDiceIndicator(IDialogOption option)
    {
        // ResetOptionIndicator();
        // foreach (var item in option.DiceConditions)
        // {
        //     Log.Warning("{0}-{1}", item.Key, item.Value);
        // }
        m_CachedActiveIndicatorNum = 0;
        foreach (var optionCondition in option.DiceConditions)
        {
            // 如果有这个条件属性的要求
            if (optionCondition.Value > 0)
            {
                // 每个值显示一个对象
                for (int i = 0; i < optionCondition.Value; i++)
                {
                    m_CachedActiveIndicatorNum++;
                    // 添加的属性要求数目不大于预设值的OptionIndicators的数目
                    if (m_CurrentIndicatorIndex < OptionIndicators.Count)
                    {
                        // Log.Warning(m_CurrentIndicatorIndex);
                        Color indicatorColor = DialogUtility.GetDiceAttributColor(optionCondition.Key);
                        OptionIndicators[m_CurrentIndicatorIndex].SetDiceRequire(optionCondition.Key);
                        OptionIndicators[m_CurrentIndicatorIndex].SetColor(indicatorColor);
                        OptionIndicators[m_CurrentIndicatorIndex].Show();
                        m_CurrentIndicatorIndex++;
                    }
                    else
                        Log.Fatal("The num of option indicator is over maximum");
                }
            }
        }
    }

    public void ChargeDiceIndicator(Dice_Result diceResult)
    {
        // 读取结果
        foreach (KeyValuePair<string, int> serialResult in diceResult.SerializableSum)
        {
            // 如果有筛出当前面
            if (serialResult.Value > 0)
            {
                // 遍历这个面出现的数量
                for (int i = 0; i < serialResult.Value; i++)
                {
                    // 对于每个打开的节点，尝试进行激活

                    for (int j = 0; j < m_CachedActiveIndicatorNum; j++)
                    {
                        // Log.Info("{0} try charge Indicator '{1}' with {2}==?{3}.", this.gameObject.name, j, serialResult.Key, OptionIndicators[j].CachedType);
                        OptionIndicators[j].Charge(serialResult.Key);
                    }
                }
            }
        }
    }

    public void Unlock()
    {
        Log.Success("Unlock {0}", this.gameObject.name);
        m_IsLocked = false;
        if (!SetEnable(true))
            CanvasGroup.alpha = 1f;
    }

    public void Lock()
    {
        Log.Success("Lock {0}", this.gameObject.name);
        m_IsLocked = true;
        if (!SetEnable(false))
            CanvasGroup.alpha = 0.5f;
    }

    private void ResetOptionIndicator()
    {
        Log.Warning("ResetOptionIndicator");
        m_CurrentIndicatorIndex = 0;
        m_CachedActiveIndicatorNum = 0;
        for (int i = 0; i < OptionIndicators.Count; i++)
        {
            OptionIndicators[i].Hide();
        }
    }

    public void Update()
    {
        base.OnUpdate();
        for (int i = 0; i < m_CachedActiveIndicatorNum; i++)
        {
            OptionIndicators[i].OnUpdate();
        }
    }
}