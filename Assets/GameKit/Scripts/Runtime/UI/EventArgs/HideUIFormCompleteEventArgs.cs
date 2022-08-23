using GameKit;
using GameKit.UI;
using GameKit.Event;

namespace UnityGameKit.Runtime
{
    public sealed class HideUIFormCompleteEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(HideUIFormCompleteEventArgs).GetHashCode();

        public HideUIFormCompleteEventArgs()
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

        public static HideUIFormCompleteEventArgs Create(GameKit.UI.CloseUIFormCompleteEventArgs e)
        {
            HideUIFormCompleteEventArgs hideUIFormCompleteEventArgs = ReferencePool.Acquire<HideUIFormCompleteEventArgs>();
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
