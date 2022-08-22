using GameKit.Event;
namespace GameKit
{
    public sealed class ShowUIFormFailureEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(ShowUIFormFailureEventArgs).GetHashCode();

        public ShowUIFormFailureEventArgs()
        {
            SerialId = 0;
            UIFormAssetName = null;
            UIGroupName = null;
            PauseCoveredUIForm = false;
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

        public string UIGroupName
        {
            get;
            private set;
        }

        public bool PauseCoveredUIForm
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

        public static ShowUIFormFailureEventArgs Create(GameKit.UI.OpenUIFormFailureEventArgs e)
        {
            ShowUIFormFailureEventArgs showUIFormFailureEventArgs = ReferencePool.Acquire<ShowUIFormFailureEventArgs>();
            showUIFormFailureEventArgs.SerialId = e.SerialId;
            showUIFormFailureEventArgs.UIFormAssetName = e.UIFormAssetName;
            showUIFormFailureEventArgs.UIGroupName = e.UIGroupName;
            showUIFormFailureEventArgs.PauseCoveredUIForm = e.PauseCoveredUIForm;
            showUIFormFailureEventArgs.ErrorMessage = e.ErrorMessage;
            showUIFormFailureEventArgs.UserData = e.UserData;
            return showUIFormFailureEventArgs;
        }

        public override void Clear()
        {
            SerialId = 0;
            UIFormAssetName = null;
            UIGroupName = null;
            PauseCoveredUIForm = false;
            ErrorMessage = null;
            UserData = null;
        }
    }
}
