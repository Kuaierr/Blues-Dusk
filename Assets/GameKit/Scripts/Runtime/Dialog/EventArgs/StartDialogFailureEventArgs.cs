using GameKit;
using GameKit.Dialog;
using GameKit.Event;

namespace UnityGameKit.Runtime
{
    public sealed class StartDialogFailureEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(StartDialogFailureEventArgs).GetHashCode();

        public StartDialogFailureEventArgs()
        {
            ErrorMessage = string.Empty;
            UserData = null;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
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

        public static StartDialogFailureEventArgs Create(GameKit.Dialog.StartDialogFailureEventArgs e)
        {
            StartDialogFailureEventArgs startDialogFailureEventArgs = ReferencePool.Acquire<StartDialogFailureEventArgs>();
            startDialogFailureEventArgs.ErrorMessage = e.ErrorMessage;
            startDialogFailureEventArgs.UserData = e.UserData;
            return startDialogFailureEventArgs;
        }

        public override void Clear()
        {
            ErrorMessage = string.Empty;
            UserData = null;
        }
    }
}
