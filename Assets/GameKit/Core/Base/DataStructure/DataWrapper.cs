using System.Collections.Generic;

namespace GameKit.DataStructure
{
    public class DataWrapper : IReference
    {
        public List<object> Data;
        public int Count;
        public DataWrapper()
        {
            Count = 0;
        }

        public DataWrapper Create<T0>(T0 dataA) where T0 : class
        {
            Count = 1;
            DataWrapper dataWrapper = ReferencePool.Acquire<DataWrapper>();
            Data = new List<object>();
            Data.Add(dataA);
            return dataWrapper;
        }

        public DataWrapper Create<T0, T1>(T0 dataA, T1 dataB) where T0 : class where T1 : class
        {
            Count = 2;
            DataWrapper dataWrapper = ReferencePool.Acquire<DataWrapper>();
            Data = new List<object>();
            Data.Add(dataA);
            Data.Add(dataB);
            return dataWrapper;
        }

        public DataWrapper Create<T0, T1, T2>(T0 dataA, T1 dataB, T2 dataC) where T0 : class where T1 : class where T2 : class
        {
            Count = 3;
            DataWrapper dataWrapper = ReferencePool.Acquire<DataWrapper>();
            Data = new List<object>();
            Data.Add(dataA);
            Data.Add(dataB);
            Data.Add(dataC);
            return dataWrapper;
        }

        public void Clear()
        {
            Data.Clear();
        }
    }
}