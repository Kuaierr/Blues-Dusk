namespace GameKit
{
    public interface IDataProviderHelper<T>
    {
        bool ReadData(T dataProviderOwner, string dataAssetName, object dataAsset, object userData);

        bool ReadData(T dataProviderOwner, string dataAssetName, byte[] dataBytes, int startIndex, int length, object userData);

        bool ReadExternalData(T dataProviderOwner, string dataAssetName, object userData);

        bool ParseData(T dataProviderOwner, string dataString, object userData);

        bool ParseData(T dataProviderOwner, byte[] dataBytes, int startIndex, int length, object userData);

        void ReleaseDataAsset(T dataProviderOwner, object dataAsset);
    }
}
