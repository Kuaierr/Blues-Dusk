namespace GameKit.Scene
{
    public sealed class UnloadSceneSuccessEventArgs : GameKitEventArgs
    {
        public UnloadSceneSuccessEventArgs()
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

        public static UnloadSceneSuccessEventArgs Create(string sceneAssetName, object userData)
        {
            UnloadSceneSuccessEventArgs unloadSceneSuccessEventArgs = ReferencePool.Acquire<UnloadSceneSuccessEventArgs>();
            unloadSceneSuccessEventArgs.SceneAssetName = sceneAssetName;
            unloadSceneSuccessEventArgs.UserData = userData;
            return unloadSceneSuccessEventArgs;
        }

        public override void Clear()
        {
            SceneAssetName = null;
            UserData = null;
        }
    }
}
