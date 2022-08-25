using System;
using System.Collections.Generic;


namespace GameKit.DataTable
{
    public interface IDataTableManager
    {
        int Count { get; }

        int CachedBytesSize { get; }

        // void SetResourceManager(IResourceManager resourceManager);

        void SetDataProviderHelper(IDataProviderHelper<DataTableBase> dataProviderHelper);

        void SetDataTableHelper(IDataTableHelper dataTableHelper);

        void EnsureCachedBytesSize(int ensureSize);

        void FreeCachedBytes();

        bool HasDataTable<T>();

        bool HasDataTable(Type dataRowType);

        bool HasDataTable<T>(string name);

        bool HasDataTable(Type dataRowType, string name);

        IDataTable<T> GetDataTable<T>();

        DataTableBase GetDataTable(Type dataRowType);

        IDataTable<T> GetDataTable<T>(string name);

        DataTableBase GetDataTable(Type dataRowType, string name);

        DataTableBase[] GetAllDataTables();

        void GetAllDataTables(List<DataTableBase> results);

        IDataTable<T> CreateDataTable<T>() where T : class, IDataRow, new();

        DataTableBase CreateDataTable(Type dataRowType);

        IDataTable<T> CreateDataTable<T>(string name) where T : class, IDataRow, new();

        DataTableBase CreateDataTable(Type dataRowType, string name);

        bool DestroyDataTable<T>();

        bool DestroyDataTable(Type dataRowType);

        bool DestroyDataTable<T>(string name);

        bool DestroyDataTable(Type dataRowType, string name);

        bool DestroyDataTable<T>(IDataTable<T> dataTable);

        bool DestroyDataTable(DataTableBase dataTable);
    }
}
