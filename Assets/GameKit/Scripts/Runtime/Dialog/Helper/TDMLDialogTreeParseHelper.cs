using GameKit.DataNode;
using System.Collections.Generic;
using GameKit.Dialog;
using GameKit;

namespace UnityGameKit.Runtime
{
    public sealed partial class TDMLDialogTreeParseHelper : DialogTreePharseHelperBase
    {
        private Queue<CommandBase> m_LinkBuffer;
        private List<IDataNode> m_DeclaredNodes;
        private List<IDataNode> m_BranchNodes;
        private IDialogTree m_DialogTree;

        private void Awake()
        {
            m_BranchNodes = new List<IDataNode>();
            m_DeclaredNodes = new List<IDataNode>();
            m_LinkBuffer = new Queue<CommandBase>();
        }
        public override void Phase(string rawData, IDialogTree dialogTree)
        {
            // throw new System.NotImplementedException();
            Clear();
            string[] lines = rawData.Replace(((char)13).ToString(), "").Replace("\t", "").Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            m_DialogTree = dialogTree;
            for (int i = 0; i < lines.Length; i++)
            {
                IDataNode node = dialogTree.DataNodeManager.GetOrAddNode(i.ToString());
                // m_DataNodeManager.SetData<DialogDataNodeVariable>(DialogDataNodeVariable.Create());
                // DialogPhaser.PhaseNode(node, line);
            }
            ExcuteAllBufferCommand();
            OnBuildEnd();
        }

        public void Clear()
        {
            m_DeclaredNodes.Clear();
            m_BranchNodes.Clear();
            m_LinkBuffer.Clear();
            m_DialogTree = null;
        }

        public void AddFromLast(IDataNode node)
        {
            AddFrom(node, m_DialogTree.CurrentNode as IDataNode);
        }

        public void RecordBranch<T>(IDataNode node)
        {
            if (!m_BranchNodes.Contains(node))
                m_BranchNodes.Add(node);
        }

        public void RecordDeclaredDataNode(IDataNode node)
        {
            if (!m_DeclaredNodes.Contains(node))
                m_DeclaredNodes.Add(node);
        }

        public bool ContainsDeclaredNode(string name)
        {
            foreach (var node in m_DeclaredNodes)
            {
                if (node.Name == name)
                    return true;
            }
            return false;
        }

        public void CachedLinkToDeclared(IDataNode srcnode, string name)
        {
            LinkCommand command = new LinkCommand(srcnode, name, LinkToDeclared);
            m_LinkBuffer.Enqueue(command);
        }

        public void CachedLinkFromDeclared(IDataNode srcnode, string name)
        {
            LinkCommand command = new LinkCommand(srcnode, name, LinkFromDeclared);
            m_LinkBuffer.Enqueue(command);
        }

        public void LinkToDeclared(IDataNode srcnode, string name)
        {
            foreach (var node in m_DeclaredNodes)
            {
                if (node.Name == name)
                {

                    AddTo(srcnode, (node as IDataNode));
                    break;
                }
            }
        }

        public void LinkFromDeclared(IDataNode srcnode, string name)
        {
            foreach (var node in m_DeclaredNodes)
            {
                if (node.Name == name)
                {
                    AddFrom(srcnode, (node as IDataNode));
                    break;
                }
            }
        }

        public void AddFrom(IDataNode target, IDataNode parent)
        {
            // parent.GetOrAddChild(target);
        }

        public void AddTo(IDataNode target, IDataNode son)
        {
            // target.GetOrAddChild(son);
        }

        public void ExcuteAllBufferCommand()
        {
            foreach (var command in m_LinkBuffer)
            {
                (command as LinkCommand).Excute();
            }
            m_LinkBuffer.Clear();
        }

        public void OnBuildEnd()
        {
            m_DialogTree.Reset();
        }
    }
}