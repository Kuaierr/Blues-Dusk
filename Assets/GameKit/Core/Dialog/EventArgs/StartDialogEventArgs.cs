namespace GameKit.Dialog
{
    public sealed class StartDialogEventArgs : GameKitEventArgs
    {
        public StartDialogEventArgs()
        {
            DialogName = string.Empty;
            DialogContent = string.Empty;
            UserData = null;
        }

        public string DialogName
        {
            get;
            private set;
        }

        public string DialogContent
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static StartDialogEventArgs Create(string dialogName, string dialogContent, object userData)
        {
            StartDialogEventArgs startDialogEventArgs = ReferencePool.Acquire<StartDialogEventArgs>();
            startDialogEventArgs.DialogName = dialogName;
            startDialogEventArgs.DialogContent = dialogContent;
            startDialogEventArgs.UserData = userData;
            return startDialogEventArgs;
        }

        public override void Clear()
        {
            DialogName = string.Empty;
            DialogContent = string.Empty;
            UserData = null;
        }
    }
}
