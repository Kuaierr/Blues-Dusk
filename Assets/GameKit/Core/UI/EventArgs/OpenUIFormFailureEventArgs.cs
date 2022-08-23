
namespace GameKit.UI
{
    public sealed class OpenUIFormFailureEventArgs : GameKitEventArgs
    {
        public OpenUIFormFailureEventArgs()
        {
            SerialId = 0;
            UIFormAssetName = null;
            UIGroupName = null;
            PauseCoveredUIForm = false;
            ErrorMessage = null;
            UserData = null;
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

        public static OpenUIFormFailureEventArgs Create(int serialId, string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm, string errorMessage, object userData)
        {
            OpenUIFormFailureEventArgs openUIFormFailureEventArgs = ReferencePool.Acquire<OpenUIFormFailureEventArgs>();
            openUIFormFailureEventArgs.SerialId = serialId;
            openUIFormFailureEventArgs.UIFormAssetName = uiFormAssetName;
            openUIFormFailureEventArgs.UIGroupName = uiGroupName;
            openUIFormFailureEventArgs.PauseCoveredUIForm = pauseCoveredUIForm;
            openUIFormFailureEventArgs.ErrorMessage = errorMessage;
            openUIFormFailureEventArgs.UserData = userData;
            return openUIFormFailureEventArgs;
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
