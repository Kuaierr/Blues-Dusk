using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if PACKAGE_ADDRESSABLES
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameKit
{
    public class AddressableManager : SingletonBase<AddressableManager>
    {
        private Dictionary<int, AsyncOperationHandle> m_cachedHandles;
        public AddressableManager()
        {
            m_cachedHandles = new Dictionary<int, AsyncOperationHandle>();
        }

        IEnumerator GetBinaryProcess(string keyName, UnityAction<byte[]> onSuccess, UnityAction onFail)
        {
            AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(keyName);
            yield return handle;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                onSuccess?.Invoke(handle.Result.bytes);
                if (!m_cachedHandles.ContainsKey(handle.Result.GetInstanceID()))
                    m_cachedHandles.Add(handle.Result.GetInstanceID(), handle);
            }
            else
                onFail?.Invoke();
        }
        IEnumerator GetAsynProcess<T>(string keyName, UnityAction<T> onSuccess, UnityAction onFail) where T : Object
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(keyName);
            yield return handle;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                if (handle.Result is GameObject)
                    onSuccess?.Invoke(GameObject.Instantiate(handle.Result as GameObject) as T);
                else
                    onSuccess?.Invoke(handle.Result as T);

                if (!m_cachedHandles.ContainsKey(handle.Result.GetInstanceID()))
                    m_cachedHandles.Add(handle.Result.GetInstanceID(), handle);
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
            if (!m_cachedHandles.ContainsKey(handle.Result.First().GetInstanceID()))
                m_cachedHandles.Add(handle.Result.First().GetInstanceID(), handle);
        }

        public void GetAssetAsyn<T>(string keyName, UnityAction<T> onSuccess = null, UnityAction onFail = null) where T : Object
        {
            QuickMonoManager.instance.StartCoroutine(GetAsynProcess<T>(keyName, onSuccess, onFail));  
        }

        public void GetBinaryAsyn(string keyName, UnityAction<byte[]> onSuccess = null, UnityAction onFail = null)
        {
            QuickMonoManager.instance.StartCoroutine(GetBinaryProcess(keyName, onSuccess, onFail));
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
            if (!m_cachedHandles.ContainsKey(handle.Result.GetInstanceID()))
                m_cachedHandles.Add(handle.Result.GetInstanceID(), handle);
            
        }

        public void GetAssets<T>(IList<string> labels, UnityAction<IList<T>> action) where T : Object
        {
            AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(labels, null);
            IList<T> result = handle.WaitForCompletion();
            action?.Invoke(result);
            if (!m_cachedHandles.ContainsKey(handle.Result.First().GetInstanceID()))
                m_cachedHandles.Add(handle.Result.First().GetInstanceID(), handle);
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

        public void ReleaseHandle(Object obj)
        {
            int instanceId = obj.GetInstanceID();
            if (m_cachedHandles.ContainsKey(instanceId))
            {
                Addressables.Release(m_cachedHandles[instanceId]);
                m_cachedHandles.Remove(instanceId);
            }
            else
                Utility.Debugger.LogWarning("Try Release Uncached {0} Asset Handle.", obj.name);
        }
    }
}
#endif

