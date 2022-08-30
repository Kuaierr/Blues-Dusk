using GameKit;
using GameKit.Dialog;
using GameKit.Event;

namespace UnityGameKit.Runtime
{
    public sealed class StartDialogSuccessEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(StartDialogSuccessEventArgs).GetHashCode();

        public StartDialogSuccessEventArgs()
        {
            DialogTree = null;
            UserData = null;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
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

        public static StartDialogSuccessEventArgs Create(GameKit.Dialog.StartDialogSuccessEventArgs e)
        {
            StartDialogSuccessEventArgs startDialogSuccessEventArgs = ReferencePool.Acquire<StartDialogSuccessEventArgs>();
            startDialogSuccessEventArgs.DialogTree = (IDialogTree)e.DialogTree;
            startDialogSuccessEventArgs.UserData = e.UserData;
            return startDialogSuccessEventArgs;
        }

        public override void Clear()
        {
            DialogTree = null;
            UserData = null;
        }
    }
}
