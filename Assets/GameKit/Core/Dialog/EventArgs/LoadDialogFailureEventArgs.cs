namespace GameKit.Dialog
{
    public sealed class LoadDialogFailureEventArgs : GameKitEventArgs
    {
        public LoadDialogFailureEventArgs()
        {
            DialogAssetName = null;
            ErrorMessage = null;
            UserData = null;
        }

        public string DialogAssetName
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

        public static LoadDialogFailureEventArgs Create(string sceneAssetName, string errorMessage, object userData)
        {
            LoadDialogFailureEventArgs loadDialogFailureEventArgs = ReferencePool.Acquire<LoadDialogFailureEventArgs>();
            loadDialogFailureEventArgs.DialogAssetName = sceneAssetName;
            loadDialogFailureEventArgs.ErrorMessage = errorMessage;
            loadDialogFailureEventArgs.UserData = userData;
            return loadDialogFailureEventArgs;
        }

        public override void Clear()
        {
            DialogAssetName = null;
            ErrorMessage = null;
            UserData = null;
        }
    }
}
