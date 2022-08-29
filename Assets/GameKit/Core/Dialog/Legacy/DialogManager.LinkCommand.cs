using System.Collections.Generic;
using GameKit.DataStructure;
using GameKit.DataNode;

namespace GameKit.Dialog
{
    internal sealed partial class DialogManager : GameKitModule, IDialogManager
    {
        public delegate void DialogPostEventHandler(IDataNode nodeA, string nodeB);
        public sealed class LinkCommand : CommandBase
        {
            public IDataNode nodeA;
            public string targetNode;
            public DialogPostEventHandler command;

            public LinkCommand(IDataNode nodeA, string targetNode, DialogPostEventHandler command)
            {
                this.command = command;
                this.nodeA = nodeA;
                this.targetNode = targetNode;
            }

            public override void Excute()
            {
                command.Invoke(nodeA, targetNode);
            }

            public override string ToString()
            {
                return "链接命令: " + nodeA.ToString() + " to " + targetNode;
            }
        }
    }
}