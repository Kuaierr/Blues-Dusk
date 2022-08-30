using GameKit;
using GameKit.Dialog;
using GameKit.Event;

namespace UnityGameKit.Runtime
{
    public sealed class InterruptDialogDislayEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(InterruptDialogDislayEventArgs).GetHashCode();

        public InterruptDialogDislayEventArgs()
        {
            UserData = null;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
        }


        public object UserData
        {
            get;
            private set;
        }

        public static InterruptDialogDislayEventArgs Create(object UserData)
        {
            InterruptDialogDislayEventArgs interruptDialogDislayEventArgs = ReferencePool.Acquire<InterruptDialogDislayEventArgs>();
            interruptDialogDislayEventArgs.UserData = UserData;
            return interruptDialogDislayEventArgs;
        }

        public override void Clear()
        {
            UserData = null;
        }
    }
}
