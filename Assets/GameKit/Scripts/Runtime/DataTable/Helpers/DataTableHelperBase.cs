using GameKit;
using GameKit.DataTable;
using UnityEngine;

namespace UnityGameKit.Runtime
{
    public abstract class DataTableHelperBase : MonoBehaviour, IDataProviderHelper<DataTableBase>, IDataTableHelper
    {
        public abstract bool ReadData(DataTableBase dataTable, string dataTableAssetName, object dataTableAsset, object userData);

        public abstract bool ReadData(DataTableBase dataTable, string dataTableAssetName, byte[] dataTableBytes, int startIndex, int length, object userData);

        public abstract bool ParseData(DataTableBase dataTable, string dataTableString, object userData);

        public abstract bool ParseData(DataTableBase dataTable, byte[] dataTableBytes, int startIndex, int length, object userData);

        public abstract void ReleaseDataAsset(DataTableBase dataTable, object dataTableAsset);
    }
}
