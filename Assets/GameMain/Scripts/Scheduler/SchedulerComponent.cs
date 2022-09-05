using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using DG.Tweening;
using GameKitQuickCode;
using GameKit;
using GameKit.Event;
using UnityGameKit.Runtime;

[DisallowMultipleComponent]
[AddComponentMenu("GameKit/GameKit Scheduler Component")]
public partial class SchedulerComponent : GameKitComponent
{
    public bool IsActive;
    public SceneTransitionType defaultSwitchType = SceneTransitionType.Swipe;
    [Tooltip("初始场景")]
    public string StartScene = "GameKit_Main";
    [Tooltip("当前激活场景")]
    public string CurrentScene = "GameKit_Main";
    [Tooltip("加载过场场景")]
    public string LoadingScene = "GameKit_Loading";

    private const int DefaultScenePrioty = 0;
    private Switcher switcher;
    private SceneComponent m_SceneComponent = null;
    private EventComponent m_EventComponent = null;
    private EventHandler<DoTransitionCompleteEventArgs> m_SceneTransitEventHandler = null;


    public bool MultiScene
    {
        get
        {

            return SceneManager.sceneCount > 1;
        }
    }

    public string ActiveSceneName
    {
        get
        {
            if (MultiScene)
                return SceneManager.GetSceneAt(1).name;
            return string.Empty;
        }
    }

    // public string CurrentAssetName
    // {
    //     get
    //     {
    //         return AssetUtility.GetSceneAsset(CurrentScene);
    //     }
    // }

    public void Init()
    {

        if (MultiScene)
        {
            StartScene = ActiveSceneName;
            CurrentScene = AssetUtility.GetSceneAsset(ActiveSceneName);
        }
        else
        {
            CurrentScene = AssetUtility.GetSceneAsset(StartScene);
            // StartScene = AssetUtility.GetSceneAsset(StartScene);
        }
        Log.Info(ActiveSceneName);
        // else
        //     LoadSceneAsyn(StartScene, onSuccess: () => { CurrentScene = StartScene; });
    }

    public bool HasScene(string name)
    {
        return true;
    }

    protected override void Awake()
    {
        base.Awake();
        IsActive = true;
        // m_SceneTransitEventHandler += OnTransitScene;

        switcher = GetComponentInChildren<Switcher>();
    }

    private void Start()
    {
        m_SceneComponent = GameKitComponentCenter.GetComponent<SceneComponent>();
        if (m_SceneComponent == null)
        {
            Log.Fatal("Scene component is invalid.");
            return;
        }

        m_EventComponent = GameKitComponentCenter.GetComponent<EventComponent>();
        if (m_EventComponent == null)
        {
            Log.Fatal("Event component is invalid.");
            return;
        }

        m_EventComponent.Subscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
        m_EventComponent.Subscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
        m_EventComponent.Subscribe(UnloadSceneSuccessEventArgs.EventId, OnUnloadSceneSuccess);
        m_EventComponent.Subscribe(UnloadSceneFailureEventArgs.EventId, OnUnloadSceneFailure);
        // m_EventComponent.Subscribe(DoTransitionCompleteEventArgs.EventId, OnDoTransitionComplete);
        // m_EventComponent.Subscribe(UndoTransitionCompleteEventArgs.EventId, OnUndoTransitionComplete);
    }

    private void OnDestroy()
    {
        m_EventComponent.Unsubscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
        m_EventComponent.Unsubscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
        m_EventComponent.Unsubscribe(UnloadSceneSuccessEventArgs.EventId, OnUnloadSceneSuccess);
        m_EventComponent.Unsubscribe(UnloadSceneFailureEventArgs.EventId, OnUnloadSceneFailure);
        // m_EventComponent.Unsubscribe(DoTransitionCompleteEventArgs.EventId, OnDoTransitionComplete);
        // m_EventComponent.Unsubscribe(UndoTransitionCompleteEventArgs.EventId, OnUndoTransitionComplete);
    }

    public void LoadSceneWithProcedure(SceneTransitionType switchType = SceneTransitionType.None)
    {

    }

    public void SwitchSceneByDefault(string name, UnityAction onSuccess = null, UnityAction onFail = null)
    {
        if (defaultSwitchType == SceneTransitionType.Swipe)
            SwitchSceneBySwipe(name, onSuccess, onFail);
        else if (defaultSwitchType == SceneTransitionType.LoadingScene)
            SwitchSceneByLoadingScene(name, onSuccess, onFail);
        else if (defaultSwitchType == SceneTransitionType.Fade)
            SwitchSceneByFade(name, onSuccess, onFail);
        else if (defaultSwitchType == SceneTransitionType.Animation)
            SwitchSceneByAnimation(name, onSuccess, onFail);
        else if (defaultSwitchType == SceneTransitionType.Immediately)
            SwitchScene(name, onSuccess, onFail);
    }
    public void SwitchSceneByAnimation(string name, UnityAction onSuccess = null, UnityAction onFail = null)
    {
        switcher.animator.gameObject.SetActive(true);
        switcher.animator.SetTrigger("Swicth");
        switcher.animator.OnComplete(1f, () =>
        {
            UnloadSceneAsyn(CurrentScene, () =>
            {
                LoadSceneAsyn(name, () =>
                {
                    CurrentScene = name;
                    switcher.animator.SetTrigger("UnSwicth");
                    switcher.animator.OnComplete(1f, () =>
                    {
                        switcher.animator.gameObject.SetActive(false);
                    });
                    onSuccess?.Invoke();
                });
            });
        });
    }

    public void SwitchSceneBySwipe(string name, UnityAction onSuccess = null, UnityAction onFail = null)
    {
        switcher.swiper.gameObject.SetActive(true);
        switcher.swiper.DOLocalMoveX(0, 0.5f).OnComplete(() =>
        {
            UnloadSceneAsyn(CurrentScene, () =>
            {
                LoadSceneAsyn(name, () =>
                {
                    CurrentScene = name;
                    switcher.swiper.DOLocalMoveX(-2420f, 0.5f).OnComplete(() =>
                    {
                        switcher.swiper.localPosition = new Vector3(2420f, switcher.swiper.localPosition.y, switcher.swiper.localPosition.z);
                        switcher.swiper.gameObject.SetActive(false);
                    });
                    onSuccess?.Invoke();
                });
            });
        });
    }

    public void SwitchSceneByFade(string name, UnityAction onSuccess = null, UnityAction onFail = null)
    {
        switcher.gradienter.gameObject.SetActive(true);
        switcher.gradienter.DOFade(1, 0.5f).OnComplete(() =>
        {
            UnloadSceneAsyn(CurrentScene, () =>
            {
                // LoadSceneAsyn(name, () =>
                // {
                //     CurrentScene = name;
                //     switcher.gradienter.DOFade(0f, 0.5f).OnComplete(() =>
                //     {
                //         switcher.gradienter.gameObject.SetActive(false);
                //     });
                //     onSuccess?.Invoke();
                // });
            });
        });
    }

    public void SwitchScene(string name, UnityAction onSuccess = null, UnityAction onFail = null)
    {
        UnloadSceneAsyn(CurrentScene, () =>
        {
            LoadSceneAsyn(name, () =>
            {
                CurrentScene = name;
                onSuccess?.Invoke();
            });
        });
    }

    public void ReloadCurrentSceneSwipe()
    {
        switcher.swiper.gameObject.SetActive(true);
        switcher.swiper.DOLocalMoveX(0, 0.5f).OnComplete(() =>
        {
            UnloadSceneAsyn(CurrentScene, () =>
            {
                LoadSceneAsyn(CurrentScene, () =>
                {
                    switcher.swiper.DOLocalMoveX(-2420f, 0.5f).OnComplete(() =>
                    {
                        switcher.swiper.localPosition = new Vector3(2420f, switcher.swiper.localPosition.y, switcher.swiper.localPosition.z);
                        switcher.swiper.gameObject.SetActive(false);
                    });
                });
            });
        });
    }

    public void SwitchSceneByLoadingScene(string name, UnityAction onSuccess = null, UnityAction onFail = null)
    {
        ScenesManager.instance.TryGetScene(name, out Scene scene);

        if (scene == null)
        {
            Debug.LogError("No such scene in build settings.");
            return;
        }
        SwitchScene(LoadingScene, onSuccess);
    }

    public void LoadSceneAsyn(string name, UnityAction onSuccess = null, UnityAction onFail = null)
    {
        // Log.Info(name);
        // ScenesManager.instance.LoadSceneAsynAdd(name, onSuccess);
        // AddressableManager.instance.LoadSceneAsyn(name, loadMode: LoadSceneMode.Additive, onSuccess: onSuccess, onFail: onFail);
        GameKitCenter.Scene.LoadScene(name);
    }

    public void UnloadSceneAsyn(string name, UnityAction onSuccess = null, UnityAction onFail = null)
    {
        // ScenesManager.instance.UnloadSceneAsyn(name, onSuccess);
        // AddressableManager.instance.UnloadSceneAsyn(name, onSuccess: onSuccess, onFail: onFail);
        // Log.Warning(name);
        GameKitCenter.Scene.UnloadScene(name);
    }

    public int GetActiveSceneNumber()
    {
        if (SceneManager.sceneCount > 1)
        {
            int order = 0;
            string[] levelSplit = SceneManager.GetSceneAt(1).name.Split('_');
            if (levelSplit.Length <= 1)
                return 0;
            System.Int32.TryParse(levelSplit[1], out order);
            return order;
        }
        Debug.LogWarning("No Active Level in Scene");
        return 0;
    }

    // private void DoTransition(string name, SceneTransitionType switchType)
    // {
    //     if (switchType == SceneTransitionType.Swipe)
    //     {
    //         switcher.swiper.gameObject.SetActive(true);
    //         switcher.swiper.DOLocalMoveX(0, 0.5f).OnComplete(() =>
    //         {
    //             m_EventComponent.Fire(this, DoTransitionCompleteEventArgs.Create(name, switchType, this));
    //         });
    //     }
    //     else if (switchType == SceneTransitionType.Fade)
    //     {
    //         switcher.gradienter.gameObject.SetActive(true);
    //         switcher.gradienter.DOFade(1, 0.5f).OnComplete(() =>
    //         {
    //             m_EventComponent.Fire(this, DoTransitionCompleteEventArgs.Create(name, switchType, this));
    //         });
    //     }
    //     else if (switchType == SceneTransitionType.Animation)
    //     {
    //         switcher.animator.gameObject.SetActive(true);
    //         switcher.animator.SetTrigger("Do");
    //         switcher.animator.OnComplete(1f, () =>
    //         {
    //             m_EventComponent.Fire(this, DoTransitionCompleteEventArgs.Create(name, switchType, this));
    //         });
    //     }
    //     else if (switchType == SceneTransitionType.LoadingScene)
    //     {
    //         switcher.gradienter.gameObject.SetActive(true);
    //         switcher.gradienter.DOFade(1, 0.5f).OnComplete(() =>
    //         {
    //             m_EventComponent.Fire(this, DoTransitionCompleteEventArgs.Create(name, switchType, this));
    //         });
    //     }
    // }

    // private void UndoTransition(string name, SceneTransitionType switchType)
    // {
    //     if (switchType == SceneTransitionType.Swipe)
    //     {
    //         // m_EventComponent.Fire(this, );
    //     }
    //     else if (switchType == SceneTransitionType.LoadingScene)
    //     {

    //     }
    //     else if (switchType == SceneTransitionType.Fade)
    //     {

    //     }
    //     else if (switchType == SceneTransitionType.Animation)
    //     {

    //     }
    // }

    private void OnLoadSceneSuccess(object sender, GameEventArgs e)
    {
        LoadSceneSuccessEventArgs args = (LoadSceneSuccessEventArgs)e;
        Debug.Log("Load Success");
    }

    private void OnLoadSceneFailure(object sender, GameEventArgs e)
    {
        LoadSceneFailureEventArgs args = (LoadSceneFailureEventArgs)e;
        Debug.Log("Load Fail");
    }

    private void OnUnloadSceneSuccess(object sender, GameEventArgs e)
    {
        UnloadSceneSuccessEventArgs args = (UnloadSceneSuccessEventArgs)e;
        Log.Warning(sender.GetType());

        // DoTransitionCompleteEventArgs transitionArgs = (DoTransitionCompleteEventArgs)args.UserData;
        // if (transitionArgs != null)
        // {
        //     m_SceneComponent.LoadScene(AssetUtility.GetSceneAsset(transitionArgs.TargetName), DefaultScenePrioty, this);
        //     Debug.Log("UnLoad Success");
        // }
    }

    private void OnUnloadSceneFailure(object sender, GameEventArgs e)
    {
        UnloadSceneFailureEventArgs args = (UnloadSceneFailureEventArgs)e;
        Debug.Log("UnLoad Fail");
    }

    // private void OnDoTransitionComplete(object sender, GameEventArgs e)
    // {
    //     DoTransitionCompleteEventArgs args = (DoTransitionCompleteEventArgs)e;
    //     if (args.UserData != this)
    //     {
    //         Log.Fail("OnDoTransitionComplete: Not fired by myself");
    //         return;
    //     }

    //     SceneTransitionType transitionType = args.TransitionType;
    //     string[] loadedSceneAssetNames = m_SceneComponent.GetLoadedSceneAssetNames();
    //     for (int i = 0; i < loadedSceneAssetNames.Length; i++)
    //         m_SceneComponent.UnloadScene(loadedSceneAssetNames[i]);
    // }

    // private void OnUndoTransitionComplete(object sender, GameEventArgs e)
    // {
    //     UndoTransitionCompleteEventArgs args = (UndoTransitionCompleteEventArgs)e;
    //     if (args.UserData != this)
    //     {
    //         Log.Fail("OnUndoTransitionComplete: Not fired by myself");
    //         return;
    //     }
    // }

    // private void OnTransitScene(object sender, DoTransitionCompleteEventArgs e)
    // {
    //     if (e.UserData != this)
    //     {
    //         Log.Fail("OnDoTransitionComplete: Not fired by myself");
    //         return;
    //     }
    //     SceneTransitionType transitionType = e.TransitionType;
    //     m_SceneComponent.LoadScene(e.TargetName, DefaultScenePrioty, null);
    // }
}
