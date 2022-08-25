using System;
using GameKit.DataStructure;

namespace GameKit.DataTable
{
    public abstract class DataTableBase : IDataProvider<DataTableBase>
    {
        private readonly string m_Name;
        private readonly DataProvider<DataTableBase> m_DataProvider;

        public DataTableBase() : this(null, null)
        {

        }

        public DataTableBase(string name, IDataTableHelper dataTableHelper)
        {
            m_Name = name ?? string.Empty;
            m_DataProvider = new DataProvider<DataTableBase>(this);
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public string FullName
        {
            get
            {
                return new TypeNamePair(Type, m_Name).ToString();
            }
        }

        public abstract Type Type
        {
            get;
        }

        public abstract int Count
        {
            get;
        }

        public event EventHandler<ReadDataSuccessEventArgs> ReadDataSuccess
        {
            add
            {
                m_DataProvider.ReadDataSuccess += value;
            }
            remove
            {
                m_DataProvider.ReadDataSuccess -= value;
            }
        }

        public event EventHandler<ReadDataFailureEventArgs> ReadDataFailure
        {
            add
            {
                m_DataProvider.ReadDataFailure += value;
            }
            remove
            {
                m_DataProvider.ReadDataFailure -= value;
            }
        }

        public void ReadData(string dataTableAssetName)
        {
            m_DataProvider.ReadData(dataTableAssetName);
        }

        public void ReadData(string dataTableAssetName, int priority)
        {
            m_DataProvider.ReadData(dataTableAssetName, priority);
        }

        public void ReadData(string dataTableAssetName, object userData)
        {
            m_DataProvider.ReadData(dataTableAssetName, userData);
        }

        public void ReadData(string dataTableAssetName, int priority, object userData)
        {
            m_DataProvider.ReadData(dataTableAssetName, priority, userData);
        }

        public bool ParseData(string dataTableString)
        {
            return m_DataProvider.ParseData(dataTableString);
        }

        public bool ParseData(string dataTableString, object userData)
        {
            return m_DataProvider.ParseData(dataTableString, userData);
        }

        public bool ParseData(byte[] dataTableBytes)
        {
            return m_DataProvider.ParseData(dataTableBytes);
        }

        public bool ParseData(byte[] dataTableBytes, object userData)
        {
            return m_DataProvider.ParseData(dataTableBytes, userData);
        }

        public bool ParseData(byte[] dataTableBytes, int startIndex, int length)
        {
            return m_DataProvider.ParseData(dataTableBytes, startIndex, length);
        }

        public bool ParseData(byte[] dataTableBytes, int startIndex, int length, object userData)
        {
            return m_DataProvider.ParseData(dataTableBytes, startIndex, length, userData);
        }

        public abstract bool HasDataRow(int id);

        public abstract bool AddDataRow(string dataRowString, object userData);

        public abstract bool AddDataRow(byte[] dataRowBytes, int startIndex, int length, object userData);

        public abstract bool AddDataRow(object dataRowRaw, object userData);
        
        public abstract bool RemoveDataRow(int id);

        public abstract void RemoveAllDataRows();

        // internal void SetResourceManager(IResourceManager resourceManager)
        // {
        //     m_DataProvider.SetResourceManager(resourceManager);
        // }

        internal void SetDataProviderHelper(IDataProviderHelper<DataTableBase> dataProviderHelper)
        {
            m_DataProvider.SetDataProviderHelper(dataProviderHelper);
        }

        internal abstract void Shutdown();
    }
}
