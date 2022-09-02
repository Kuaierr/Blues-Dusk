using GameKit;
using GameKit.DataTable;
using UnityEngine;

namespace UnityGameKit.Runtime
{
    public abstract class DataTableHelperBase : MonoBehaviour, IDataProviderHelper<DataTableBase>, IDataTableHelper
    {
        public abstract bool ReadData(DataTableBase dataTable, string dataTableAssetName, object dataTableAsset, object userData);

        public abstract bool ReadData(DataTableBase dataTable, string dataTableAssetName, byte[] dataTableBytes, int startIndex, int length, object userData);

        public abstract bool ReadExternalData(DataTableBase dataTable, string rawData, object userData);

        public abstract bool ParseData(DataTableBase dataTable, string dataTableString, object userData);

        public abstract bool ParseData(DataTableBase dataTable, byte[] dataTableBytes, int startIndex, int length, object userData);

        public abstract void ReleaseDataAsset(DataTableBase dataTable, object dataTableAsset);

        public abstract bool ParseInternalData(object internalRawData, string dataRowString, object userData);

        public abstract bool ParseInternalData(object internalRawData, byte[] dataRowBytes, int startIndex, int length, object userData);

        public abstract bool ParseInternalData(object internalRawData, object dataRaw, object userData);

        public abstract int ParseExternalDataId(object dataRaw);
    }
}
