using GameKit;
using GameKit.Dialog;
using GameKit.Event;

namespace UnityGameKit.Runtime
{
    public sealed class InformDialogOptionUIEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(InformDialogOptionUIEventArgs).GetHashCode();

        public InformDialogOptionUIEventArgs()
        {
            DialogOptionSet = null;
            UserData = null;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public IDialogOptionSet DialogOptionSet
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static InformDialogOptionUIEventArgs Create(IDialogOptionSet dialogOptionSet, object UserData)
        {
            InformDialogOptionUIEventArgs informDialogOptionUIEventArgs = ReferencePool.Acquire<InformDialogOptionUIEventArgs>();
            informDialogOptionUIEventArgs.DialogOptionSet = dialogOptionSet;
            informDialogOptionUIEventArgs.UserData = UserData;
            return informDialogOptionUIEventArgs;
        }

        public override void Clear()
        {
            DialogOptionSet = null;
            UserData = null;
        }
    }
}
