using System;
using System.Collections.Generic;


namespace GameKit.ExternalDataTable
{
    public interface IExternalDataTableManager
    {
        int Count { get; }

        int CachedBytesSize { get; }

        // void SetResourceManager(IResourceManager resourceManager);

        void SetDataProviderHelper(IDataProviderHelper<ExternalDataTableBase> dataProviderHelper);

        void SetExternalDataTableHelper(IDataTableHelper dataTableHelper);

        bool HasDataTable<T>() where T : IExternalData;

        bool HasDataTable(Type dataRowType);

        bool HasDataTable<T>(string name) where T : IExternalData;

        bool HasDataTable(Type dataRowType, string name);

        IDataTable<T> GetDataTable<T>() where T : IExternalData;

        ExternalDataTableBase GetDataTable(Type dataRowType);

        IDataTable<T> GetDataTable<T>(string name) where T : IExternalData;

        ExternalDataTableBase GetDataTable(Type dataRowType, string name);

        ExternalDataTableBase[] GetAllDataTables();

        void GetAllDataTables(List<ExternalDataTableBase> results);

        IDataTable<T> CreateDataTable<T>() where T : class, IExternalData, new();

        ExternalDataTableBase CreateDataTable(Type dataRowType);

        IDataTable<T> CreateDataTable<T>(string name) where T : class, IExternalData, new();

        ExternalDataTableBase CreateDataTable(Type dataRowType, string name);

        bool DestroyDataTable<T>() where T : IExternalData;

        bool DestroyDataTable(Type dataRowType);

        bool DestroyDataTable<T>(string name) where T : IExternalData;

        bool DestroyDataTable(Type dataRowType, string name);

        bool DestroyDataTable<T>(IDataTable<T> dataTable) where T : IExternalData;

        bool DestroyDataTable(ExternalDataTableBase dataTable);
    }
}
