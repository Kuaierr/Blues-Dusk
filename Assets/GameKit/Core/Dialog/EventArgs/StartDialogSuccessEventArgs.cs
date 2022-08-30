namespace GameKit.Dialog
{
    public sealed class StartDialogSuccessEventArgs : GameKitEventArgs
    {
        public StartDialogSuccessEventArgs()
        {
            DialogTree = null;
            UserData = null;
        }

        public IDialogTree DialogTree
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static StartDialogSuccessEventArgs Create(IDialogTree dialogTree, object userData)
        {
            StartDialogSuccessEventArgs startDialogSuccessEventArgs = ReferencePool.Acquire<StartDialogSuccessEventArgs>();
            startDialogSuccessEventArgs.DialogTree = dialogTree;
            startDialogSuccessEventArgs.UserData = userData;
            return startDialogSuccessEventArgs;
        }

        public override void Clear()
        {
            DialogTree = null;
            UserData = null;
        }
    }
}
