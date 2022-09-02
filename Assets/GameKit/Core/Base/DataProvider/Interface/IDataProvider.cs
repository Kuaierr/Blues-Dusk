using System;

namespace GameKit
{
    public interface IDataProvider<T>
    {
        event EventHandler<ReadDataSuccessEventArgs> ReadDataSuccess;

        event EventHandler<ReadDataFailureEventArgs> ReadDataFailure;

        void ReadData(string dataAssetName);

        void ReadData(string dataAssetName, int priority);

        void ReadData(string dataAssetName, object userData);

        void ReadData(string dataAssetName, int priority, object userData);

        void ReadExternalData(string dataAssetName, int priority, object userData);

        bool ParseData(string dataString);

        bool ParseData(string dataString, object userData);

        bool ParseData(byte[] dataBytes);

        bool ParseData(byte[] dataBytes, object userData);

        bool ParseData(byte[] dataBytes, int startIndex, int length);

        bool ParseData(byte[] dataBytes, int startIndex, int length, object userData);
    }
}
