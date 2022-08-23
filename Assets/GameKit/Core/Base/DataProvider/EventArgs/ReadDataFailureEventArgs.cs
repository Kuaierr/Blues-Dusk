namespace GameKit
{
    public sealed class ReadDataFailureEventArgs : GameKitEventArgs
    {
        public ReadDataFailureEventArgs()
        {
            DataAssetName = null;
            ErrorMessage = null;
            UserData = null;
        }

        public string DataAssetName
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

        public static ReadDataFailureEventArgs Create(string dataAssetName, string errorMessage, object userData)
        {
            ReadDataFailureEventArgs loadDataFailureEventArgs = ReferencePool.Acquire<ReadDataFailureEventArgs>();
            loadDataFailureEventArgs.DataAssetName = dataAssetName;
            loadDataFailureEventArgs.ErrorMessage = errorMessage;
            loadDataFailureEventArgs.UserData = userData;
            return loadDataFailureEventArgs;
        }

        public override void Clear()
        {
            DataAssetName = null;
            ErrorMessage = null;
            UserData = null;
        }
    }
}
