using System.Collections;
using System.Collections.Generic;
using UnityGameKit.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIFormChildBase : UIBehaviour
{
    protected const int DepthFactor = 10;
    protected const float FadeTime = 0.3f;
    private int ParentDepth = 0;
    private RectTransform m_RectTransform;
    private Canvas m_CachedCanvas = null;
    private CanvasGroup m_CanvasGroup = null;
    public int OriginalDepth
    {
        get;
        private set;
    }

    public int Depth
    {
        get
        {
            return m_CachedCanvas.sortingOrder;
        }
    }
    public RectTransform RectTransform
    {
        get
        {
            if (m_RectTransform == null)
                m_RectTransform = this.GetComponent<RectTransform>();
            return m_RectTransform;
        }
    }


    protected override void Awake()
    {
        m_CachedCanvas = this.gameObject.GetOrAddComponent<Canvas>();
        m_CachedCanvas.overrideSorting = true;
        OriginalDepth = m_CachedCanvas.sortingOrder;
        m_CanvasGroup = this.gameObject.GetOrAddComponent<CanvasGroup>();
        this.gameObject.GetOrAddComponent<GraphicRaycaster>();
    }

    public void Close()
    {
        m_CanvasGroup.alpha = 0;
    }

    public virtual void OnInit(int parentDepth)
    {
        ParentDepth = parentDepth;
    }

    public virtual void OnShow(UnityAction callback = null)
    {
        this.gameObject.SetActive(true);
    }

    public virtual void OnHide(UnityAction callback = null)
    {
        this.gameObject.SetActive(false);
    }

    public virtual void OnPause(UnityAction callback = null)
    {

    }

    public virtual void OnResume(UnityAction callback = null)
    {

    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnDepthChanged(int depthInForm)
    {
        int deltaDepth = ParentDepth + DepthFactor * depthInForm + OriginalDepth;
        m_CachedCanvas.sortingOrder = deltaDepth;
    }
}
