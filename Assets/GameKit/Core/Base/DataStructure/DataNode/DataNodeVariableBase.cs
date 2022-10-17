using System;

namespace GameKit.DataNode
{
    public abstract class DataNodeVariableBase : IReference
    {
        public abstract Type Type { get; }
        public abstract void Clear();
    }
}
