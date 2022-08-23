using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if PACKAGE_ADDRESSABLES
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
#endif

namespace GameKit
{
    internal partial class ResourceManager : GameKitModule, IResourceManager
    {
#if PACKAGE_ADDRESSABLES
        private readonly Dictionary<int, AsyncOperationHandle> m_CachedHandles;
        private readonly Dictionary<IList<string>, Asset> m_CachedLabelsAssets;
#endif
        private readonly Dictionary<string, Asset> m_CachedAssets;
        public ResourceManager()
        {
#if PACKAGE_ADDRESSABLES
            m_CachedHandles = new Dictionary<int, AsyncOperationHandle>();
            m_CachedLabelsAssets = new Dictionary<IList<string>, Asset>();
#endif
            m_CachedAssets = new Dictionary<string, Asset>();
        }
        public T LoadResource<T>(string idName) where T : Object
        {
            if (m_CachedAssets.ContainsKey(idName))
                return m_CachedAssets[idName].Target as T;
            T res = Resources.Load<T>(idName);
            m_CachedAssets.Add(idName, Asset.Create(res.name, res));
            return res;
        }

        public void LoadResource<T>(string idName, UnityAction<T> callback) where T : Object
        {
            if (m_CachedAssets.ContainsKey(idName))
            {
                callback.Invoke(m_CachedAssets[idName].Target as T);
                return;
            }
            T res = Resources.Load<T>(idName);
            m_CachedAssets.Add(idName, Asset.Create(res.name, res));
            callback.Invoke(res as T);
        }

        public void LoadResourceAsync<T>(string idName, UnityAction<T> callback) where T : Object
        {
            if (m_CachedAssets.ContainsKey(idName))
            {
                callback.Invoke(m_CachedAssets[idName].Target as T);
                return;
            }
            // UnityGameKit.Runtime.MonoManager.instance.StartCoroutine(LoadResourceAsyncProcess<T>(idName, callback));
        }

        private IEnumerator LoadResourceAsyncProcess<T>(string idName, UnityAction<T> callback) where T : Object
        {
            ResourceRequest res = Resources.LoadAsync<T>(idName);
            yield return res;
            if (res.isDone && !m_CachedAssets.ContainsKey(idName))
                m_CachedAssets.Add(idName, Asset.Create(res.asset.name, res));
            callback.Invoke(res.asset as T);
        }

        public T[] LoadResourceAll<T>(string path) where T : Object
        {
            T[] res = Resources.LoadAll<T>(path);
            return res;
        }

#if PACKAGE_ADDRESSABLES
        IEnumerator GetAddressableAsynProcess<T>(string keyName, UnityAction<T> onSuccess, UnityAction onFail) where T : Object
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(keyName);
            yield return handle;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                onSuccess?.Invoke(handle.Result as T);
                if (!m_CachedAssets.ContainsKey(keyName))
                    m_CachedAssets.Add(keyName, Asset.Create(handle.Result.name, handle.Result));
                if (!m_CachedHandles.ContainsKey(handle.Result.GetInstanceID()))
                    m_CachedHandles.Add(handle.Result.GetInstanceID(), handle);
            }
            else if (handle.Status == AsyncOperationStatus.Failed)
                onFail?.Invoke();
        }

        IEnumerator GetAddressableAsynProcessByLabel<T>(IList<string> labels, UnityAction<T> eachCall, UnityAction<IList<T>> onSuccess, UnityAction onFail) where T : Object
        {
            AsyncOperationHandle<IList<T>> handle =
                Addressables.LoadAssetsAsync<T>(labels,
                    obj =>
                    {
                        eachCall?.Invoke(obj as T);
                    },
                Addressables.MergeMode.Union, false);

            yield return handle;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                onSuccess?.Invoke(handle.Result as IList<T>);
                if (!m_CachedLabelsAssets.ContainsKey(labels))
                    m_CachedLabelsAssets.Add(labels, Asset.Create(string.Join(",", labels), handle.Result));
                for (int i = 0; i < handle.Result.Count; i++)
                {
                    if (!m_CachedHandles.ContainsKey(handle.Result[i].GetInstanceID()))
                        m_CachedHandles.Add(handle.Result[i].GetInstanceID(), handle);
                }
            }
            else if (handle.Status == AsyncOperationStatus.Failed)
                onFail?.Invoke();
        }
#endif
        public void GetAddressableAsyn(string keyName, UnityAction<Object> onSuccess = null, UnityAction onFail = null)
        {
            GetAddressableAsyn<Object>(keyName, onSuccess, onFail);
        }

        public void GetAddressableAsyn<T>(string keyName, UnityAction<T> onSuccess = null, UnityAction onFail = null) where T : Object
        {
            if (m_CachedAssets.ContainsKey(keyName))
            {
                onSuccess?.Invoke(m_CachedAssets[keyName].Target as T);
                return;
            }
#if PACKAGE_ADDRESSABLES
            // UnityGameKit.Runtime.MonoManager.instance.StartCoroutine(GetAddressableAsynProcess<T>(keyName, onSuccess, onFail));
#else
            Utility.Debugger.LogFail("Addressables Is Not Installed.");
#endif
        }

        public void GetAddressablesAsyn<T>(IList<string> labels, UnityAction<IList<T>> onSuccess, UnityAction onFail = null, UnityAction<T> eachCall = null) where T : Object
        {
            if (m_CachedLabelsAssets.ContainsKey(labels))
            {
                onSuccess?.Invoke(m_CachedLabelsAssets[labels].Target as IList<T>);
                return;
            }
#if PACKAGE_ADDRESSABLES
            // UnityGameKit.Runtime.MonoManager.instance.StartCoroutine(GetAddressableAsynProcessByLabel<T>(labels, eachCall, onSuccess, onFail));
#else
            Utility.Debugger.LogFail("Addressables Is Not Installed.");
#endif
        }

        public void LoadAsset<T>(string keyName, UnityAction<T> action) where T : Object
        {
            if (m_CachedAssets.ContainsKey(keyName))
            {
                action?.Invoke(m_CachedAssets[keyName].Target as T);
                return;
            }
#if PACKAGE_ADDRESSABLES
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(keyName);
            T result = handle.WaitForCompletion();
            action?.Invoke(result);
            if (!m_CachedHandles.ContainsKey(handle.Result.GetInstanceID()))
                m_CachedHandles.Add(handle.Result.GetInstanceID(), handle);
#else
            Utility.Debugger.LogFail("Addressables Is Not Installed.");
#endif
        }

        public void LoadAssets<T>(IList<string> labels, UnityAction<IList<T>> action) where T : Object
        {
            if (m_CachedLabelsAssets.ContainsKey(labels))
            {
                action?.Invoke(m_CachedLabelsAssets[labels].Target as IList<T>);
                return;
            }
#if PACKAGE_ADDRESSABLES
            AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(labels, null);
            IList<T> result = handle.WaitForCompletion();
            action?.Invoke(result);
            for (int i = 0; i < handle.Result.Count; i++)
            {
                if (!m_CachedHandles.ContainsKey(handle.Result[i].GetInstanceID()))
                    m_CachedHandles.Add(handle.Result[i].GetInstanceID(), handle);
            }
#else
            Utility.Debugger.LogFail("Addressables Is Not Installed.");
#endif
        }

        public void Clear()
        {
#if PACKAGE_ADDRESSABLES
            m_CachedHandles.Clear();
#endif
        }

        internal override void Shutdown()
        {
#if PACKAGE_ADDRESSABLES
            m_CachedHandles.Clear();
#endif
        }

        public void ReleaseHandle(Object obj)
        {
            int instanceId = obj.GetInstanceID();
#if PACKAGE_ADDRESSABLES
            if (m_CachedHandles.ContainsKey(instanceId))
            {
                Addressables.Release(m_CachedHandles[instanceId]);
                m_CachedHandles.Remove(instanceId);
            }
            else
                Utility.Debugger.LogWarning("Try Release Uncached {0} Asset Handle.", obj.name);
#endif
        }

        public void Refresh()
        {
            for (int i = 0; i < m_CachedHandles.Count; i++)
            {
                if (m_CachedHandles[i].IsValid())
                    m_CachedHandles.Remove(m_CachedHandles[i].Result.GetHashCode());
            }

            // for (int i = 0; i < m_CachedAssets.Count; i++)
            // {
            //     m_CachedAssets[i].Refresh();
            // }
        }

        public void GetAsset<T>(string keyName, UnityAction<T> action) where T : Object
        {
            LoadAsset(keyName, action);
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {

        }

    }
}
