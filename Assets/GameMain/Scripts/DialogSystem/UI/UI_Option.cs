using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameKit;
using UnityEngine.EventSystems;
using GameKit.QuickCode;
public class UI_Option : UIFormChildBase, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI m_Content;
    private int m_Index = 0;
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

    public void OnInit(UI_Response m_Response)
    {
        base.OnInit(m_Response.Depth);
        this.m_Response = m_Response;
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
}