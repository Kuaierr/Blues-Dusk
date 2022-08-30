using GameKit;
using GameKit.Dialog;
using GameKit.Event;

namespace UnityGameKit.Runtime
{
    public sealed class InformDialogUIEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(InformDialogUIEventArgs).GetHashCode();

        public InformDialogUIEventArgs()
        {
            DialogData = null;
            UserData = null;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public DialogDataNodeVariable DialogData
        {
            get;
            private set;
        }


        public object UserData
        {
            get;
            private set;
        }

        public static InformDialogUIEventArgs Create(DialogDataNodeVariable DialogData, object UserData)
        {
            InformDialogUIEventArgs informDialogUIEventArgs = ReferencePool.Acquire<InformDialogUIEventArgs>();
            informDialogUIEventArgs.DialogData = DialogData;
            informDialogUIEventArgs.UserData = UserData;
            return informDialogUIEventArgs;
        }

        public override void Clear()
        {
            DialogData = null;
            UserData = null;
        }
    }
}
