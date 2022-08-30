using System.Collections.Generic;

namespace GameKit.DataNode
{
    public interface IDataNode
    {
        string Name { get; }

        string FullName { get; }

        IDataNode Parent { get; }

        IDataNode Next { get; }

        int ChildCount { get; }

        bool IsBranch { get; }

        bool IsLeaf { get; }

        bool IsRoot { get; }

        T GetData<T>() where T : DataNodeVariableBase;

        DataNodeVariableBase GetData();

        void SetData<T>(T data) where T : DataNodeVariableBase;

        void SetData(DataNodeVariableBase data);

        bool HasChild(int index);

        bool HasChild(string name);

        IDataNode GetChild(int index);

        IDataNode GetChild(string name);

        IDataNode GetOrAddChild(string name);

        IDataNode AddChild(IDataNode child);

        IDataNode[] GetAllChild();

        void GetAllChild(List<IDataNode> results);

        void RemoveChild(int index);

        void RemoveChild(string name);

        void ChangeName(string name);

        void Clear();


        string ToString();

        string ToDataString();
    }
}
