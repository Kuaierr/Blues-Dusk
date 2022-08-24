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
    private UIFormBase m_UIFormBase;
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

    public UIFormBase UIFormParent
    {
        get
        {
            return m_UIFormBase;
        }
    }

    public void Close()
    {
        StopAllCoroutines();
        StartCoroutine(CloseProcess(FadeTime));
    }

    public virtual void OnInit(UIFormBase uIFormBase)
    {
        m_UIFormBase = uIFormBase;
        m_CachedCanvas = this.gameObject.GetOrAddComponent<Canvas>();
        m_CachedCanvas.overrideSorting = true;
        OriginalDepth = m_CachedCanvas.sortingOrder;

        m_CanvasGroup = this.gameObject.GetOrAddComponent<CanvasGroup>();
        this.gameObject.GetOrAddComponent<GraphicRaycaster>();
    }

    public virtual void OnShow()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void OnHide()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void OnPause()
    {

    }

    public virtual void OnResume()
    {

    }

    public virtual void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {

    }

    public virtual void OnDepthChanged(int depthInForm)
    {
        int deltaDepth = m_UIFormBase.Depth + DepthFactor * depthInForm + OriginalDepth;
        m_CachedCanvas.sortingOrder = deltaDepth;
    }

    private IEnumerator CloseProcess(float duration)
    {
        yield return m_CanvasGroup.FadeToAlpha(0f, duration);
        OnHide();
    }
}
