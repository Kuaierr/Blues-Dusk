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
    private Switcher m_Switcher;
    private SceneComponent m_SceneComponent = null;
    private EventComponent m_EventComponent = null;
    private EventHandler<DoTransitionCompleteEventArgs> m_SceneTransitEventHandler = null;
    private List<string> m_CachedLoadScenes = null;
    private List<string> m_CachedUnloadScenes = null;

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

    public void SetStartScene()
    {

        if (MultiScene)
        {
            StartScene = ActiveSceneName;
            CurrentScene = AssetUtility.GetSceneAsset(ActiveSceneName);
            Log.Info(ActiveSceneName);
        }
        else
        {
            CurrentScene = AssetUtility.GetSceneAsset(StartScene);
        }
    }

    public bool HasScene(string name)
    {
        return true;
    }

    protected override void Awake()
    {
        base.Awake();
        IsActive = true;
        m_CachedLoadScenes = new List<string>();
        m_CachedUnloadScenes = new List<string>();
        m_Switcher = GetComponentInChildren<Switcher>();
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
        m_EventComponent.Subscribe(DoTransitionCompleteEventArgs.EventId, OnDoTransitionComplete);
        m_EventComponent.Subscribe(UndoTransitionCompleteEventArgs.EventId, OnUndoTransitionComplete);
    }

    private void OnDestroy()
    {
        
        m_EventComponent.Unsubscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
        m_EventComponent.Unsubscribe(UnloadSceneSuccessEventArgs.EventId, OnUnloadSceneSuccess);
        m_EventComponent.Unsubscribe(UnloadSceneFailureEventArgs.EventId, OnUnloadSceneFailure);
        m_EventComponent.Unsubscribe(DoTransitionCompleteEventArgs.EventId, OnDoTransitionComplete);
        m_EventComponent.Unsubscribe(UndoTransitionCompleteEventArgs.EventId, OnUndoTransitionComplete);
        m_EventComponent.Unsubscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
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
        m_Switcher.animator.gameObject.SetActive(true);
        m_Switcher.animator.SetTrigger("Swicth");
        m_Switcher.animator.OnComplete(1f, () =>
        {
            UnloadSceneAsyn(CurrentScene, onSuccess: () =>
            {
                LoadSceneAsyn(name, onSuccess: () =>
                {
                    CurrentScene = name;
                    m_Switcher.animator.SetTrigger("UnSwicth");
                    m_Switcher.animator.OnComplete(1f, () =>
                    {
                        m_Switcher.animator.gameObject.SetActive(false);
                    });
                    onSuccess?.Invoke();
                });
            });
        });
    }

    public void SwitchSceneBySwipe(string name, UnityAction onSuccess = null, UnityAction onFail = null)
    {
        m_Switcher.swiper.gameObject.SetActive(true);
        m_Switcher.swiper.DOLocalMoveX(0, 0.5f).OnComplete(() =>
        {
            UnloadSceneAsyn(CurrentScene, onSuccess: () =>
            {
                LoadSceneAsyn(name, onSuccess: () =>
                {
                    CurrentScene = name;
                    m_Switcher.swiper.DOLocalMoveX(-2420f, 0.5f).OnComplete(() =>
                    {
                        m_Switcher.swiper.localPosition = new Vector3(2420f, m_Switcher.swiper.localPosition.y, m_Switcher.swiper.localPosition.z);
                        m_Switcher.swiper.gameObject.SetActive(false);
                    });
                    onSuccess?.Invoke();
                });
            });
        });
    }

    public void SwitchSceneByFade(string name, UnityAction onSuccess = null, UnityAction onFail = null)
    {
        m_Switcher.gradienter.gameObject.SetActive(true);
        m_Switcher.gradienter.DOFade(1, 0.5f).OnComplete(() =>
        {
            UnloadSceneAsyn(CurrentScene, onSuccess: () =>
            {
                // LoadSceneAsyn(name, () =>
                // {
                //     CurrentScene = name;
                //     m_Switcher.gradienter.DOFade(0f, 0.5f).OnComplete(() =>
                //     {
                //         m_Switcher.gradienter.gameObject.SetActive(false);
                //     });
                //     onSuccess?.Invoke();
                // });
            });
        });
    }

    public void SwitchScene(string name, UnityAction onSuccess = null, UnityAction onFail = null)
    {
        UnloadSceneAsyn(CurrentScene, onSuccess: () =>
        {
            LoadSceneAsyn(name, onSuccess: () =>
            {
                CurrentScene = name;
                onSuccess?.Invoke();
            });
        });
    }

    public void ReloadCurrentSceneSwipe()
    {
        m_Switcher.swiper.gameObject.SetActive(true);
        m_Switcher.swiper.DOLocalMoveX(0, 0.5f).OnComplete(() =>
        {
            UnloadSceneAsyn(CurrentScene, onSuccess: () =>
            {
                LoadSceneAsyn(CurrentScene, onSuccess: () =>
                {
                    m_Switcher.swiper.DOLocalMoveX(-2420f, 0.5f).OnComplete(() =>
                    {
                        m_Switcher.swiper.localPosition = new Vector3(2420f, m_Switcher.swiper.localPosition.y, m_Switcher.swiper.localPosition.z);
                        m_Switcher.swiper.gameObject.SetActive(false);
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

    public void LoadSceneAsyn(string name, SceneTransitionType transitionType = SceneTransitionType.Fade, UnityAction onSuccess = null, UnityAction onFail = null)
    {
        // Log.Info(name);
        // ScenesManager.instance.LoadSceneAsynAdd(name, onSuccess);
        // AddressableManager.instance.LoadSceneAsyn(name, loadMode: LoadSceneMode.Additive, onSuccess: onSuccess, onFail: onFail);
        // GameKitCenter.Scene.LoadScene(name, DoTransitionCompleteEventArgs.Create(name, transitionType, this));
        GameKitCenter.Scene.LoadScene(name, null);
    }

    public void UnloadSceneAsyn(string name, SceneTransitionType transitionType = SceneTransitionType.Fade, UnityAction onSuccess = null, UnityAction onFail = null)
    {
        // ScenesManager.instance.UnloadSceneAsyn(name, onSuccess);
        // AddressableManager.instance.UnloadSceneAsyn(name, onSuccess: onSuccess, onFail: onFail);
        // Log.Warning("UnloadSceneAsyn");
        GameKitCenter.Scene.UnloadScene(name, null);
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

    public void AddPreloadedScene(string sceneAssetName)
    {
        m_SceneComponent.AddPreloadedScene(sceneAssetName);
    }

    public void DoTransition(string loadName, List<string> unloadNames, SceneTransitionType switchType = SceneTransitionType.Fade)
    {
        m_CachedLoadScenes.Clear();
        m_CachedLoadScenes.Add(loadName);
        DoTransition(m_CachedLoadScenes, unloadNames, switchType);
    }

    public void DoTransition(string loadName, string unloadName, SceneTransitionType switchType = SceneTransitionType.Fade)
    {
        m_CachedLoadScenes.Clear();
        m_CachedUnloadScenes.Clear();
        m_CachedLoadScenes.Add(loadName);
        m_CachedUnloadScenes.Add(unloadName);
        DoTransition(m_CachedLoadScenes, m_CachedUnloadScenes, switchType);
    }

    public void DoTransition(string loadName, SceneTransitionType switchType = SceneTransitionType.Fade)
    {
        m_CachedLoadScenes.Clear();
        m_CachedUnloadScenes.Clear();
        m_CachedLoadScenes.Add(loadName);
        DoTransition(m_CachedLoadScenes, m_CachedUnloadScenes, switchType = SceneTransitionType.Fade);
    }

    public void DoTransition(List<string> loadNames, SceneTransitionType switchType = SceneTransitionType.Fade)
    {
        m_CachedUnloadScenes.Clear();
        DoTransition(loadNames, m_CachedUnloadScenes, switchType);
    }

    public void DoTransition(List<string> loadNames, List<string> unloadNames, SceneTransitionType switchType = SceneTransitionType.Fade)
    {
        if (switchType == SceneTransitionType.Swipe)
        {
            m_Switcher.swiper.gameObject.SetActive(true);
            m_Switcher.swiper.DOLocalMoveX(0, 0.5f).OnComplete(() =>
            {
                m_EventComponent.Fire(this, DoTransitionCompleteEventArgs.Create(loadNames, unloadNames, switchType, this));
            });
        }
        else if (switchType == SceneTransitionType.Fade)
        {
            m_Switcher.gradienter.gameObject.SetActive(true);
            m_Switcher.gradienter.DOFade(1, 0.5f).OnComplete(() =>
            {
                m_EventComponent.Fire(this, DoTransitionCompleteEventArgs.Create(loadNames, unloadNames, switchType, this));
            });
        }
        else if (switchType == SceneTransitionType.Animation)
        {
            m_Switcher.animator.gameObject.SetActive(true);
            m_Switcher.animator.SetTrigger("Do");
            m_Switcher.animator.OnComplete(1f, () =>
            {
                m_EventComponent.Fire(this, DoTransitionCompleteEventArgs.Create(loadNames, unloadNames, switchType, this));
            });
        }
        else if (switchType == SceneTransitionType.LoadingScene)
        {
            m_Switcher.gradienter.gameObject.SetActive(true);
            m_Switcher.gradienter.DOFade(1, 0.5f).OnComplete(() =>
            {
                m_EventComponent.Fire(this, DoTransitionCompleteEventArgs.Create(loadNames, unloadNames, switchType, this));
            });
        }
    }

    private void OnDoTransitionComplete(object sender, GameEventArgs e)
    {
        if (e == null)
        {
            Log.Warning("OnDoTransitionComplete is null");
            return;
        }

        DoTransitionCompleteEventArgs transitionArgs = (DoTransitionCompleteEventArgs)e;
        if (transitionArgs.UserData.GetType() != typeof(SchedulerComponent))
        {
            Log.Warning("OnDoTransitionComplete Not fired by Scheduler Component");
            return;
        }


        SceneTransitionType transitionType = transitionArgs.TransitionType;
        if (transitionArgs.RemoveAll)
        {
            m_SceneComponent.GetLoadedSceneAssetNames(m_CachedUnloadScenes);
            transitionArgs.ManuallySetRemoveNames(m_CachedUnloadScenes);
        }

        for (int i = 0; i < transitionArgs.RemoveNames.Count; i++)
        {
            m_SceneComponent.UnloadScene(transitionArgs.RemoveNames[i], transitionArgs);
        }

    }

    private void OnUnloadSceneSuccess(object sender, GameEventArgs e)
    {
        // sender 是 SceneComponent
        // args.UserData 是 SchedulerComponent
        // 只有通过scheduler加载和卸载才能出发回调
        // 所有unload结束后才会开始load
        UnloadSceneSuccessEventArgs args = (UnloadSceneSuccessEventArgs)e;
        if (args.UserData == null)
        {
            Log.Warning("DoTransitionCompleteEventArgs is null");
            return;
        }

        DoTransitionCompleteEventArgs transitionArgs = (DoTransitionCompleteEventArgs)args.UserData;
        if (transitionArgs.UserData == null)
        {
            Log.Warning("DoTransitionCompleteEventArgs is null when unload {0}", args.SceneAssetName);
            return;
        }

        if (transitionArgs.UserData.GetType() == typeof(SchedulerComponent))
        {
            Log.Success("Unload {0} Success with transition.", args.SceneAssetName);
            transitionArgs.RemoveNames.Remove(args.SceneAssetName);
            if (transitionArgs.RemoveCount == 0)
            {
                for (int i = 0; i < transitionArgs.TargetNames.Count; i++)
                {
                    m_SceneComponent.LoadScene(transitionArgs.TargetNames[i], DefaultScenePrioty, transitionArgs);
                }
            }
        }
    }

    private void OnLoadSceneSuccess(object sender, GameEventArgs e)
    {
        LoadSceneSuccessEventArgs args = (LoadSceneSuccessEventArgs)e;
        if (args.UserData == null)
        {
            Log.Warning("LoadSceneSuccessEventArgs is null, scheduler will ignore the load");
            return;
        }

        DoTransitionCompleteEventArgs transitionArgs = (DoTransitionCompleteEventArgs)args.UserData;
        if (transitionArgs.UserData == null)
        {
            Log.Warning("The transition is not fired by {0} when loading {1}", this.name, args.SceneAssetName);
            return;
        }

        if (transitionArgs.UserData.GetType() == typeof(SchedulerComponent))
        {
            Log.Success("Load {0} Success with transition.", args.SceneAssetName);
            transitionArgs.TargetNames.Remove(args.SceneAssetName);
            if (transitionArgs.TargetCount == 0)
            {
                m_EventComponent.Fire(this, UndoTransitionCompleteEventArgs.Create(transitionArgs.TransitionType, transitionArgs));
            }
        }
    }

    private void OnUndoTransitionComplete(object sender, GameEventArgs e)
    {
        UndoTransitionCompleteEventArgs args = (UndoTransitionCompleteEventArgs)e;
        DoTransitionCompleteEventArgs transitionArgs = (DoTransitionCompleteEventArgs)args.UserData;
        if (transitionArgs.GetType() != typeof(DoTransitionCompleteEventArgs))
        {
            Log.Warning("OnUndoTransitionComplete is fired without calling DoTransition");
            return;
        }
        UndoTransition(args.TransitionType);
        ReferencePool.Release(args);
        ReferencePool.Release(transitionArgs);
    }

    private void UndoTransition(SceneTransitionType switchType)
    {
        if (switchType == SceneTransitionType.Swipe)
        {
            m_Switcher.swiper.DOLocalMoveX(-2420f, 0.5f).OnComplete(HideSwitchSwiper);
        }
        else if (switchType == SceneTransitionType.Fade)
        {
            Log.Success("Fade");
            m_Switcher.gradienter.SetAlpha(1f);
            m_Switcher.gradienter.DOFade(0f, 5f).OnComplete(HideSwitchFader);
        }
        else if (switchType == SceneTransitionType.Animation)
        {
            m_Switcher.animator.SetTrigger("Undo");
            m_Switcher.animator.OnComplete(1f, HideSwitchAnimator);
        }
    }



    private void OnLoadSceneFailure(object sender, GameEventArgs e)
    {
        LoadSceneFailureEventArgs args = (LoadSceneFailureEventArgs)e;
        Log.Fail("Load {0} Fail", args.SceneAssetName);
    }

    private void OnUnloadSceneFailure(object sender, GameEventArgs e)
    {
        UnloadSceneFailureEventArgs args = (UnloadSceneFailureEventArgs)e;
        Log.Fail("Unload {0} Fail", args.SceneAssetName);
    }


    private void HideSwitchAnimator()
    {
        m_Switcher.animator.gameObject.SetActive(false);
    }

    private void HideSwitchSwiper()
    {
        m_Switcher.swiper.localPosition = new Vector3(2420f, m_Switcher.swiper.localPosition.y, m_Switcher.swiper.localPosition.z);
        m_Switcher.swiper.gameObject.SetActive(false);
    }

    private void HideSwitchFader()
    {
        m_Switcher.gradienter.gameObject.SetActive(false);
    }
}
