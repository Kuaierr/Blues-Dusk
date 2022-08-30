namespace GameKit.Dialog
{
    public sealed class FinishDialogCompleteEventArgs : GameKitEventArgs
    {
        public FinishDialogCompleteEventArgs()
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

        public static FinishDialogCompleteEventArgs Create(string sceneAssetName, object userData)
        {
            FinishDialogCompleteEventArgs loadDialogSuccessEventArgs = ReferencePool.Acquire<FinishDialogCompleteEventArgs>();
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
