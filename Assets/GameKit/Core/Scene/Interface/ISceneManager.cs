

// using GameKit.Resource;
using System;
using System.Collections.Generic;

namespace GameKit.Scene
{
    public interface ISceneManager
    {
        event EventHandler<LoadSceneSuccessEventArgs> LoadSceneSuccess;

        event EventHandler<LoadSceneFailureEventArgs> LoadSceneFailure;

        event EventHandler<UnloadSceneSuccessEventArgs> UnloadSceneSuccess;

        event EventHandler<UnloadSceneFailureEventArgs> UnloadSceneFailure;

        
        void SetSceneHelper(ISceneHelper helper);


        // void SetResourceManager(IResourceManager resourceManager);

        bool SceneIsLoaded(string sceneAssetName);

        string[] GetLoadedSceneAssetNames();

        void GetLoadedSceneAssetNames(List<string> results);

        bool SceneIsLoading(string sceneAssetName);

        string[] GetLoadingSceneAssetNames();

        void GetLoadingSceneAssetNames(List<string> results);

        bool SceneIsUnloading(string sceneAssetName);

        string[] GetUnloadingSceneAssetNames();

        void GetUnloadingSceneAssetNames(List<string> results);

        bool HasScene(string sceneAssetName);

        void LoadScene(string sceneAssetName);

        void LoadScene(string sceneAssetName, int priority);

        void LoadScene(string sceneAssetName, object userData);

        void LoadScene(string sceneAssetName, int priority, object userData);

        void UnloadScene(string sceneAssetName);

        void UnloadScene(string sceneAssetName, object userData);
    }
}
