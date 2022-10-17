﻿namespace GameKit.Scene
{
    public sealed class LoadSceneSuccessEventArgs : GameKitEventArgs
    {
        public LoadSceneSuccessEventArgs()
        {
            SceneAssetName = null;
            Duration = 0f;
            UserData = null;
        }

        public string SceneAssetName
        {
            get;
            private set;
        }

        public float Duration
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static LoadSceneSuccessEventArgs Create(string sceneAssetName, float duration, object userData)
        {
            LoadSceneSuccessEventArgs loadSceneSuccessEventArgs = ReferencePool.Acquire<LoadSceneSuccessEventArgs>();
            loadSceneSuccessEventArgs.SceneAssetName = sceneAssetName;
            loadSceneSuccessEventArgs.Duration = duration;
            loadSceneSuccessEventArgs.UserData = userData;
            return loadSceneSuccessEventArgs;
        }

        public override void Clear()
        {
            SceneAssetName = null;
            Duration = 0f;
            UserData = null;
        }
    }
}
