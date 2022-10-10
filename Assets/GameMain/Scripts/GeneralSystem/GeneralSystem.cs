using System;
using System.Collections.Generic;
using GameKit.Event;
using UnityEngine.Events;
using UnityGameKit.Runtime;

public class GeneralSystem : MonoSingletonBase<GeneralSystem>
{
    private string m_CachedTipContent;
    private string m_CachedTipConfirm;
    private string m_CachedTipCancel;
    private UnityAction m_CachedConfirmCallback;
    private UnityAction m_CachedCancelCallback;
    private List<string> m_CachedAvailiableScenes;

    private UI_Tip m_CachedUITip;
    private UI_SelectScene m_CachedUISelectScene;

    private void Start()
    {
        GameKitCenter.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUISuccess);
        m_CachedTipContent = string.Empty;
        m_CachedTipConfirm = string.Empty;
        m_CachedTipCancel = string.Empty;
        m_CachedConfirmCallback = null;
        m_CachedCancelCallback = null;
        m_CachedAvailiableScenes = new List<string>();
    }

    public void OpenTipUI(string contentText, UnityAction confirmCallback, UnityAction cancelCallback = null, string confirmText = "<None>", string cancelText = "<None>")
    {
        GameKitCenter.UI.TryOpenUIForm("UI_Tip", userData: typeof(UI_Tip));

        m_CachedTipContent = contentText;
        m_CachedTipConfirm = confirmText;
        m_CachedTipCancel = cancelText;
        m_CachedConfirmCallback = confirmCallback;
        m_CachedCancelCallback = cancelCallback;
        if (m_CachedUITip != null)
        {
            m_CachedUITip.Show();
            m_CachedUITip.UpdateTip(m_CachedTipContent, m_CachedConfirmCallback, m_CachedCancelCallback, m_CachedTipConfirm, m_CachedTipCancel);
        }
    }

    public void OpenSelectSceneUI(List<string> availaibleScene)
    {
        GameKitCenter.UI.TryOpenUIForm("UI_SelectScene", userData: typeof(UI_SelectScene));

        foreach (string s in availaibleScene)
            m_CachedAvailiableScenes.Add(s);
        
        if (m_CachedUISelectScene != null)
        {
            m_CachedUISelectScene.Show();
            m_CachedUISelectScene.UpdateScenes(m_CachedAvailiableScenes);
            ClearAvailiableScenes();
        }
    }

    private void OnOpenUISuccess(object sender, GameEventArgs e)
    {
        // Log.Success("GeneralSystem detect dialog started.");
        OpenUIFormSuccessEventArgs args = (OpenUIFormSuccessEventArgs)e;
        if (args == null || args.UserData == null)
        {
            Log.Warning("UserData is null.");
            return;
        }

        if (args.UserData.Equals(typeof(UI_Tip)))
        {
            m_CachedUITip = (UI_Tip)args.UIForm.Logic;
            m_CachedUITip.UpdateTip(m_CachedTipContent, m_CachedConfirmCallback, m_CachedCancelCallback, m_CachedTipConfirm, m_CachedTipCancel);
            ClearTipCache();
        }

        if (args.UserData.Equals(typeof(UI_SelectScene)))
        {
            m_CachedUISelectScene = (UI_SelectScene)args.UIForm.Logic;
            m_CachedUISelectScene.UpdateScenes(m_CachedAvailiableScenes);
            ClearAvailiableScenes();
        }
    }

    public void ClearTipCache()
    {
        m_CachedTipContent = string.Empty;
        m_CachedTipConfirm = string.Empty;
        m_CachedTipCancel = string.Empty;
        m_CachedConfirmCallback = null;
        m_CachedCancelCallback = null;
    }

    public void ClearAvailiableScenes()
    {
        m_CachedAvailiableScenes.Clear();
    }
}