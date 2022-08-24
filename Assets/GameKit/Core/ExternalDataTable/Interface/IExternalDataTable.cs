using System;
using System.Collections.Generic;

namespace GameKit.ExternalDataTable
{
    public interface IDataTable<T> : IEnumerable<T> where T : IExternalData
    {
        string Name { get; }

        string FullName { get; }

        Type Type { get; }

        int Count { get; }

        T this[int id] { get; }

        bool HasData(int id);

        bool HasData(Predicate<T> condition);

        T GetData(int id);

        T GetData(Predicate<T> condition);

        T[] GetDatas(Predicate<T> condition);

        void GetDatas(Predicate<T> condition, List<T> results);

        T[] GetDatas(Comparison<T> comparison);

        void GetDatas(Comparison<T> comparison, List<T> results);

        T[] GetDatas(Predicate<T> condition, Comparison<T> comparison);

        void GetDatas(Predicate<T> condition, Comparison<T> comparison, List<T> results);

        T[] GetAllDatas();

        void GetAllDatas(List<T> results);

        bool AddData(object rawData, object userData);

        bool RemoveData(int id);

        void RemoveAllDatas();
    }
}
