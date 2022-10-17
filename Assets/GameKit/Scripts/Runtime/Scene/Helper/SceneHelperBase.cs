using GameKit.Scene;
using UnityEngine;

namespace UnityGameKit.Runtime
{

    public abstract class SceneHelperBase : MonoBehaviour, ISceneHelper
    {
        public abstract bool HasScene(string sceneAssetName);
        public abstract void LoadScene(string sceneAssetName, int priority, LoadSceneCallbacks loadSceneCallbacks, object userData = null);
        public abstract void UnloadScene(string sceneAssetName, UnloadSceneCallbacks unloadSceneCallbacks, object userData = null);
    }
}
