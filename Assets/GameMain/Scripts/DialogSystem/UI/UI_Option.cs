using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameKit;
using UnityEngine.EventSystems;
using GameKit.QuickCode;
[RequireComponent(typeof(Animator))]
public class UI_Option : UIFormChildBase, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private int m_Index = 0;
    private Animator m_Animator;
    private TextMeshProUGUI m_Content;
    private UI_Response m_Response;
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

    public void Unlock()
    {
        if (m_Animator != null && m_Animator.runtimeAnimatorController != null)
        {
            m_Animator.SetTrigger(UIUtility.DO_ANIMATION_NAME);
        }
    }

    public void Lock()
    {
        if (m_Animator != null && m_Animator.runtimeAnimatorController != null)
        {
            m_Animator.SetTrigger(UIUtility.UNDO_ANIMATION_NAME);
        }
    }
}