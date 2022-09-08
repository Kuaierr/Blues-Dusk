using GameKit;
using GameKit.Event;

namespace UnityGameKit.Runtime
{
    public sealed class StartDialogEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(StartDialogEventArgs).GetHashCode();

        public StartDialogEventArgs()
        {
            DialogName = string.Empty;
            DialogContent = string.Empty;
            UserData = null;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public bool HasContent
        {
            get
            {
                return DialogContent != string.Empty;
            }
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

        public static StartDialogEventArgs Create(GameKit.Dialog.StartDialogEventArgs e)
        {
            StartDialogEventArgs startDialogEventArgs = ReferencePool.Acquire<StartDialogEventArgs>();
            startDialogEventArgs.DialogName = e.DialogName;
            startDialogEventArgs.DialogContent = e.DialogContent;
            startDialogEventArgs.UserData = e.UserData;
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
