using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using DG.Tweening;
using GameKitQuickCode;
using GameKit;
using GameKit.Event;

namespace UnityGameKit.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("GameKit/GameKit Scheduler Component")]
    public partial class SchedulerComponent : GameKitComponent
    {
        public bool IsActive;
        public SceneTransitionType defaultSwitchType = SceneTransitionType.Swipe;

        [Tooltip("初始场景")]
        public string StartScene = "GameKit_Main";

        [Tooltip("加载过场场景")]
        public string LoadScene = "GameKit_Loading";

        [SerializeField]
        private string m_ScenesPath = "";

        [SerializeField]
        private string m_MultiScenesPath = "";

        private const int DefaultScenePrioty = 0;
        private Switcher m_Switcher;
        private SceneComponent m_SceneComponent = null;
        private EventComponent m_EventComponent = null;
        private EventHandler<DoTransitionCompleteEventArgs> m_SceneTransitEventHandler = null;
        private List<string> m_CachedLoadScenes = null;
        private List<string> m_CachedUnloadScenes = null;
        private Sequence m_TransitionSequence;

        public bool MultiScene
        {
            get { return SceneManager.sceneCount > 1; }
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

        public void SetStartScene()
        {
            if (MultiScene)
            {
                StartScene = ActiveSceneName;
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
            m_TransitionSequence = DOTween.Sequence();
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
            // m_EventComponent.Unsubscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
            // m_EventComponent.Unsubscribe(UnloadSceneSuccessEventArgs.EventId, OnUnloadSceneSuccess);
            // m_EventComponent.Unsubscribe(UnloadSceneFailureEventArgs.EventId, OnUnloadSceneFailure);
            // m_EventComponent.Unsubscribe(DoTransitionCompleteEventArgs.EventId, OnDoTransitionComplete);
            // m_EventComponent.Unsubscribe(UndoTransitionCompleteEventArgs.EventId, OnUndoTransitionComplete);
            // m_EventComponent.Unsubscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
        }

        public void LoadSceneAsyn(string name, SceneTransitionType transitionType = SceneTransitionType.Fade,
            UnityAction onSuccess = null, UnityAction onFail = null)
        {
            // Log.Info(name);
            m_SceneComponent.LoadScene(name, null);
        }

        public void UnloadSceneAsyn(string name, SceneTransitionType transitionType = SceneTransitionType.Fade,
            UnityAction onSuccess = null, UnityAction onFail = null)
        {
            // Log.Warning("UnloadSceneAsyn");
            m_SceneComponent.UnloadScene(name, null);
        }

        public string[] GetLoadedSceneAssetNames()
        {
            return m_SceneComponent.GetLoadedSceneAssetNames();
        }

        public void AddPreloadedScene(string sceneAssetName)
        {
            m_SceneComponent.AddPreloadedScene(sceneAssetName);
        }

        public void DoTransition(string loadName, List<string> unloadNames,
            SceneTransitionType switchType = SceneTransitionType.Fade)
        {
            m_CachedLoadScenes.Clear();
            m_CachedLoadScenes.Add(loadName);
            DoTransition(m_CachedLoadScenes, unloadNames, switchType);
        }

        public void DoTransition(string loadName, string unloadName,
            SceneTransitionType switchType = SceneTransitionType.Fade)
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
            DoTransition(m_CachedLoadScenes, m_CachedUnloadScenes, switchType);
        }

        public void DoTransition(List<string> loadNames, SceneTransitionType switchType = SceneTransitionType.Fade)
        {
            m_CachedUnloadScenes.Clear();
            DoTransition(loadNames, m_CachedUnloadScenes, switchType);
        }

        public void DoTransition(List<string> loadNames, List<string> unloadNames,
            SceneTransitionType switchType = SceneTransitionType.Fade)
        {
            m_TransitionSequence.Kill();
            m_TransitionSequence = DOTween.Sequence();
            if (switchType == SceneTransitionType.Swipe)
            {
                m_Switcher.swiper.gameObject.SetActive(true);
                m_TransitionSequence.Append(m_Switcher.swiper.DOLocalMoveX(0, 0.5f).OnComplete(() =>
                {
                    m_EventComponent.Fire(this,
                        DoTransitionCompleteEventArgs.Create(loadNames, unloadNames, switchType, this));
                }));
            }
            else if (switchType == SceneTransitionType.Fade)
            {
                m_Switcher.gradienter.gameObject.SetActive(true);
                m_TransitionSequence.Append(m_Switcher.gradienter.DOFade(1, 0.5f).OnComplete(() =>
                {
                    m_EventComponent.Fire(this,
                        DoTransitionCompleteEventArgs.Create(loadNames, unloadNames, switchType, this));
                }));
            }
            else if (switchType == SceneTransitionType.Animation)
            {
                m_Switcher.animator.gameObject.SetActive(true);
                m_Switcher.animator.SetTrigger("Do");
                m_Switcher.animator.OnComplete(1f,
                    () =>
                    {
                        m_EventComponent.Fire(this,
                            DoTransitionCompleteEventArgs.Create(loadNames, unloadNames, switchType, this));
                    });
            }
            else if (switchType == SceneTransitionType.LoadingScene)
            {
                m_Switcher.loading.gameObject.SetActive(true);
                //m_TransitionSequence.Append(m_Switcher.gradienter.DOFade(1, 0.5f).OnComplete(() =>
                //{
                m_EventComponent.Fire(this,
                    DoTransitionCompleteEventArgs.Create(loadNames, unloadNames, switchType, this));
                //}));
            }
        }

        private void OnDoTransitionComplete(object sender, BaseEventArgs e)
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

            StartCoroutine(RemoveSceneProcess(transitionArgs));

            // for (int i = 0; i < transitionArgs.RemoveNames.Count; i++)
            // {
            //     m_SceneComponent.UnloadScene(transitionArgs.RemoveNames[i], transitionArgs);
            // }
        }

        private void OnUnloadSceneSuccess(object sender, BaseEventArgs e)
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

        private void OnLoadSceneSuccess(object sender, BaseEventArgs e)
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
                    m_EventComponent.Fire(this,
                        UndoTransitionCompleteEventArgs.Create(transitionArgs.TransitionType, transitionArgs));
                }
            }
        }

        private async void OnUndoTransitionComplete(object sender, BaseEventArgs e)
        {
            UndoTransitionCompleteEventArgs args = (UndoTransitionCompleteEventArgs)e;
            DoTransitionCompleteEventArgs transitionArgs = (DoTransitionCompleteEventArgs)args.UserData;
            if (transitionArgs.GetType() != typeof(DoTransitionCompleteEventArgs))
            {
                Log.Warning("OnUndoTransitionComplete is fired without calling DoTransition");
                return;
            }

            switch (args.TransitionType)
            {
                case SceneTransitionType.Fade:
                    await Task.Yield();
                    break;
                case SceneTransitionType.LoadingScene:
                    float timer = UnityEngine.Random.Range(5f, 10f);
                    float time = 0;
                    while (timer > time)
                    {
                        await Task.Yield();
                        time += Time.deltaTime;
                        if (time > timer)
                            m_Switcher.loading.UpdateLoadingPrecent(1);
                        else
                            m_Switcher.loading.UpdateLoadingPrecent(time / timer);
                    }
                    m_Switcher.loading.UpdateLoadingPrecent(1);
                    break;
                default:
                    await Task.Yield();
                    break;
            }

            Debug.Log("Debugger : UndoTransition");
            UndoTransition(args.TransitionType);
            ReferencePool.Release(args);
            ReferencePool.Release(transitionArgs);
        }

        private void UndoTransition(SceneTransitionType switchType) 
        {
            Debug.Log("Debugger : " + switchType);
            
            m_TransitionSequence.Kill();
            m_TransitionSequence = DOTween.Sequence();
            //if (switchType == SceneTransitionType.Swipe)
            if (m_Switcher.Swipe)
            {
                m_TransitionSequence.Append(m_Switcher.swiper.DOLocalMoveX(-2420f, 0.5f).OnComplete(HideSwitchSwiper));
            }
            //else if (switchType == SceneTransitionType.Fade)
            else if (m_Switcher.Fade)
            {
                Log.Success("Fade");
                m_Switcher.gradienter.SetAlpha(1f);
                m_TransitionSequence.Append(m_Switcher.gradienter.DOFade(0f, 5f).OnComplete(HideSwitchFader));
            }
            //else if (switchType == SceneTransitionType.Animation)
            else if (m_Switcher.Animating)
            {
                m_Switcher.animator.SetTrigger("Undo");
                m_Switcher.animator.OnComplete(1f, HideSwitchAnimator);
            }
            //else if (switchType == SceneTransitionType.LoadingScene)
            else if (m_Switcher.Loading)
            {
                m_Switcher.loading.gameObject.SetActive(false);
            }
            else
            {
                m_Switcher.loading.gameObject.SetActive(false);
                
                Log.Success("Fade");
                m_Switcher.gradienter.SetAlpha(1f);
                m_TransitionSequence.Append(m_Switcher.gradienter.DOFade(0f, 5f).OnComplete(HideSwitchFader));
            }
        }

        private void OnLoadSceneFailure(object sender, BaseEventArgs e)
        {
            LoadSceneFailureEventArgs args = (LoadSceneFailureEventArgs)e;
            Log.Fail("Load {0} Fail", args.SceneAssetName);
        }

        private void OnUnloadSceneFailure(object sender, BaseEventArgs e)
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
            m_Switcher.swiper.localPosition = new Vector3(2420f, m_Switcher.swiper.localPosition.y,
                m_Switcher.swiper.localPosition.z);
            m_Switcher.swiper.gameObject.SetActive(false);
        }

        private void HideSwitchFader()
        {
            m_Switcher.gradienter.gameObject.SetActive(false);
        }

        IEnumerator RemoveSceneProcess(DoTransitionCompleteEventArgs transitionArgs)
        {
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < transitionArgs.RemoveNames.Count; i++)
            {
                m_SceneComponent.UnloadScene(transitionArgs.RemoveNames[i], transitionArgs);
            }
        }
    }
}