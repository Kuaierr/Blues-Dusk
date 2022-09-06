// using GameKit.Resource;
using System;
using System.Collections.Generic;

namespace GameKit.Scene
{
    internal sealed class SceneManager : GameKitModule, ISceneManager
    {
        private readonly List<string> m_LoadedSceneAssetNames;
        private readonly List<string> m_LoadingSceneAssetNames;
        private readonly List<string> m_UnloadingSceneAssetNames;
        private readonly LoadSceneCallbacks m_LoadSceneCallbacks;
        private readonly UnloadSceneCallbacks m_UnloadSceneCallbacks;
        // private IResourceManager m_ResourceManager;
        private EventHandler<LoadSceneSuccessEventArgs> m_LoadSceneSuccessEventHandler;
        private EventHandler<LoadSceneFailureEventArgs> m_LoadSceneFailureEventHandler;
        private EventHandler<UnloadSceneSuccessEventArgs> m_UnloadSceneSuccessEventHandler;
        private EventHandler<UnloadSceneFailureEventArgs> m_UnloadSceneFailureEventHandler;
        private ISceneHelper m_SceneHelper;

        public SceneManager()
        {
            m_LoadedSceneAssetNames = new List<string>();
            m_LoadingSceneAssetNames = new List<string>();
            m_UnloadingSceneAssetNames = new List<string>();
            m_LoadSceneCallbacks = new LoadSceneCallbacks(LoadSceneSuccessCallback, LoadSceneFailureCallback);
            m_UnloadSceneCallbacks = new UnloadSceneCallbacks(UnloadSceneSuccessCallback, UnloadSceneFailureCallback);
            // m_ResourceManager = null;
            m_LoadSceneSuccessEventHandler = null;
            m_LoadSceneFailureEventHandler = null;
            m_UnloadSceneSuccessEventHandler = null;
            m_UnloadSceneFailureEventHandler = null;
        }

        internal override int Priority
        {
            get
            {
                return 2;
            }
        }

        public event EventHandler<LoadSceneSuccessEventArgs> LoadSceneSuccess
        {
            add
            {
                m_LoadSceneSuccessEventHandler += value;
            }
            remove
            {
                m_LoadSceneSuccessEventHandler -= value;
            }
        }

        public event EventHandler<LoadSceneFailureEventArgs> LoadSceneFailure
        {
            add
            {
                m_LoadSceneFailureEventHandler += value;
            }
            remove
            {
                m_LoadSceneFailureEventHandler -= value;
            }
        }

        public event EventHandler<UnloadSceneSuccessEventArgs> UnloadSceneSuccess
        {
            add
            {
                m_UnloadSceneSuccessEventHandler += value;
            }
            remove
            {
                m_UnloadSceneSuccessEventHandler -= value;
            }
        }

        public event EventHandler<UnloadSceneFailureEventArgs> UnloadSceneFailure
        {
            add
            {
                m_UnloadSceneFailureEventHandler += value;
            }
            remove
            {
                m_UnloadSceneFailureEventHandler -= value;
            }
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {

        }

        internal override void Shutdown()
        {
            string[] loadedSceneAssetNames = m_LoadedSceneAssetNames.ToArray();
            foreach (string loadedSceneAssetName in loadedSceneAssetNames)
            {
                if (SceneIsUnloading(loadedSceneAssetName))
                {
                    continue;
                }

                UnloadScene(loadedSceneAssetName);
            }

            m_LoadedSceneAssetNames.Clear();
            m_LoadingSceneAssetNames.Clear();
            m_UnloadingSceneAssetNames.Clear();
        }

        // public void SetResourceManager(IResourceManager resourceManager)
        // {
        //     if (resourceManager == null)
        //     {
        //         throw new GameKitException("Resource manager is invalid.");
        //     }

        //     m_ResourceManager = resourceManager;
        // }

        public void SetSceneHelper(ISceneHelper helper)
        {
            m_SceneHelper = helper;
        }

        public bool SceneIsLoaded(string sceneAssetName)
        {
            if (string.IsNullOrEmpty(sceneAssetName))
            {
                throw new GameKitException("Scene asset name is invalid.");
            }

            return m_LoadedSceneAssetNames.Contains(sceneAssetName);
        }

        public string[] GetLoadedSceneAssetNames()
        {
            return m_LoadedSceneAssetNames.ToArray();
        }

        public void GetLoadedSceneAssetNames(List<string> results)
        {
            if (results == null)
            {
                throw new GameKitException("Results is invalid.");
            }

            results.Clear();
            results.AddRange(m_LoadedSceneAssetNames);
        }

        public bool SceneIsLoading(string sceneAssetName)
        {
            if (string.IsNullOrEmpty(sceneAssetName))
            {
                throw new GameKitException("Scene asset name is invalid.");
            }

            return m_LoadingSceneAssetNames.Contains(sceneAssetName);
        }

        public string[] GetLoadingSceneAssetNames()
        {
            return m_LoadingSceneAssetNames.ToArray();
        }

        public void GetLoadingSceneAssetNames(List<string> results)
        {
            if (results == null)
            {
                throw new GameKitException("Results is invalid.");
            }

            results.Clear();
            results.AddRange(m_LoadingSceneAssetNames);
        }

        public bool SceneIsUnloading(string sceneAssetName)
        {
            if (string.IsNullOrEmpty(sceneAssetName))
            {
                throw new GameKitException("Scene asset name is invalid.");
            }

            return m_UnloadingSceneAssetNames.Contains(sceneAssetName);
        }

        public string[] GetUnloadingSceneAssetNames()
        {
            return m_UnloadingSceneAssetNames.ToArray();
        }

        public void GetUnloadingSceneAssetNames(List<string> results)
        {
            if (results == null)
            {
                throw new GameKitException("Results is invalid.");
            }

            results.Clear();
            results.AddRange(m_UnloadingSceneAssetNames);
        }

        public bool HasScene(string sceneAssetName)
        {
            return m_SceneHelper.HasScene(sceneAssetName);
            // return m_ResourceManager.HasAsset(sceneAssetName) != HasAssetResult.NotExist;
        }

        public void LoadScene(string sceneAssetName)
        {
            LoadScene(sceneAssetName, 0, null);
        }

        public void LoadScene(string sceneAssetName, int priority)
        {
            LoadScene(sceneAssetName, priority, null);
        }

        public void LoadScene(string sceneAssetName, object userData)
        {
            LoadScene(sceneAssetName, 0, userData);
        }

        public void LoadScene(string sceneAssetName, int priority, object userData)
        {
            if (string.IsNullOrEmpty(sceneAssetName))
            {
                throw new GameKitException("Scene asset name is invalid.");
            }

            // if (m_ResourceManager == null)
            // {
            //     throw new GameKitException("You must set resource manager first.");
            // }

            if (SceneIsUnloading(sceneAssetName))
            {
                throw new GameKitException(Utility.Text.Format("Scene asset '{0}' is being unloaded.", sceneAssetName));
            }

            if (SceneIsLoading(sceneAssetName))
            {
                throw new GameKitException(Utility.Text.Format("Scene asset '{0}' is being loaded.", sceneAssetName));
            }

            if (SceneIsLoaded(sceneAssetName))
            {
                throw new GameKitException(Utility.Text.Format("Scene asset '{0}' is already loaded.", sceneAssetName));
            }

            m_LoadingSceneAssetNames.Add(sceneAssetName);

            // m_SceneHelper.LoadScene(sceneAssetName, priority, m_LoadSceneCallbacks, userData);

            // m_ResourceManager.LoadScene(sceneAssetName, priority, m_LoadSceneCallbacks, userData);

            AddressableManager.instance.LoadSceneAsyn(sceneAssetName, onSuccess: () =>
                {
                    LoadSceneSuccessCallback(sceneAssetName, 0, userData);
                },
                onFail: () =>
                {
                    LoadSceneFailureCallback(sceneAssetName, "Load binary data fail", userData);
                });
        }

        public void UnloadScene(string sceneAssetName)
        {
            UnloadScene(sceneAssetName, null);
        }


        public void UnloadScene(string sceneAssetName, object userData)
        {
            if (string.IsNullOrEmpty(sceneAssetName))
            {
                throw new GameKitException("Scene asset name is invalid.");
            }

            // if (m_ResourceManager == null)
            // {
            //     throw new GameKitException("You must set resource manager first.");
            // }

            if (SceneIsUnloading(sceneAssetName))
            {
                throw new GameKitException(Utility.Text.Format("Scene asset '{0}' is being unloaded.", sceneAssetName));
            }

            if (SceneIsLoading(sceneAssetName))
            {
                throw new GameKitException(Utility.Text.Format("Scene asset '{0}' is being loaded.", sceneAssetName));
            }

            if (!SceneIsLoaded(sceneAssetName))
            {
                throw new GameKitException(Utility.Text.Format("Scene asset '{0}' is not loaded yet.", sceneAssetName));
            }

            m_UnloadingSceneAssetNames.Add(sceneAssetName);
            // m_SceneHelper.UnloadScene(sceneAssetName, m_UnloadSceneCallbacks, userData);
            // m_ResourceManager.UnloadScene(sceneAssetName, m_UnloadSceneCallbacks, userData);
            // Utility.Debugger.LogWarning(userData.ToString());
            AddressableManager.instance.UnloadSceneAsyn(sceneAssetName, onSuccess: () =>
                {
                    UnloadSceneSuccessCallback(sceneAssetName, userData);
                },
                onFail: () =>
                {
                    UnloadSceneFailureCallback(sceneAssetName, userData);
                });
        }

        private void LoadSceneSuccessCallback(string sceneAssetName, float duration, object userData)
        {
            m_LoadingSceneAssetNames.Remove(sceneAssetName);
            m_LoadedSceneAssetNames.Add(sceneAssetName);
            if (m_LoadSceneSuccessEventHandler != null)
            {
                LoadSceneSuccessEventArgs loadSceneSuccessEventArgs = LoadSceneSuccessEventArgs.Create(sceneAssetName, duration, userData);
                m_LoadSceneSuccessEventHandler(this, loadSceneSuccessEventArgs);
                ReferencePool.Release(loadSceneSuccessEventArgs);
            }
        }

        private void LoadSceneFailureCallback(string sceneAssetName, string errorMessage, object userData)
        {
            m_LoadingSceneAssetNames.Remove(sceneAssetName);
            string appendErrorMessage = Utility.Text.Format("Load scene failure, scene asset name '{0}', error message '{1}'.", sceneAssetName, errorMessage);
            if (m_LoadSceneFailureEventHandler != null)
            {
                LoadSceneFailureEventArgs loadSceneFailureEventArgs = LoadSceneFailureEventArgs.Create(sceneAssetName, appendErrorMessage, userData);
                m_LoadSceneFailureEventHandler(this, loadSceneFailureEventArgs);
                ReferencePool.Release(loadSceneFailureEventArgs);
                return;
            }

            throw new GameKitException(appendErrorMessage);
        }

        private void UnloadSceneSuccessCallback(string sceneAssetName, object userData)
        {
            m_UnloadingSceneAssetNames.Remove(sceneAssetName);
            m_LoadedSceneAssetNames.Remove(sceneAssetName);
            if (m_UnloadSceneSuccessEventHandler != null)
            {
                UnloadSceneSuccessEventArgs unloadSceneSuccessEventArgs = UnloadSceneSuccessEventArgs.Create(sceneAssetName, userData);
                m_UnloadSceneSuccessEventHandler(this, unloadSceneSuccessEventArgs);
                ReferencePool.Release(unloadSceneSuccessEventArgs);
            }
        }

        private void UnloadSceneFailureCallback(string sceneAssetName, object userData)
        {
            m_UnloadingSceneAssetNames.Remove(sceneAssetName);
            if (m_UnloadSceneFailureEventHandler != null)
            {
                UnloadSceneFailureEventArgs unloadSceneFailureEventArgs = UnloadSceneFailureEventArgs.Create(sceneAssetName, userData);
                m_UnloadSceneFailureEventHandler(this, unloadSceneFailureEventArgs);
                ReferencePool.Release(unloadSceneFailureEventArgs);
                return;
            }

            throw new GameKitException(Utility.Text.Format("Unload scene failure, scene asset name '{0}'.", sceneAssetName));
        }
    }
}
