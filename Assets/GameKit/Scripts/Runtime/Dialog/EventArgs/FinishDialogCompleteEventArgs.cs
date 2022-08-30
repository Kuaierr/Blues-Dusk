using GameKit;
using GameKit.Dialog;
using GameKit.Event;

namespace UnityGameKit.Runtime
{
    public sealed class FinishDialogCompleteEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(FinishDialogCompleteEventArgs).GetHashCode();

        public FinishDialogCompleteEventArgs()
        {
            AssetName = string.Empty;
            UserData = null;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public string AssetName
        {
            get;
            private set;
        }


        public object UserData
        {
            get;
            private set;
        }

        public static FinishDialogCompleteEventArgs Create(GameKit.Dialog.FinishDialogCompleteEventArgs e)
        {
            FinishDialogCompleteEventArgs startDialogFailureEventArgs = ReferencePool.Acquire<FinishDialogCompleteEventArgs>();
            startDialogFailureEventArgs.AssetName = e.DialogAssetName;
            startDialogFailureEventArgs.UserData = e.UserData;
            return startDialogFailureEventArgs;
        }

        public override void Clear()
        {
            AssetName = string.Empty;
            UserData = null;
        }
    }
}
