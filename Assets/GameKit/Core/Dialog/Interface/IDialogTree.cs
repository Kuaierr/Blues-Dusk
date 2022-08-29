using System.Collections.Generic;
using GameKit.DataNode;

namespace GameKit.Dialog
{
    public interface IDialogTree
    {
        string Name { get; }
        IDataNode StartNode { get; }
        IDataNode RootNode { get; }
        IDataNode CurrentNode { get; }
        IDataNodeManager DataNodeManager { get; }
        Dictionary<string, bool> LocalConditions { get; }
        void Reset();
    }
}