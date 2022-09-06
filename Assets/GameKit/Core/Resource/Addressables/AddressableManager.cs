using System.IO.Compression;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using SceneInstance = UnityEngine.ResourceManagement.ResourceProviders.SceneInstance;
#if PACKAGE_ADDRESSABLES
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace GameKit
{
    public class AddressableManager : SingletonBase<AddressableManager>
    {
        private Dictionary<string, AsyncOperationHandle> m_cachedHandles;
        public AddressableManager()
        {
            m_cachedHandles = new Dictionary<string, AsyncOperationHandle>();
        }

        IEnumerator GetBinaryProcess(string keyName, UnityAction<byte[]> onSuccess, UnityAction onFail)
        {
            AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(keyName);
            yield return handle;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                onSuccess?.Invoke(handle.Result.bytes);
                if (!m_cachedHandles.ContainsKey(handle.Result.GetInstanceID().ToString()))
                    m_cachedHandles.Add(handle.Result.GetInstanceID().ToString(), handle);
            }
            else
                onFail?.Invoke();
        }

        IEnumerator LoadSceneProcess(string keyName, LoadSceneMode loadMode, bool activeOnLoad, UnityAction onSuccess, UnityAction onFail)
        {
            AsyncOperationHandle handle = Addressables.LoadSceneAsync(keyName, loadMode, activeOnLoad);
            yield return handle;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                onSuccess.Invoke();
                if (!m_cachedHandles.ContainsKey(keyName))
                {
                    // Utility.Debugger.LogWarning("Add Unload Handle Key");
                    m_cachedHandles.Add(keyName, handle);
                }
            }
            else
                onFail?.Invoke();
        }

        IEnumerator UnloadSceneProcess(string keyName, bool autoReleaseHanlde, UnityAction onSuccess, UnityAction onFail)
        {
            AsyncOperationHandle handle = Addressables.UnloadSceneAsync(m_cachedHandles[keyName], autoReleaseHanlde);
            if(autoReleaseHanlde && m_cachedHandles.ContainsKey(keyName))
            {
                // Utility.Debugger.LogWarning("Remove Unload Handle Key");
                m_cachedHandles.Remove(keyName);
            }
            yield return handle;
            onSuccess.Invoke();
        }

        IEnumerator GetTextProcess(string keyName, UnityAction<string> onSuccess, UnityAction onFail)
        {
            AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(keyName);
            yield return handle;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                onSuccess?.Invoke(handle.Result.text);
                if (!m_cachedHandles.ContainsKey(handle.Result.GetInstanceID().ToString()))
                    m_cachedHandles.Add(handle.Result.GetInstanceID().ToString(), handle);
            }
            else
                onFail?.Invoke();
        }

        IEnumerator GetAsynProcess<T>(string keyName, UnityAction<T> onSuccess, UnityAction onFail) where T : Object
        {
            // Utility.Debugger.LogFail(keyName);
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(keyName);
            yield return handle;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                onSuccess?.Invoke(handle.Result as T);
                if (!m_cachedHandles.ContainsKey(handle.Result.GetInstanceID().ToString()))
                    m_cachedHandles.Add(handle.Result.GetInstanceID().ToString(), handle);
            }
            else
                onFail?.Invoke();
        }

        IEnumerator GetAsynProcessByLabel<T>(IList<string> labels, UnityAction<T> eachCall, UnityAction<IList<T>> callback) where T : Object
        {
            AsyncOperationHandle<IList<T>> handle =
                Addressables.LoadAssetsAsync<T>(labels,
                    obj =>
                    {
                        eachCall?.Invoke(obj as T);
                    }, Addressables.MergeMode.Union, false);

            yield return handle;
            callback?.Invoke(handle.Result as IList<T>);
            if (!m_cachedHandles.ContainsKey(handle.Result.First().GetInstanceID().ToString()))
                m_cachedHandles.Add(handle.Result.First().GetInstanceID().ToString(), handle);
        }

        public void GetAssetAsyn<T>(string keyName, UnityAction<T> onSuccess = null, UnityAction onFail = null) where T : Object
        {
            QuickMonoManager.instance.StartCoroutine(GetAsynProcess<T>(keyName, onSuccess, onFail));
        }

        public void LoadSceneAsyn(string keyName, LoadSceneMode loadMode = LoadSceneMode.Additive, bool activeOnLoad = true, UnityAction onSuccess = null, UnityAction onFail = null)
        {
            QuickMonoManager.instance.StartCoroutine(LoadSceneProcess(keyName, loadMode, activeOnLoad, onSuccess, onFail));
        }

        public void UnloadSceneAsyn(string keyName, bool autoReleaseHanld = true, UnityAction onSuccess = null, UnityAction onFail = null)
        {
            QuickMonoManager.instance.StartCoroutine(UnloadSceneProcess(keyName, autoReleaseHanld, onSuccess, onFail));
        }

        public void GetBinaryAsyn(string keyName, UnityAction<byte[]> onSuccess = null, UnityAction onFail = null)
        {
            QuickMonoManager.instance.StartCoroutine(GetBinaryProcess(keyName, onSuccess, onFail));
        }

        public void GetTextAsyn(string keyName, UnityAction<string> onSuccess = null, UnityAction onFail = null)
        {
            QuickMonoManager.instance.StartCoroutine(GetTextProcess(keyName, onSuccess, onFail));
        }

        public void GetAssetAsyn(string keyName, UnityAction<Object> onSuccess = null, UnityAction onFail = null)
        {
            GetAssetAsyn<Object>(keyName, onSuccess, onFail);
        }

        public void GetAssetsAsyn<T>(IList<string> labels, UnityAction<T> eachCall = null, UnityAction<IList<T>> callback = null) where T : Object
        {
            QuickMonoManager.instance.StartCoroutine(GetAsynProcessByLabel<T>(labels, eachCall, callback));
        }

        public void GetAsset<T>(string keyName, UnityAction<T> action) where T : Object
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(keyName);
            T result = handle.WaitForCompletion();
            action?.Invoke(result);
            if (!m_cachedHandles.ContainsKey(handle.Result.GetInstanceID().ToString()))
                m_cachedHandles.Add(handle.Result.GetInstanceID().ToString(), handle);
        }

        public void GetAssets<T>(IList<string> labels, UnityAction<IList<T>> action) where T : Object
        {
            AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(labels, null);
            IList<T> result = handle.WaitForCompletion();
            action?.Invoke(result);
            if (!m_cachedHandles.ContainsKey(handle.Result.First().GetInstanceID().ToString()))
                m_cachedHandles.Add(handle.Result.First().GetInstanceID().ToString(), handle);
        }

        public override void Clear()
        {
            base.Clear();
            m_cachedHandles.Clear();
        }

        public override void ShutDown()
        {
            m_cachedHandles.Clear();
            base.ShutDown();
        }

        public void ReleaseHandle(object obj)
        {
            Object monoObj = (Object)obj;
            string instanceId = monoObj.GetInstanceID().ToString();
            if (m_cachedHandles.ContainsKey(instanceId))
            {
                Utility.Debugger.LogSuccess("Release Asset Handle {0}.", monoObj.name);
                Addressables.Release(m_cachedHandles[instanceId]);
                m_cachedHandles.Remove(instanceId);
            }
            else
                Utility.Debugger.LogWarning("Try Release Uncached {0} Asset Handle.", monoObj.name);
        }
    }
}
#endif

