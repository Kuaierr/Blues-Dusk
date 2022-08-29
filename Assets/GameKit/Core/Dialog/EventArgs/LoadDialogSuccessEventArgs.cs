namespace GameKit.Dialog
{
    public sealed class LoadDialogSuccessEventArgs : GameKitEventArgs
    {
        public LoadDialogSuccessEventArgs()
        {
            DialogAssetName = null;
            UserData = null;
        }

        public string DialogAssetName
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static LoadDialogSuccessEventArgs Create(string sceneAssetName, object userData)
        {
            LoadDialogSuccessEventArgs loadDialogSuccessEventArgs = ReferencePool.Acquire<LoadDialogSuccessEventArgs>();
            loadDialogSuccessEventArgs.DialogAssetName = sceneAssetName;
            loadDialogSuccessEventArgs.UserData = userData;
            return loadDialogSuccessEventArgs;
        }

        public override void Clear()
        {
            DialogAssetName = null;
            UserData = null;
        }
    }
}
