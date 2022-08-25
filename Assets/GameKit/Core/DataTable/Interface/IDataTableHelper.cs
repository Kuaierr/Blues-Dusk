namespace GameKit.DataTable
{

    public interface IDataTableHelper
    {
        bool ParseInternalData(object internalRawData, string dataRowString, object userData);
        bool ParseInternalData(object internalRawData, byte[] dataRowBytes, int startIndex, int length, object userData);
        bool ParseInternalData(object internalRawData, object dataRaw, object userData);
        int ParseExternalDataId(object dataRaw);
    }
}
