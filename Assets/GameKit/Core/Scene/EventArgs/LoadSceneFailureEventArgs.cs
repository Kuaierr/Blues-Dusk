namespace GameKit.Scene
{
    public sealed class LoadSceneFailureEventArgs : GameKitEventArgs
    {
        public LoadSceneFailureEventArgs()
        {
            SceneAssetName = null;
            ErrorMessage = null;
            UserData = null;
        }

        public string SceneAssetName
        {
            get;
            private set;
        }

        public string ErrorMessage
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static LoadSceneFailureEventArgs Create(string sceneAssetName, string errorMessage, object userData)
        {
            LoadSceneFailureEventArgs loadSceneFailureEventArgs = ReferencePool.Acquire<LoadSceneFailureEventArgs>();
            loadSceneFailureEventArgs.SceneAssetName = sceneAssetName;
            loadSceneFailureEventArgs.ErrorMessage = errorMessage;
            loadSceneFailureEventArgs.UserData = userData;
            return loadSceneFailureEventArgs;
        }

        public override void Clear()
        {
            SceneAssetName = null;
            ErrorMessage = null;
            UserData = null;
        }
    }
}
