using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if PACKAGE_ADDRESSABLES
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
#endif
using UnityEngine.ResourceManagement.ResourceLocations;

using GameKit;

public class Addressable_Test : MonoBehaviour
{
    public List<GameObject> objs;
    void Start()
    {
        AddressableManager.instance.GetAssetAsyn<GameObject>("Assets/Prototype/SourceControlSystem/Addressable/Prefabs/Capsule.prefab", (obj) =>
        {
            Debug.Log(obj.name);
            Instantiate(obj);
            obj.transform.position = Vector3.zero;
        });

        AddressableManager.instance.GetAssetsAsyn<GameObject>(new List<string> { "Capsule" }, eachCall: (obj) =>
        {
            if (obj is GameObject)
                Debug.Log(obj);
        }, callback: (IList<GameObject> objList) =>
        {
            objs = new List<GameObject>(objList);
        });
    }
}
