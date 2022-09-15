using System.Collections;
using System.Collections.Generic;
using UnityGameKit.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup), typeof(Animator))]
public abstract class UIFormChildBase : UIBehaviour
{
    protected const int DepthFactor = 10;
    protected const float FadeTime = 0.3f;
    private int ParentDepth = 0;
    private RectTransform m_RectTransform;
    private Canvas m_CachedCanvas = null;
    private CanvasGroup m_CanvasGroup = null;
    private Animator m_Animator;
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

    public CanvasGroup CanvasGroup
    {
        get
        {
            return m_CanvasGroup;
        }
    }

    public Animator Animator
    {
        get
        {
            return m_Animator;
        }
    }

    public bool SetEmphasize(bool status, bool isForce = false)
    {
        if (m_Animator != null && m_Animator.runtimeAnimatorController != null)
        {
            if (isForce)
                m_Animator.SetTriggerOneTime(status ? UIUtility.FORCE_DOEMPHASIZE_ANIMATION_NAME : UIUtility.FORCE_UNDOEMPHASIZE_ANIMATION_NAME);
            else
                m_Animator.SetTriggerOneTime(status ? UIUtility.DOEMPHASIZE_ANIMATION_NAME : UIUtility.UNDOEMPHASIZE_ANIMATION_NAME);
            return true;
        }
        return false;
    }

    public bool SetEnable(bool status, bool isForce = false)
    {
        if (m_Animator != null && m_Animator.runtimeAnimatorController != null)
        {
            if (isForce)
                m_Animator.SetTriggerOneTime(status ? UIUtility.FORCE_ENABLE_ANIMATION_NAME : UIUtility.FORCE_DISABLE_ANIMATION_NAME);
            else
                m_Animator.SetTriggerOneTime(status ? UIUtility.ENABLE_ANIMATION_NAME : UIUtility.DISABLE_ANIMATION_NAME);
            return true;
        }
        return false;
    }

    public bool SetActive(bool status, bool isForce = false)
    {
        m_CanvasGroup.interactable = status ? true : false;
        m_CanvasGroup.blocksRaycasts = status ? true : false;
        if (m_Animator != null && m_Animator.runtimeAnimatorController != null)
        {
            if (isForce)
                m_Animator.SetTriggerOneTime(status ? UIUtility.FORCE_ON_ANIMATION_NAME : UIUtility.FORCE_OFF_ANIMATION_NAME);
            else
                m_Animator.SetTriggerOneTime(status ? UIUtility.SHOW_ANIMATION_NAME : UIUtility.HIDE_ANIMATION_NAME);
            return true;
        }
        return false;
    }


    protected override void Awake()
    {
        m_CachedCanvas = this.gameObject.GetOrAddComponent<Canvas>();
        m_CachedCanvas.overrideSorting = true;
        OriginalDepth = m_CachedCanvas.sortingOrder;
        m_CanvasGroup = this.gameObject.GetOrAddComponent<CanvasGroup>();
        m_Animator = GetComponent<Animator>();
        this.gameObject.GetOrAddComponent<GraphicRaycaster>();
    }

    protected virtual void OnInit(int parentDepth)
    {
        ParentDepth = parentDepth;
    }

    protected virtual void OnShow(UnityAction callback = null)
    {

        if (!SetActive(true))
        {
            // this.gameObject.SetActive(true);
            m_CanvasGroup.alpha = 1;
        }
        callback?.Invoke();
    }

    protected virtual void OnHide(UnityAction callback = null)
    {
        if (!SetActive(false))
        {
            // this.gameObject.SetActive(false);
            m_CanvasGroup.alpha = 0;
        }
        callback?.Invoke();
    }

    protected virtual void OnPause(UnityAction callback = null)
    {
        callback?.Invoke();
    }

    protected virtual void OnResume(UnityAction callback = null)
    {
        callback?.Invoke();
    }

    protected virtual void OnDepthChanged(int depthInForm)
    {
        int deltaDepth = ParentDepth + DepthFactor * depthInForm + OriginalDepth;
        m_CachedCanvas.sortingOrder = deltaDepth;
    }

    public virtual void OnUpdate()
    {

    }
}
