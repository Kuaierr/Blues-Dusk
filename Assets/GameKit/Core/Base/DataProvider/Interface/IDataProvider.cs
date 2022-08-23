using System;

namespace GameKit
{
    public interface IDataProvider<T>
    {
        event EventHandler<ReadDataSuccessEventArgs> ReadDataSuccess;

        event EventHandler<ReadDataFailureEventArgs> ReadDataFailure;

        void ReadData(string dataAssetName, object userData = null);

        void ReadData(string dataAssetName, int priority, object userData = null);

        bool ParseData(string dataString);

        bool ParseData(byte[] dataBytes, object userData = null);

        bool ParseData(byte[] dataBytes);

        bool ParseData(byte[] dataBytes, int startIndex, int length, object userData = null);
    }
}
