using System.Collections;
using System.Collections.Generic;
using GameKit;
using UnityGameKit.Runtime;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup), typeof(Animator))]
public abstract class UIFormBase : UIFormLogic
{
    public const int DepthFactor = 100;
    private const float FadeTime = 0.3f;
    private static Font s_MainFont = null;
    private Canvas m_CachedCanvas = null;
    private CanvasGroup m_CanvasGroup = null;
    private List<Canvas> m_CachedCanvasContainer = new List<Canvas>();

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

    public void Close()
    {
        Close(false);
    }

    public void Close(bool ignoreFade)
    {
        StopAllCoroutines();

        if (ignoreFade)
        {
            GameKitCenter.UI.CloseUIForm(this);
        }
        else
        {
            // StartCoroutine(CloseCo(FadeTime));
        }
    }

    public static void SetMainFont(Font mainFont)
    {
        if (mainFont == null)
        {
            Utility.Debugger.LogError("Main font is invalid.");
            return;
        }

        s_MainFont = mainFont;
    }

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);

        m_CachedCanvas = gameObject.GetOrAddComponent<Canvas>();
        m_CachedCanvas.overrideSorting = true;
        OriginalDepth = m_CachedCanvas.sortingOrder;

        m_CanvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();

        RectTransform transform = GetComponent<RectTransform>();
        transform.anchorMin = Vector2.zero;
        transform.anchorMax = Vector2.one;
        transform.anchoredPosition = Vector2.zero;
        transform.sizeDelta = Vector2.zero;

        gameObject.GetOrAddComponent<GraphicRaycaster>();

        // Text[] texts = GetComponentsInChildren<Text>(true);
        // for (int i = 0; i < texts.Length; i++)
        // {
        //     texts[i].font = s_MainFont;
        //     if (!string.IsNullOrEmpty(texts[i].text))
        //     {
        //         // texts[i].text = GameEntry.Localization.GetString(texts[i].text);
        //     }
        // }
    }

    protected override void OnRecycle()
    {
        base.OnRecycle();
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);

        m_CanvasGroup.alpha = 0f;
        StopAllCoroutines();
        // StartCoroutine(m_CanvasGroup.FadeToAlpha(1f, FadeTime));
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
    }

    protected override void OnPause()
    {
        base.OnPause();
    }

    protected override void OnResume()
    {
        base.OnResume();

        m_CanvasGroup.alpha = 0f;
        StopAllCoroutines();
        // StartCoroutine(m_CanvasGroup.FadeToAlpha(1f, FadeTime));
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

    // private IEnumerator CloseCo(float duration)
    // {
    //     yield return m_CanvasGroup.FadeToAlpha(0f, duration);
    //     GameEntry.UI.CloseUIForm(this);
    // }
}
