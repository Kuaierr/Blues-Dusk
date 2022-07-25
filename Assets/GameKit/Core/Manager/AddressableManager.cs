using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if PACKAGE_ADDRESSABLES
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
#endif
using UnityEngine.ResourceManagement.ResourceLocations;

/// <summary>
/// 使用案例
/* 
    AddressableManager.instance.GetAssetAsyn<GameObject>("Capsule.prefab", (obj) =>
    {
        Debug.Log(obj.name);
    });

    AddressableManager.instance.GetAssetsAsyn<GameObject>(new List<string> { "Capsule" }, eachCall: (obj) =>
    {
        if (obj is GameObject)
            Debug.Log(obj);
    }, callback: (IList<GameObject> objList) =>
    {
        objs = new List<GameObject>(objList);
    });
*/
/// </summary>

namespace GameKit
{
    public class AddressableManager : SingletonBase<AddressableManager>
    {
#if PACKAGE_ADDRESSABLES
        private List<AssetReference> assetReferences;
        private Dictionary<AssetReference, List<GameObject>> gameobjs;

        IEnumerator GetAsynProcess<T>(string keyName, UnityAction<T> callback) where T : Object
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(keyName);
            yield return handle;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                if (handle.Result is GameObject)
                    callback?.Invoke(GameObject.Instantiate(handle.Result as GameObject) as T);
                else
                    callback?.Invoke(handle.Result as T);
            }
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
        }
#endif

        public void GetAssetAsyn<T>(string keyName, UnityAction<T> callback = null) where T : Object
        {
#if PACKAGE_ADDRESSABLES
            MonoManager.instance.StartCoroutine(GetAsynProcess<T>(keyName, callback));
#else
            Utility.Debug.LogFail("Addressables Is Not Installed.");
#endif
        }

        public void GetAssetsAsyn<T>(IList<string> labels, UnityAction<T> eachCall = null, UnityAction<IList<T>> callback = null) where T : Object
        {
#if PACKAGE_ADDRESSABLES
            MonoManager.instance.StartCoroutine(GetAsynProcessByLabel<T>(labels, eachCall, callback));
#else
            Utility.Debug.LogFail("Addressables Is Not Installed.");
#endif
        }
    }

}