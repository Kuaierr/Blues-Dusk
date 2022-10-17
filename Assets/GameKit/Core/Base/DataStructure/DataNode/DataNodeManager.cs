using System;

namespace GameKit.DataNode
{
    internal sealed partial class DataNodeManager : GameKitModule, IDataNodeManager
    {
        private static readonly string[] EmptyStringArray = new string[] { };
        private static readonly string[] PathSplitSeparator = new string[] { ".", "/", "\\" };

        private const string RootName = "<Root>";
        private DataNode m_Root;

        public DataNodeManager()
        {
            m_Root = DataNode.Create(RootName, null);
        }

        public IDataNode Root
        {
            get
            {
                return m_Root;
            }
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }


        internal override void Shutdown()
        {
            ReferencePool.Release(m_Root);
            m_Root = null;
        }

        public T GetData<T>(string path) where T : DataNodeVariableBase
        {
            return GetData<T>(path, null);
        }

        public DataNodeVariableBase GetData(string path)
        {
            return GetData(path, null);
        }

        public T GetData<T>(string path, IDataNode node) where T : DataNodeVariableBase
        {
            IDataNode current = GetNode(path, node);
            if (current == null)
            {
                throw new GameKitException(Utility.Text.Format("Data node is not exist, path '{0}', node '{1}'.", path, node != null ? node.FullName : string.Empty));
            }

            return current.GetData<T>();
        }

        public DataNodeVariableBase GetData(string path, IDataNode node)
        {
            IDataNode current = GetNode(path, node);
            if (current == null)
            {
                throw new GameKitException(Utility.Text.Format("Data node is not exist, path '{0}', node '{1}'.", path, node != null ? node.FullName : string.Empty));
            }

            return current.GetData();
        }

        public void SetData<T>(string path, T data) where T : DataNodeVariableBase
        {
            SetData(path, data, null);
        }

        public void SetData(string path, DataNodeVariableBase data)
        {
            SetData(path, data, null);
        }

        public void SetData<T>(string path, T data, IDataNode node) where T : DataNodeVariableBase
        {
            IDataNode current = GetOrAddNode(path, node);
            current.SetData(data);
        }

        public void SetData(string path, DataNodeVariableBase data, IDataNode node)
        {
            IDataNode current = GetOrAddNode(path, node);
            current.SetData(data);
        }

        public IDataNode GetNode(string path)
        {
            return GetNode(path, null);
        }

        public IDataNode GetNode(string path, IDataNode node)
        {
            IDataNode current = node ?? m_Root;
            string[] splitedPath = GetSplitedPath(path);
            foreach (string i in splitedPath)
            {
                current = current.GetChild(i);
                if (current == null)
                {
                    return null;
                }
            }

            return current;
        }

        public IDataNode GetOrAddNode(string path)
        {
            return GetOrAddNode(path, null);
        }

        public IDataNode GetOrAddNode(string path, IDataNode node)
        {
            IDataNode current = node ?? m_Root;
            string[] splitedPath = GetSplitedPath(path);
            foreach (string i in splitedPath)
            {
                current = current.GetOrAddChild(i);
            }

            return current;
        }

        public void RemoveNode(string path)
        {
            RemoveNode(path, null);
        }

        public void RemoveNode(string path, IDataNode node)
        {
            IDataNode current = node ?? m_Root;
            IDataNode parent = current.Parent;
            string[] splitedPath = GetSplitedPath(path);
            foreach (string i in splitedPath)
            {
                parent = current;
                current = current.GetChild(i);
                if (current == null)
                {
                    return;
                }
            }

            if (parent != null)
            {
                parent.RemoveChild(current.Name);
            }
        }

        public void Clear()
        {
            m_Root.Clear();
        }

        private static string[] GetSplitedPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return EmptyStringArray;
            }

            return path.Split(PathSplitSeparator, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
