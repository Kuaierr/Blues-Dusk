using GameKit;
using GameKit.Event;

namespace UnityGameKit.Runtime
{
    public sealed class LoadSettingsEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(LoadSettingsEventArgs).GetHashCode();

        public LoadSettingsEventArgs()
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

        public static LoadSettingsEventArgs Create(object userData)
        {
            LoadSettingsEventArgs loadSettingsEventArgs = ReferencePool.Acquire<LoadSettingsEventArgs>();
            loadSettingsEventArgs.UserData = userData;
            return loadSettingsEventArgs;
        }

        public override void Clear()
        {
            UserData = null;
        }
    }
}
