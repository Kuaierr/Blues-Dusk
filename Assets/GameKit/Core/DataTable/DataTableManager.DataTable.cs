using System;
using System.Collections;
using System.Collections.Generic;
using GameKit.DataStructure;


namespace GameKit.DataTable
{
    internal sealed partial class DataTableManager : GameKitModule, IDataTableManager
    {
        private sealed class DataTable<T> : DataTableBase, IDataTable<T> where T : class, IDataRow, new()
        {
            private readonly Dictionary<int, T> m_DataSet;
            private T m_MinIdDataRow;
            private T m_MaxIdDataRow;

            public DataTable(string name)
                : base(name)
            {
                m_DataSet = new Dictionary<int, T>();
                m_MinIdDataRow = null;
                m_MaxIdDataRow = null;
            }

            public override Type Type
            {
                get
                {
                    return typeof(T);
                }
            }

            public override int Count
            {
                get
                {
                    return m_DataSet.Count;
                }
            }

            public T this[int id]
            {
                get
                {
                    return GetDataRow(id);
                }
            }

            public T MinIdDataRow
            {
                get
                {
                    return m_MinIdDataRow;
                }
            }

            public T MaxIdDataRow
            {
                get
                {
                    return m_MaxIdDataRow;
                }
            }

            public override bool HasDataRow(int id)
            {
                return m_DataSet.ContainsKey(id);
            }

            public bool HasDataRow(Predicate<T> condition)
            {
                if (condition == null)
                {
                    throw new GameKitException("Condition is invalid.");
                }

                foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                {
                    if (condition(dataRow.Value))
                    {
                        return true;
                    }
                }

                return false;
            }

            public T GetDataRow(int id)
            {
                T dataRow = null;
                if (m_DataSet.TryGetValue(id, out dataRow))
                {
                    return dataRow;
                }

                return null;
            }

            public T GetDataRow(Predicate<T> condition)
            {
                if (condition == null)
                {
                    throw new GameKitException("Condition is invalid.");
                }

                foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                {
                    if (condition(dataRow.Value))
                    {
                        return dataRow.Value;
                    }
                }

                return null;
            }

            public T[] GetDataRows(Predicate<T> condition)
            {
                if (condition == null)
                {
                    throw new GameKitException("Condition is invalid.");
                }

                List<T> results = new List<T>();
                foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                {
                    if (condition(dataRow.Value))
                    {
                        results.Add(dataRow.Value);
                    }
                }

                return results.ToArray();
            }

            public void GetDataRows(Predicate<T> condition, List<T> results)
            {
                if (condition == null)
                {
                    throw new GameKitException("Condition is invalid.");
                }

                if (results == null)
                {
                    throw new GameKitException("Results is invalid.");
                }

                results.Clear();
                foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                {
                    if (condition(dataRow.Value))
                    {
                        results.Add(dataRow.Value);
                    }
                }
            }

            public T[] GetDataRows(Comparison<T> comparison)
            {
                if (comparison == null)
                {
                    throw new GameKitException("Comparison is invalid.");
                }

                List<T> results = new List<T>();
                foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                {
                    results.Add(dataRow.Value);
                }

                results.Sort(comparison);
                return results.ToArray();
            }

            public void GetDataRows(Comparison<T> comparison, List<T> results)
            {
                if (comparison == null)
                {
                    throw new GameKitException("Comparison is invalid.");
                }

                if (results == null)
                {
                    throw new GameKitException("Results is invalid.");
                }

                results.Clear();
                foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                {
                    results.Add(dataRow.Value);
                }

                results.Sort(comparison);
            }

            public T[] GetDataRows(Predicate<T> condition, Comparison<T> comparison)
            {
                if (condition == null)
                {
                    throw new GameKitException("Condition is invalid.");
                }

                if (comparison == null)
                {
                    throw new GameKitException("Comparison is invalid.");
                }

                List<T> results = new List<T>();
                foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                {
                    if (condition(dataRow.Value))
                    {
                        results.Add(dataRow.Value);
                    }
                }

                results.Sort(comparison);
                return results.ToArray();
            }

            public void GetDataRows(Predicate<T> condition, Comparison<T> comparison, List<T> results)
            {
                if (condition == null)
                {
                    throw new GameKitException("Condition is invalid.");
                }

                if (comparison == null)
                {
                    throw new GameKitException("Comparison is invalid.");
                }

                if (results == null)
                {
                    throw new GameKitException("Results is invalid.");
                }

                results.Clear();
                foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                {
                    if (condition(dataRow.Value))
                    {
                        results.Add(dataRow.Value);
                    }
                }

                results.Sort(comparison);
            }

            public T[] GetAllDataRows()
            {
                int index = 0;
                T[] results = new T[m_DataSet.Count];
                foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                {
                    results[index++] = dataRow.Value;
                }

                return results;
            }

            public void GetAllDataRows(List<T> results)
            {
                if (results == null)
                {
                    throw new GameKitException("Results is invalid.");
                }

                results.Clear();
                foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                {
                    results.Add(dataRow.Value);
                }
            }

            public override bool AddDataRow(string dataRowString, object userData)
            {
                try
                {
                    T dataRow = new T();
                    if (!dataRow.ParseDataRow(dataRowString, userData))
                    {
                        return false;
                    }

                    InternalAddDataRow(dataRow);
                    return true;
                }
                catch (Exception exception)
                {
                    if (exception is GameKitException)
                    {
                        throw;
                    }

                    throw new GameKitException(Utility.Text.Format("Can not parse data row string for data table '{0}' with exception '{1}'.", new TypeNamePair(typeof(T), Name), exception), exception);
                }
            }

            public override bool AddDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
            {
                try
                {
                    T dataRow = new T();
                    if (!dataRow.ParseDataRow(dataRowBytes, startIndex, length, userData))
                    {
                        return false;
                    }

                    InternalAddDataRow(dataRow);
                    return true;
                }
                catch (Exception exception)
                {
                    if (exception is GameKitException)
                    {
                        throw;
                    }

                    throw new GameKitException(Utility.Text.Format("Can not parse data row bytes for data table '{0}' with exception '{1}'.", new TypeNamePair(typeof(T), Name), exception), exception);
                }
            }

            public override bool RemoveDataRow(int id)
            {
                if (!HasDataRow(id))
                {
                    return false;
                }

                if (!m_DataSet.Remove(id))
                {
                    return false;
                }

                if (m_MinIdDataRow != null && m_MinIdDataRow.Id == id || m_MaxIdDataRow != null && m_MaxIdDataRow.Id == id)
                {
                    m_MinIdDataRow = null;
                    m_MaxIdDataRow = null;
                    foreach (KeyValuePair<int, T> dataRow in m_DataSet)
                    {
                        if (m_MinIdDataRow == null || m_MinIdDataRow.Id > dataRow.Key)
                        {
                            m_MinIdDataRow = dataRow.Value;
                        }

                        if (m_MaxIdDataRow == null || m_MaxIdDataRow.Id < dataRow.Key)
                        {
                            m_MaxIdDataRow = dataRow.Value;
                        }
                    }
                }

                return true;
            }

            public override void RemoveAllDataRows()
            {
                m_DataSet.Clear();
                m_MinIdDataRow = null;
                m_MaxIdDataRow = null;
            }

            public IEnumerator<T> GetEnumerator()
            {
                return m_DataSet.Values.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return m_DataSet.Values.GetEnumerator();
            }

            internal override void Shutdown()
            {
                m_DataSet.Clear();
            }

            private void InternalAddDataRow(T dataRow)
            {
                if (m_DataSet.ContainsKey(dataRow.Id))
                {
                    throw new GameKitException(Utility.Text.Format("Already exist '{0}' in data table '{1}'.", dataRow.Id, new TypeNamePair(typeof(T), Name)));
                }

                m_DataSet.Add(dataRow.Id, dataRow);

                if (m_MinIdDataRow == null || m_MinIdDataRow.Id > dataRow.Id)
                {
                    m_MinIdDataRow = dataRow;
                }

                if (m_MaxIdDataRow == null || m_MaxIdDataRow.Id < dataRow.Id)
                {
                    m_MaxIdDataRow = dataRow;
                }
            }
        }
    }
}
