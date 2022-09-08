using System.Collections.Generic;
using GameKit.DataNode;

namespace GameKit.Dialog
{
    public interface IDialogTree
    {
        string Name { get; }
        IDataNode StartNode { get; }
        IDataNode RootNode { get; }
        IDataNode CurrentNode { get; set; }
        IDataNodeManager DataNodeManager { get; }
        Dictionary<string, bool> LocalConditions { get; set; }
        IDataNode GetChildNode(int index = 0);
        IDataNode[] GetAllChildNodes();
        void Reset();
        void Release();
        void Update(float elapseSeconds, float realElapseSeconds);
    }
}