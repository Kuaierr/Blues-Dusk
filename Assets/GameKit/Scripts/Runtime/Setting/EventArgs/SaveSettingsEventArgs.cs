using GameKit;
using GameKit.Event;

namespace UnityGameKit.Runtime
{
    public sealed class SaveSettingsEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(SaveSettingsEventArgs).GetHashCode();

        public SaveSettingsEventArgs()
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

        public static SaveSettingsEventArgs Create(object userData)
        {
            SaveSettingsEventArgs saveSettingsEventArgs = ReferencePool.Acquire<SaveSettingsEventArgs>();
            saveSettingsEventArgs.UserData = userData;
            return saveSettingsEventArgs;
        }

        public override void Clear()
        {
            UserData = null;
        }
    }
}
