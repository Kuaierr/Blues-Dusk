using System;
using System.Collections.Generic;

namespace GameKit.Scene
{
    public interface ISceneHelper
    {
        bool HasScene(string sceneAssetName);
        void LoadScene(string sceneAssetName, int priority, LoadSceneCallbacks loadSceneCallbacks, object userData = null);
        void UnloadScene(string sceneAssetName, UnloadSceneCallbacks unloadSceneCallbacks, object userData = null);
    }
}
