namespace GameKit.Scene
{
    public sealed class UnloadSceneFailureEventArgs : GameKitEventArgs
    {
        public UnloadSceneFailureEventArgs()
        {
            SceneAssetName = null;
            UserData = null;
        }

        public string SceneAssetName
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static UnloadSceneFailureEventArgs Create(string sceneAssetName, object userData)
        {
            UnloadSceneFailureEventArgs unloadSceneFailureEventArgs = ReferencePool.Acquire<UnloadSceneFailureEventArgs>();
            unloadSceneFailureEventArgs.SceneAssetName = sceneAssetName;
            unloadSceneFailureEventArgs.UserData = userData;
            return unloadSceneFailureEventArgs;
        }

        public override void Clear()
        {
            SceneAssetName = null;
            UserData = null;
        }
    }
}
