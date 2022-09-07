using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityGameKit.Runtime;
using UnityEngine.EventSystems;
using GameKit.Dialog;
[RequireComponent(typeof(Animator))]
public class UI_Option : UIFormChildBase, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public List<UI_OptionIndicator> OptionIndicators;
    private Animator m_Animator;
    private TextMeshProUGUI m_Content;
    private UI_Response m_Response;
    private int m_Index = 0;
    private int m_CurrentIndicatorIndex = 0;
    public TextMeshProUGUI Content
    {
        get
        {
            return m_Content;
        }
    }

    public int Index
    {
        get
        {
            return m_Index;
        }
    }

    public void OnInit(UI_Response response)
    {
        base.OnInit(response.Depth);
        this.m_Response = response;
        m_Animator = GetComponent<Animator>();
        m_Content = GetComponentInChildren<TextMeshProUGUI>();
        ResetOptionIndicator();
        this.gameObject.SetActive(false);
    }
    public void OnReEnable(int Index)
    {
        this.m_Index = Index;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_Response.OnOptionDown(this);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_Response.OnOptionEnter(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        m_Response.OnOptionExit(this);
    }

    public void ShowDiceIndicator(IDialogOption option)
    {
        ResetOptionIndicator();
        foreach (var condition in option.DiceConditions)
        {
            if (condition.Value > 0)
            {
                for (int i = 0; i < condition.Value; i++)
                {
                    if (m_CurrentIndicatorIndex < OptionIndicators.Count)
                        OptionIndicators[m_CurrentIndicatorIndex++].OnShow();
                    else
                        Log.Fatal("The num of option indicator is over maximum");
                }
            }
        }
    }

    public void Unlock()
    {
        if (m_Animator != null && m_Animator.runtimeAnimatorController != null)
        {
            m_Animator.SetTrigger(UIUtility.ENABLE_ANIMATION_NAME);
        }
        CanvasGroup.alpha = 1f;
    }

    public void Lock()
    {
        if (m_Animator != null && m_Animator.runtimeAnimatorController != null)
        {
            m_Animator.SetTrigger(UIUtility.DISABLE_ANIMATION_NAME);
        }
        CanvasGroup.alpha = 0.5f;
    }

    private void ResetOptionIndicator()
    {
        m_CurrentIndicatorIndex = 0;
        for (int i = 0; i < OptionIndicators.Count; i++)
        {
            OptionIndicators[i].OnHide();
        }
    }
}