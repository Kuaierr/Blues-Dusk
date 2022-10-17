using GameKit;
// using GameKit.Resource;
using GameKit.Scene;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityGameKit.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Kit/GameKit Scene Component")]
    public sealed class SceneComponent : GameKitComponent
    {
        private const int DefaultPriority = 0;
        private ISceneManager m_SceneManager = null;
        private EventComponent m_EventComponent = null;
        private readonly SortedDictionary<string, int> m_SceneOrder = new SortedDictionary<string, int>(StringComparer.Ordinal);
        private Camera m_MainCamera = null;
        private Scene m_GameKitScene = default(Scene);
        [SerializeField]
        private string m_SceneHelperTypeName = "UnityGameKit.Runtime.DefaultSceneHelper";

        [SerializeField]
        private SceneHelperBase m_CustomSceneHelper = null;

        public Camera MainCamera
        {
            get
            {
                return m_MainCamera;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            m_SceneManager = GameKitModuleCenter.GetModule<ISceneManager>();
            if (m_SceneManager == null)
            {
                Log.Fatal("Scene manager is invalid.");
                return;
            }

            m_SceneManager.LoadSceneSuccess += OnLoadSceneSuccess;
            m_SceneManager.LoadSceneFailure += OnLoadSceneFailure;
            m_SceneManager.UnloadSceneSuccess += OnUnloadSceneSuccess;
            m_SceneManager.UnloadSceneFailure += OnUnloadSceneFailure;

            m_GameKitScene = SceneManager.GetSceneAt(GameKitComponentCenter.GameKitSceneId);

            SceneHelperBase sceneHelper = Helper.CreateHelper(m_SceneHelperTypeName, m_CustomSceneHelper);
            if (sceneHelper == null)
            {
                Log.Error("Can not create scene helper.");
                return;
            }

            sceneHelper.name = "Scene Helper";
            Transform transform = sceneHelper.transform;
            transform.SetParent(this.transform);
            transform.localScale = Vector3.one;

            m_SceneManager.SetSceneHelper(sceneHelper);

            if (!m_GameKitScene.IsValid())
            {
                Log.Fatal("Game Kit scene is invalid.");
                return;
            }
        }

        private void Start()
        {
            GameKitCoreComponent coreComponent = GameKitComponentCenter.GetComponent<GameKitCoreComponent>();
            if (coreComponent == null)
            {
                Log.Fatal("Base component is invalid.");
                return;
            }

            m_EventComponent = GameKitComponentCenter.GetComponent<EventComponent>();
            if (m_EventComponent == null)
            {
                Log.Fatal("Event component is invalid.");
                return;
            }

            // if (coreComponent.EditorResourceMode)
            // {
            //     m_SceneManager.SetResourceManager(coreComponent.EditorResourceHelper);
            // }
            // else
            // {
            //     m_SceneManager.SetResourceManager(GameKitModuleCenter.GetModule<IResourceManager>());
            // }
        }

        public static string GetSceneName(string sceneAssetName)
        {
            if (string.IsNullOrEmpty(sceneAssetName))
            {
                Log.Error("Scene asset name is invalid.");
                return null;
            }

            int sceneNamePosition = sceneAssetName.LastIndexOf('/');
            if (sceneNamePosition + 1 >= sceneAssetName.Length)
            {
                Log.Error("Scene asset name '{0}' is invalid.", sceneAssetName);
                return null;
            }

            string sceneName = sceneAssetName.Substring(sceneNamePosition + 1);
            sceneNamePosition = sceneName.LastIndexOf(".unity");
            if (sceneNamePosition > 0)
            {
                sceneName = sceneName.Substring(0, sceneNamePosition);
            }
            return sceneName;
        }

        public bool SceneIsLoaded(string sceneAssetName)
        {
            return m_SceneManager.SceneIsLoaded(sceneAssetName);
        }

        public string[] GetLoadedSceneAssetNames()
        {
            return m_SceneManager.GetLoadedSceneAssetNames();
        }

        public void GetLoadedSceneAssetNames(List<string> results)
        {
            m_SceneManager.GetLoadedSceneAssetNames(results);
        }

        public bool SceneIsLoading(string sceneAssetName)
        {
            return m_SceneManager.SceneIsLoading(sceneAssetName);
        }

        public string[] GetLoadingSceneAssetNames()
        {
            return m_SceneManager.GetLoadingSceneAssetNames();
        }

        public void GetLoadingSceneAssetNames(List<string> results)
        {
            m_SceneManager.GetLoadingSceneAssetNames(results);
        }

        public bool SceneIsUnloading(string sceneAssetName)
        {
            return m_SceneManager.SceneIsUnloading(sceneAssetName);
        }

        public string[] GetUnloadingSceneAssetNames()
        {
            return m_SceneManager.GetUnloadingSceneAssetNames();
        }

        public void GetUnloadingSceneAssetNames(List<string> results)
        {
            m_SceneManager.GetUnloadingSceneAssetNames(results);
        }

        public bool HasScene(string sceneAssetName)
        {
            if (string.IsNullOrEmpty(sceneAssetName))
            {
                Log.Error("Scene asset name is invalid.");
                return false;
            }

            if (!sceneAssetName.StartsWith("Assets/", StringComparison.Ordinal) || !sceneAssetName.EndsWith(".unity", StringComparison.Ordinal))
            {
                Log.Error("Scene asset name '{0}' is invalid.", sceneAssetName);
                return false;
            }

            return m_SceneManager.HasScene(sceneAssetName);
        }

        public void LoadScene(string sceneAssetName)
        {
            LoadScene(sceneAssetName, DefaultPriority, null);
        }

        public void LoadScene(string sceneAssetName, int priority)
        {
            LoadScene(sceneAssetName, priority, null);
        }

        public void LoadScene(string sceneAssetName, object userData)
        {
            LoadScene(sceneAssetName, DefaultPriority, userData);
        }

        public void LoadScene(string sceneAssetName, int priority, object userData)
        {
            if (string.IsNullOrEmpty(sceneAssetName))
            {
                Log.Error("Scene asset name is invalid.");
                return;
            }

            if (!sceneAssetName.StartsWith("Assets/", StringComparison.Ordinal) || !sceneAssetName.EndsWith(".unity", StringComparison.Ordinal))
            {
                Log.Error("Scene asset name '{0}' is invalid.", sceneAssetName);
                return;
            }
            m_SceneManager.LoadScene(sceneAssetName, priority, userData);
        }

        public void UnloadScene(string sceneAssetName)
        {
            UnloadScene(sceneAssetName, null);
        }

        public void UnloadScene(string sceneAssetName, object userData)
        {
            if (string.IsNullOrEmpty(sceneAssetName))
            {
                Log.Error("Scene asset name is invalid.");
                return;
            }

            if (!sceneAssetName.StartsWith("Assets/", StringComparison.Ordinal) || !sceneAssetName.EndsWith(".unity", StringComparison.Ordinal))
            {
                Log.Error("Scene asset name '{0}' is invalid.", sceneAssetName);
                return;
            }

            m_SceneManager.UnloadScene(sceneAssetName, userData);
            m_SceneOrder.Remove(sceneAssetName);
        }

        public void SetSceneOrder(string sceneAssetName, int sceneOrder)
        {
            if (string.IsNullOrEmpty(sceneAssetName))
            {
                Log.Error("Scene asset name is invalid.");
                return;
            }

            if (!sceneAssetName.StartsWith("Assets/", StringComparison.Ordinal) || !sceneAssetName.EndsWith(".unity", StringComparison.Ordinal))
            {
                Log.Error("Scene asset name '{0}' is invalid.", sceneAssetName);
                return;
            }

            if (SceneIsLoading(sceneAssetName))
            {
                m_SceneOrder[sceneAssetName] = sceneOrder;
                return;
            }

            if (SceneIsLoaded(sceneAssetName))
            {
                m_SceneOrder[sceneAssetName] = sceneOrder;
                RefreshSceneOrder();
                return;
            }

            Log.Error("Scene '{0}' is not loaded or loading.", sceneAssetName);
        }

        public void AddPreloadedScene(string sceneAssetName)
        {
            m_SceneManager.AddPreloadedScene(sceneAssetName);
            RefreshSceneOrder();
        }

        public void RefreshMainCamera()
        {
            m_MainCamera = Camera.main;
        }

        private void RefreshSceneOrder()
        {
            if (m_SceneOrder.Count > 0)
            {
                string maxSceneName = null;
                int maxSceneOrder = 0;
                foreach (KeyValuePair<string, int> sceneOrder in m_SceneOrder)
                {
                    if (SceneIsLoading(sceneOrder.Key))
                    {
                        continue;
                    }

                    if (maxSceneName == null)
                    {
                        maxSceneName = sceneOrder.Key;
                        maxSceneOrder = sceneOrder.Value;
                        continue;
                    }

                    if (sceneOrder.Value > maxSceneOrder)
                    {
                        maxSceneName = sceneOrder.Key;
                        maxSceneOrder = sceneOrder.Value;
                    }
                }

                if (maxSceneName == null)
                {
                    SetActiveScene(m_GameKitScene);
                    return;
                }

                Scene scene = SceneManager.GetSceneByName(GetSceneName(maxSceneName));
                if (!scene.IsValid())
                {
                    Log.Error("Active scene '{0}' is invalid.", maxSceneName);
                    return;
                }

                SetActiveScene(scene);
            }
            else
            {
                SetActiveScene(m_GameKitScene);
            }
        }

        private void SetActiveScene(Scene activeScene)
        {
            Scene lastActiveScene = SceneManager.GetActiveScene();
            if (lastActiveScene != activeScene)
            {
                SceneManager.SetActiveScene(activeScene);
                m_EventComponent.Fire(this, ActiveSceneChangedEventArgs.Create(lastActiveScene, activeScene));
            }

            RefreshMainCamera();
        }

        private void OnLoadSceneSuccess(object sender, GameKit.Scene.LoadSceneSuccessEventArgs e)
        {
            // Log.Info("GameKit.Scene.LoadSceneSuccessEventArgs");
            if (!m_SceneOrder.ContainsKey(e.SceneAssetName))
            {
                m_SceneOrder.Add(e.SceneAssetName, 0);
            }

            m_EventComponent.Fire(this, LoadSceneSuccessEventArgs.Create(e));
            RefreshSceneOrder();
        }

        private void OnLoadSceneFailure(object sender, GameKit.Scene.LoadSceneFailureEventArgs e)
        {
            // Log.Warning("Load scene failure, scene asset name '{0}', error message '{1}'.", e.SceneAssetName, e.ErrorMessage);
            m_EventComponent.Fire(this, LoadSceneFailureEventArgs.Create(e));
        }

        private void OnUnloadSceneSuccess(object sender, GameKit.Scene.UnloadSceneSuccessEventArgs e)
        {
            // Log.Info("GameKit.Scene.UnloadSceneSuccessEventArgs");
            m_EventComponent.Fire(this, UnloadSceneSuccessEventArgs.Create(e));
            m_SceneOrder.Remove(e.SceneAssetName);
            RefreshSceneOrder();
        }

        private void OnUnloadSceneFailure(object sender, GameKit.Scene.UnloadSceneFailureEventArgs e)
        {
            // Log.Warning("Unload scene failure, scene asset name '{0}'.", e.SceneAssetName);
            m_EventComponent.Fire(this, UnloadSceneFailureEventArgs.Create(e));
        }
    }
}
