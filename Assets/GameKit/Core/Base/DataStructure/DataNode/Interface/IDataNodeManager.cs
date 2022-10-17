namespace GameKit.DataNode
{
    public interface IDataNodeManager
    {
        IDataNode Root { get; }

        T GetData<T>(string path) where T : DataNodeVariableBase;

        DataNodeVariableBase GetData(string path);

        T GetData<T>(string path, IDataNode node) where T : DataNodeVariableBase;

        DataNodeVariableBase GetData(string path, IDataNode node);
        
        void SetData<T>(string path, T data) where T : DataNodeVariableBase;

        void SetData(string path, DataNodeVariableBase data);

        void SetData<T>(string path, T data, IDataNode node) where T : DataNodeVariableBase;

        void SetData(string path, DataNodeVariableBase data, IDataNode node);

        IDataNode GetNode(string path);

        IDataNode GetNode(string path, IDataNode node);

        IDataNode GetOrAddNode(string path);

        IDataNode GetOrAddNode(string path, IDataNode node);

        void RemoveNode(string path);

        void RemoveNode(string path, IDataNode node);

        void Clear();
    }
}
