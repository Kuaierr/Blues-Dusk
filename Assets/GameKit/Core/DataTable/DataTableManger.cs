
using GameKit.DataStructure;
using System;
using System.Collections.Generic;

namespace GameKit.DataTable
{
    internal sealed partial class DataTableManager : GameKitModule, IDataTableManager
    {
        private readonly Dictionary<TypeNamePair, DataTableBase> m_DataTables;
        // private IResourceManager m_ResourceManager;
        private IDataProviderHelper<DataTableBase> m_DataProviderHelper;
        private IDataTableHelper m_DataTableHelper;

        public DataTableManager()
        {
            m_DataTables = new Dictionary<TypeNamePair, DataTableBase>();
            // m_ResourceManager = null;
            m_DataProviderHelper = null;
            m_DataTableHelper = null;
        }

        public int Count
        {
            get
            {
                return m_DataTables.Count;
            }
        }

        public int CachedBytesSize
        {
            get
            {
                return DataProvider<DataTableBase>.CachedBytesSize;
            }
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        internal override void Shutdown()
        {
            foreach (KeyValuePair<TypeNamePair, DataTableBase> dataTable in m_DataTables)
            {
                dataTable.Value.Shutdown();
            }

            m_DataTables.Clear();
        }

        // public void SetResourceManager(IResourceManager resourceManager)
        // {
        //     if (resourceManager == null)
        //     {
        //         throw new GameKitException("Resource manager is invalid.");
        //     }

        //     m_ResourceManager = resourceManager;
        // }

        public void SetDataProviderHelper(IDataProviderHelper<DataTableBase> dataProviderHelper)
        {
            if (dataProviderHelper == null)
            {
                throw new GameKitException("Data provider helper is invalid.");
            }

            m_DataProviderHelper = dataProviderHelper;
        }

        public void SetDataTableHelper(IDataTableHelper dataTableHelper)
        {
            if (dataTableHelper == null)
            {
                throw new GameKitException("Data table helper is invalid.");
            }

            m_DataTableHelper = dataTableHelper;
        }

        public void EnsureCachedBytesSize(int ensureSize)
        {
            DataProvider<DataTableBase>.EnsureCachedBytesSize(ensureSize);
        }

        public void FreeCachedBytes()
        {
            DataProvider<DataTableBase>.FreeCachedBytes();
        }

        public bool HasDataTable<T>() 
        {
            return InternalHasDataTable(new TypeNamePair(typeof(T)));
        }

        public bool HasDataTable(Type dataRowType)
        {
            if (dataRowType == null)
            {
                throw new GameKitException("Data row type is invalid.");
            }

            if (!typeof(IDataRow).IsAssignableFrom(dataRowType))
            {
                throw new GameKitException(Utility.Text.Format("Data row type '{0}' is invalid.", dataRowType.FullName));
            }

            return InternalHasDataTable(new TypeNamePair(dataRowType));
        }

        public bool HasDataTable<T>(string name) 
        {
            return InternalHasDataTable(new TypeNamePair(typeof(T), name));
        }

        public bool HasDataTable(Type dataRowType, string name)
        {
            if (dataRowType == null)
            {
                throw new GameKitException("Data row type is invalid.");
            }

            if (!typeof(IDataRow).IsAssignableFrom(dataRowType))
            {
                throw new GameKitException(Utility.Text.Format("Data row type '{0}' is invalid.", dataRowType.FullName));
            }

            return InternalHasDataTable(new TypeNamePair(dataRowType, name));
        }

        public IDataTable<T> GetDataTable<T>() 
        {
            return (IDataTable<T>)InternalGetDataTable(new TypeNamePair(typeof(T)));
        }

        public DataTableBase GetDataTable(Type dataRowType)
        {
            if (dataRowType == null)
            {
                throw new GameKitException("Data row type is invalid.");
            }

            if (!typeof(IDataRow).IsAssignableFrom(dataRowType))
            {
                throw new GameKitException(Utility.Text.Format("Data row type '{0}' is invalid.", dataRowType.FullName));
            }

            return InternalGetDataTable(new TypeNamePair(dataRowType));
        }

        public IDataTable<T> GetDataTable<T>(string name) 
        {
            return (IDataTable<T>)InternalGetDataTable(new TypeNamePair(typeof(T), name));
        }

        public DataTableBase GetDataTable(Type dataRowType, string name)
        {
            if (dataRowType == null)
            {
                throw new GameKitException("Data row type is invalid.");
            }

            if (!typeof(IDataRow).IsAssignableFrom(dataRowType))
            {
                throw new GameKitException(Utility.Text.Format("Data row type '{0}' is invalid.", dataRowType.FullName));
            }

            return InternalGetDataTable(new TypeNamePair(dataRowType, name));
        }

        public DataTableBase[] GetAllDataTables()
        {
            int index = 0;
            DataTableBase[] results = new DataTableBase[m_DataTables.Count];
            foreach (KeyValuePair<TypeNamePair, DataTableBase> dataTable in m_DataTables)
            {
                results[index++] = dataTable.Value;
            }

            return results;
        }

        public void GetAllDataTables(List<DataTableBase> results)
        {
            if (results == null)
            {
                throw new GameKitException("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<TypeNamePair, DataTableBase> dataTable in m_DataTables)
            {
                results.Add(dataTable.Value);
            }
        }

        public IDataTable<T> CreateDataTable<T>() where T : class, new()
        {
            return CreateDataTable<T>(string.Empty);
        }

        public DataTableBase CreateDataTable(Type dataRowType)
        {
            return CreateDataTable(dataRowType, string.Empty);
        }

        public IDataTable<T> CreateDataTable<T>(string name) where T : class, new()
        {
            // if (m_ResourceManager == null)
            // {
            //     throw new GameKitException("You must set resource manager first.");
            // }

            if (m_DataProviderHelper == null)
            {
                throw new GameKitException("You must set data provider helper first.");
            }

            TypeNamePair typeNamePair = new TypeNamePair(typeof(T), name);
            if (HasDataTable<T>(name))
            {
                throw new GameKitException(Utility.Text.Format("Already exist data table '{0}'.", typeNamePair));
            }

            DataTable<T> dataTable = new DataTable<T>(name, m_DataTableHelper);
            // dataTable.SetResourceManager(m_ResourceManager);
            dataTable.SetDataProviderHelper(m_DataProviderHelper);
            m_DataTables.Add(typeNamePair, dataTable);
            return dataTable;
        }

        public DataTableBase CreateDataTable(Type dataRowType, string name)
        {
            // if (m_ResourceManager == null)
            // {
            //     throw new GameKitException("You must set resource manager first.");
            // }

            if (m_DataProviderHelper == null)
            {
                throw new GameKitException("You must set data provider helper first.");
            }

            if (dataRowType == null)
            {
                throw new GameKitException("Data row type is invalid.");
            }

            if (!typeof(IDataRow).IsAssignableFrom(dataRowType))
            {
                throw new GameKitException(Utility.Text.Format("Data row type '{0}' is invalid.", dataRowType.FullName));
            }

            TypeNamePair typeNamePair = new TypeNamePair(dataRowType, name);
            if (HasDataTable(dataRowType, name))
            {
                throw new GameKitException(Utility.Text.Format("Already exist data table '{0}'.", typeNamePair));
            }

            Type dataTableType = typeof(DataTable<>).MakeGenericType(dataRowType);
            DataTableBase dataTable = (DataTableBase)Activator.CreateInstance(dataTableType, name);
            // dataTable.SetResourceManager(m_ResourceManager);
            dataTable.SetDataProviderHelper(m_DataProviderHelper);
            m_DataTables.Add(typeNamePair, dataTable);
            return dataTable;
        }

        public bool DestroyDataTable<T>() 
        {
            return InternalDestroyDataTable(new TypeNamePair(typeof(T)));
        }

        public bool DestroyDataTable(Type dataRowType)
        {
            if (dataRowType == null)
            {
                throw new GameKitException("Data row type is invalid.");
            }

            if (!typeof(IDataRow).IsAssignableFrom(dataRowType))
            {
                throw new GameKitException(Utility.Text.Format("Data row type '{0}' is invalid.", dataRowType.FullName));
            }

            return InternalDestroyDataTable(new TypeNamePair(dataRowType));
        }

        public bool DestroyDataTable<T>(string name) 
        {
            return InternalDestroyDataTable(new TypeNamePair(typeof(T), name));
        }

        public bool DestroyDataTable(Type dataRowType, string name)
        {
            if (dataRowType == null)
            {
                throw new GameKitException("Data row type is invalid.");
            }

            if (!typeof(IDataRow).IsAssignableFrom(dataRowType))
            {
                throw new GameKitException(Utility.Text.Format("Data row type '{0}' is invalid.", dataRowType.FullName));
            }

            return InternalDestroyDataTable(new TypeNamePair(dataRowType, name));
        }

        public bool DestroyDataTable<T>(IDataTable<T> dataTable) 
        {
            if (dataTable == null)
            {
                throw new GameKitException("Data table is invalid.");
            }

            return InternalDestroyDataTable(new TypeNamePair(typeof(T), dataTable.Name));
        }

        public bool DestroyDataTable(DataTableBase dataTable)
        {
            if (dataTable == null)
            {
                throw new GameKitException("Data table is invalid.");
            }

            return InternalDestroyDataTable(new TypeNamePair(dataTable.Type, dataTable.Name));
        }

        private bool InternalHasDataTable(TypeNamePair typeNamePair)
        {
            return m_DataTables.ContainsKey(typeNamePair);
        }

        private DataTableBase InternalGetDataTable(TypeNamePair typeNamePair)
        {
            DataTableBase dataTable = null;
            if (m_DataTables.TryGetValue(typeNamePair, out dataTable))
            {
                return dataTable;
            }

            return null;
        }

        private bool InternalDestroyDataTable(TypeNamePair typeNamePair)
        {
            DataTableBase dataTable = null;
            if (m_DataTables.TryGetValue(typeNamePair, out dataTable))
            {
                dataTable.Shutdown();
                return m_DataTables.Remove(typeNamePair);
            }

            return false;
        }
    }
}
