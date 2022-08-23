namespace GameKit
{
    public sealed class ReadDataSuccessEventArgs : GameKitEventArgs
    {
        public ReadDataSuccessEventArgs()
        {
            DataAssetName = null;
            Duration = 0f;
            UserData = null;
        }

        // 数据资源名
        public string DataAssetName
        {
            get;
            private set;
        }

        // 加载持续时间
        public float Duration
        {
            get;
            private set;
        }

        // 自定义数据
        public object UserData
        {
            get;
            private set;
        }

        public static ReadDataSuccessEventArgs Create(string dataAssetName, float duration, object userData)
        {
            ReadDataSuccessEventArgs loadDataSuccessEventArgs = ReferencePool.Acquire<ReadDataSuccessEventArgs>();
            loadDataSuccessEventArgs.DataAssetName = dataAssetName;
            loadDataSuccessEventArgs.Duration = duration;
            loadDataSuccessEventArgs.UserData = userData;
            return loadDataSuccessEventArgs;
        }

        public override void Clear()
        {
            DataAssetName = null;
            Duration = 0f;
            UserData = null;
        }
    }
}
