using GameKit.DataTable;

namespace UnityGameKit.Runtime
{
    public abstract class DataRowBase : IDataRow
    {
        public abstract int Id
        {
            get;
        }

        public virtual bool ParseDataRow(string dataRowString, object userData)
        {
            Log.Warning("Not implemented ParseDataRow(string dataRowString, object userData).");
            return false;
        }

        public virtual bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            Log.Warning("Not implemented ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData).");
            return false;
        }
    }
}
