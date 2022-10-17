namespace GameKit.Dialog
{
    public sealed class StartDialogFailureEventArgs : GameKitEventArgs
    {
        public StartDialogFailureEventArgs()
        {
            DialogAssetName = string.Empty;
            ErrorMessage = string.Empty;
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

        public static StartDialogFailureEventArgs Create(string dialogAssetName, string errorMessage, object userData)
        {
            StartDialogFailureEventArgs startDialogFailureEventArgs = ReferencePool.Acquire<StartDialogFailureEventArgs>();
            startDialogFailureEventArgs.DialogAssetName = dialogAssetName;
            startDialogFailureEventArgs.ErrorMessage = errorMessage;
            startDialogFailureEventArgs.UserData = userData;
            return startDialogFailureEventArgs;
        }

        public override void Clear()
        {
            DialogAssetName = string.Empty;
            ErrorMessage = string.Empty;
            UserData = null;
        }
    }
}
