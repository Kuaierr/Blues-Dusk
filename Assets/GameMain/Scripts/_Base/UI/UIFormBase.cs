using System.Collections;
using System.Collections.Generic;
using UnityGameKit.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using GameKit.QuickCode;
using GameKit;

[RequireComponent(typeof(CanvasGroup), typeof(Animator))]
public abstract class UIFormBase : UIFormLogic
{
    public const int DepthFactor = 100;
    private const float FadeTime = 0.3f;
    private static Font s_MainFont = null;
    private RectTransform m_RectTransform = null;
    private Canvas m_CachedCanvas = null;
    private CanvasGroup m_CanvasGroup = null;
    private List<Canvas> m_CachedCanvasContainer = new List<Canvas>();
    private Dictionary<string, List<UIBehaviour>> m_UIFormChildren = new Dictionary<string, List<UIBehaviour>>();
    [SerializeField] private Animator m_MasterAnimator = null;
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

    public Animator MasterAnimator
    {
        get
        {
            return m_MasterAnimator;
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

    protected CanvasGroup CanvasGroup
    {
        get
        {
            return m_CanvasGroup;
        }
    }

    public void ChangeDisplayUpdate(KeyCode keyCode)
    {
        if (InputManager.instance.GetUiKeyDown(keyCode))
        {
            if (Visible)
                OnPause();
            else
                OnResume();
        }
    }

    public void Close()
    {
        OnClose(false, null);
    }

    public void Pause()
    {
        OnPause();
    }

    public void Resume()
    {
        OnResume();
    }

    public void OnInstantiate()
    {
        m_CachedCanvas = gameObject.GetOrAddComponent<Canvas>();
        m_CanvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();
        gameObject.GetOrAddComponent<GraphicRaycaster>();
        if (m_MasterAnimator == null)
            m_MasterAnimator = gameObject.GetOrAddComponent<Animator>();
        // InternalSetVisible(false);
    }

    public static void SetMainFont(Font mainFont)
    {
        if (mainFont == null)
        {
            Log.Error("Main font is invalid.");
            return;
        }
        s_MainFont = mainFont;
    }

    public virtual void OnUpdateInfo(object userData)
    {

    }

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        m_CachedCanvas.overrideSorting = true;
        OriginalDepth = m_CachedCanvas.sortingOrder;
        RectTransform transform = GetComponent<RectTransform>();
        transform.anchorMin = Vector2.zero;
        transform.anchorMax = Vector2.one;
        transform.anchoredPosition = Vector2.zero;
        transform.sizeDelta = Vector2.zero;

        // FindChildrenByType<UIFormChildBase>();

        // Text[] texts = GetComponentsInChildren<Text>(true);
        // for (int i = 0; i < texts.Length; i++)
        // {
        //     texts[i].font = s_MainFont;
        //     // if (!string.IsNullOrEmpty(texts[i].text))
        //     // {
        //     //     // texts[i].text = GameKitCenter.Localization.GetString(texts[i].text);
        //     // }
        // }
    }

    protected override void OnRecycle()
    {
        
        base.OnRecycle();
        Log.Warning("Recycle {0}", gameObject.name);
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        Log.Warning("Open {0}", gameObject.name);
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
        Log.Warning("Close {0}", gameObject.name);
    }

    protected override void OnPause()
    {
        base.OnPause();
        Log.Warning("OnPause {0}", gameObject.name);
    }

    protected override void OnResume()
    {
        base.OnResume();
        Log.Warning("OnResume {0}", gameObject.name);
    }

    protected override void OnCover()
    {
        base.OnCover();
    }

    protected override void OnReveal()
    {
        base.OnReveal();
    }

    protected override void OnRefocus(object userData)
    {
        base.OnRefocus(userData);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
    }

    protected override void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
    {
        int oldDepth = Depth;
        base.OnDepthChanged(uiGroupDepth, depthInUIGroup);
        int deltaDepth = UIFormBaseGroupHelper.DepthFactor * uiGroupDepth + DepthFactor * depthInUIGroup - oldDepth + OriginalDepth;
        GetComponentsInChildren(true, m_CachedCanvasContainer);
        for (int i = 0; i < m_CachedCanvasContainer.Count; i++)
        {
            m_CachedCanvasContainer[i].sortingOrder += deltaDepth;
        }
        m_CachedCanvasContainer.Clear();
    }

    protected override void InternalSetVisible(bool visible)
    {
        // Log.Warning("{0} InternalSetVisible {1}", gameObject.name, visible);
        SetCanvasGroupBlock(visible);
        if (m_MasterAnimator != null && m_MasterAnimator.runtimeAnimatorController != null)
        {
            m_MasterAnimator.SetTrigger(visible ? UIUtility.SHOW_ANIMATION_NAME : UIUtility.HIDE_ANIMATION_NAME);
            return;
        }
        SetActiveByAlpha(visible);
    }

    protected void SetActiveByAlpha(bool status)
    {
        CanvasGroup.alpha = status ? 1 : 0;
    }

    protected void SetCanvasGroupBlock(bool status)
    {
        CanvasGroup.blocksRaycasts = status;
        CanvasGroup.interactable = status;
    }

    private IEnumerator CloseCo(float duration)
    {
        yield return m_CanvasGroup.FadeToAlpha(0f, duration);
        GameKitCenter.UI.TryCloseUIForm(this);
    }

    private void FindChildrenByType<T>() where T : UIBehaviour
    {
        T[] components = this.GetComponentsInChildren<T>(true);
        for (int i = 0; i < components.Length; ++i)
        {
            string objName = components[i].gameObject.name;
            if (m_UIFormChildren.ContainsKey(objName))
                m_UIFormChildren[objName].Add(components[i]);
            else
                m_UIFormChildren.Add(objName, new List<UIBehaviour>() { components[i] });
        }
    }
}
