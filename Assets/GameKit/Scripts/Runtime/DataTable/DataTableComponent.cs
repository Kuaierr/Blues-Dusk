using GameKit;
using GameKit.DataTable;
//using GameKit.Resource;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameKit.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("GameKit/GameKit DataTable Component")]
    public sealed class DataTableComponent : GameKitComponent
    {
        private const int DefaultPriority = 0;
        private IDataTableManager m_DataTableManager = null;
        private EventComponent m_EventComponent = null;
        [SerializeField]
        private string m_DataTableHelperTypeName = "UnityGameKit.Runtime.DefaultDataTableHelper";

        [SerializeField]
        private DataTableHelperBase m_CustomDataTableHelper = null;

        [SerializeField]
        private int m_CachedBytesSize = 0;

        public int Count
        {
            get
            {
                return m_DataTableManager.Count;
            }
        }

        public int CachedBytesSize
        {
            get
            {
                return m_DataTableManager.CachedBytesSize;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            m_DataTableManager = GameKitModuleCenter.GetModule<IDataTableManager>();
            if (m_DataTableManager == null)
            {
                Log.Fatal("Data table manager is invalid.");
                return;
            }
        }

        private void Start()
        {
            GameKitCoreComponent baseComponent = GameKitComponentCenter.GetComponent<GameKitCoreComponent>();
            if (baseComponent == null)
            {
                Log.Fatal("Base component is invalid.");
                return;
            }

            m_EventComponent = GameKitComponentCenter.GetComponent<EventComponent>();
            if (m_EventComponent == null)
            {
                Log.Fatal("Event component is invalid.");
                return;
            }

            // if (baseComponent.EditorResourceMode)
            // {
            //     m_DataTableManager.SetResourceManager(baseComponent.EditorResourceHelper);
            // }
            // else
            // {
            //     m_DataTableManager.SetResourceManager(GameKitModuleCenter.GetModule<IResourceManager>());
            // }

            DataTableHelperBase dataTableHelper = Helper.CreateHelper(m_DataTableHelperTypeName, m_CustomDataTableHelper);
            if (dataTableHelper == null)
            {
                Log.Error("Can not create data table helper.");
                return;
            }

            dataTableHelper.name = "Data Table Helper";
            Transform transform = dataTableHelper.transform;
            transform.SetParent(this.transform);
            transform.localScale = Vector3.one;

            m_DataTableManager.SetDataProviderHelper(dataTableHelper);
            m_DataTableManager.SetDataTableHelper(dataTableHelper);
            if (m_CachedBytesSize > 0)
            {
                EnsureCachedBytesSize(m_CachedBytesSize);
            }
        }

        public void EnsureCachedBytesSize(int ensureSize)
        {
            m_DataTableManager.EnsureCachedBytesSize(ensureSize);
        }

        public void FreeCachedBytes()
        {
            m_DataTableManager.FreeCachedBytes();
        }

        public bool HasDataTable<T>() where T : IDataRow
        {
            return m_DataTableManager.HasDataTable<T>();
        }

        public bool HasDataTable(Type dataRowType)
        {
            return m_DataTableManager.HasDataTable(dataRowType);
        }

        public bool HasDataTable<T>(string name) where T : IDataRow
        {
            return m_DataTableManager.HasDataTable<T>(name);
        }

        public bool HasDataTable(Type dataRowType, string name)
        {
            return m_DataTableManager.HasDataTable(dataRowType, name);
        }

        public IDataTable<T> GetDataTable<T>() where T : IDataRow
        {
            return m_DataTableManager.GetDataTable<T>();
        }

        public DataTableBase GetDataTable(Type dataRowType)
        {
            return m_DataTableManager.GetDataTable(dataRowType);
        }

        public IDataTable<T> GetDataTable<T>(string name) where T : IDataRow
        {
            return m_DataTableManager.GetDataTable<T>(name);
        }

        public DataTableBase GetDataTable(Type dataRowType, string name)
        {
            return m_DataTableManager.GetDataTable(dataRowType, name);
        }

        public DataTableBase[] GetAllDataTables()
        {
            return m_DataTableManager.GetAllDataTables();
        }

        public void GetAllDataTables(List<DataTableBase> results)
        {
            m_DataTableManager.GetAllDataTables(results);
        }

        public IDataTable<T> CreateDataTable<T>() where T : class, IDataRow, new()
        {
            return CreateDataTable<T>(null);
        }

        public DataTableBase CreateDataTable(Type dataRowType)
        {
            return CreateDataTable(dataRowType, null);
        }

        public IDataTable<T> CreateDataTable<T>(string name) where T : class, IDataRow, new()
        {
            IDataTable<T> dataTable = m_DataTableManager.CreateDataTable<T>(name);
            DataTableBase dataTableBase = (DataTableBase)dataTable;
            dataTableBase.ReadDataSuccess += OnReadDataSuccess;
            dataTableBase.ReadDataFailure += OnReadDataFailure;
            return dataTable;
        }

        public DataTableBase CreateDataTable(Type dataRowType, string name)
        {
            DataTableBase dataTable = m_DataTableManager.CreateDataTable(dataRowType, name);
            dataTable.ReadDataSuccess += OnReadDataSuccess;
            dataTable.ReadDataFailure += OnReadDataFailure;
            return dataTable;
        }

        public bool DestroyDataTable<T>() where T : IDataRow, new()
        {
            return m_DataTableManager.DestroyDataTable<T>();
        }

        public bool DestroyDataTable(Type dataRowType)
        {
            return m_DataTableManager.DestroyDataTable(dataRowType);
        }

        public bool DestroyDataTable<T>(string name) where T : IDataRow
        {
            return m_DataTableManager.DestroyDataTable<T>(name);
        }

        public bool DestroyDataTable(Type dataRowType, string name)
        {
            return m_DataTableManager.DestroyDataTable(dataRowType, name);
        }

        public bool DestroyDataTable<T>(IDataTable<T> dataTable) where T : IDataRow
        {
            return m_DataTableManager.DestroyDataTable(dataTable);
        }

        public bool DestroyDataTable(DataTableBase dataTable)
        {
            return m_DataTableManager.DestroyDataTable(dataTable);
        }

        private void OnReadDataSuccess(object sender, ReadDataSuccessEventArgs e)
        {
            m_EventComponent.Fire(this, LoadDataTableSuccessEventArgs.Create(e));
        }

        private void OnReadDataFailure(object sender, ReadDataFailureEventArgs e)
        {
            Log.Warning("Load data table failure, asset name '{0}', error message '{1}'.", e.DataAssetName, e.ErrorMessage);
            m_EventComponent.Fire(this, LoadDataTableFailureEventArgs.Create(e));
        }
    }
}
