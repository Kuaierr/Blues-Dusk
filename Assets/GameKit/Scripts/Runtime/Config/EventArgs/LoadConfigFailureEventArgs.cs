using GameKit;
using GameKit.Event;

namespace UnityGameKit.Runtime
{
    public sealed class LoadConfigFailureEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(LoadConfigFailureEventArgs).GetHashCode();

        public LoadConfigFailureEventArgs()
        {
            ConfigAssetName = null;
            ErrorMessage = null;
            UserData = null;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public string ConfigAssetName
        {
            get;
            private set;
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

        public static LoadConfigFailureEventArgs Create(ReadDataFailureEventArgs e)
        {
            LoadConfigFailureEventArgs loadConfigFailureEventArgs = ReferencePool.Acquire<LoadConfigFailureEventArgs>();
            loadConfigFailureEventArgs.ConfigAssetName = e.DataAssetName;
            loadConfigFailureEventArgs.ErrorMessage = e.ErrorMessage;
            loadConfigFailureEventArgs.UserData = e.UserData;
            return loadConfigFailureEventArgs;
        }

        public override void Clear()
        {
            ConfigAssetName = null;
            ErrorMessage = null;
            UserData = null;
        }
    }
}
