using GameKit.Scene;
using UnityEngine;
using UnityEngine.Events;

namespace UnityGameKit.Runtime
{
    // TODO:  接入Resource，拓展场景转换类型特效
    public sealed class DefaultSceneHelper : SceneHelperBase
    {
        public override bool HasScene(string sceneAssetName)
        {
            return true;
        }
        public override void LoadScene(string sceneAssetName, int priority, LoadSceneCallbacks loadSceneCallbacks, object userData = null)
        {
            // Scheduler.current.LoadSceneAsyn(sceneAssetName);
        }
        public override void UnloadScene(string sceneAssetName, UnloadSceneCallbacks unloadSceneCallbacks, object userData = null)
        {
            // Scheduler.current.UnloadSceneAsyn(sceneAssetName);
        }
    }
}
