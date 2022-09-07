using GameKit;
using GameKit.UI;
using GameKit.Event;

namespace UnityGameKit.Runtime
{
    public sealed class CloseUIFormCompleteEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(CloseUIFormCompleteEventArgs).GetHashCode();

        public CloseUIFormCompleteEventArgs()
        {
            SerialId = 0;
            UIFormAssetName = null;
            UIGroup = null;
            UserData = null;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public int SerialId
        {
            get;
            private set;
        }

        public string UIFormAssetName
        {
            get;
            private set;
        }

        public IUIGroup UIGroup
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static CloseUIFormCompleteEventArgs Create(GameKit.UI.CloseUIFormCompleteEventArgs e)
        {
            CloseUIFormCompleteEventArgs hideUIFormCompleteEventArgs = ReferencePool.Acquire<CloseUIFormCompleteEventArgs>();
            hideUIFormCompleteEventArgs.SerialId = e.SerialId;
            hideUIFormCompleteEventArgs.UIFormAssetName = e.UIFormAssetName;
            hideUIFormCompleteEventArgs.UIGroup = e.UIGroup;
            hideUIFormCompleteEventArgs.UserData = e.UserData;
            return hideUIFormCompleteEventArgs;
        }

        public override void Clear()
        {
            SerialId = 0;
            UIFormAssetName = null;
            UIGroup = null;
            UserData = null;
        }
    }
}
