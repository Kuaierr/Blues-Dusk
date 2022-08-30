using System.Collections.Generic;

namespace GameKit.DataNode
{
    internal sealed partial class DataNodeManager : GameKitModule, IDataNodeManager
    {
        private sealed class DataNode : IDataNode, IReference
        {
            private static readonly DataNode[] EmptyDataNodeArray = new DataNode[] { };
            private string m_Name;
            private DataNodeVariableBase m_Data;
            private DataNode m_Parent;
            private List<DataNode> m_Childs;

            public DataNode()
            {
                m_Name = null;
                m_Data = null;
                m_Parent = null;
                m_Childs = null;
            }

            public static DataNode Create(string name, DataNode parent)
            {
                if (!IsValidName(name))
                {
                    throw new GameKitException("Name of data node is invalid.");
                }

                DataNode node = ReferencePool.Acquire<DataNode>();
                node.m_Name = name;
                node.m_Parent = parent;
                return node;
            }

            public string Name
            {
                get
                {
                    return m_Name;
                }
            }

            public string FullName
            {
                get
                {
                    return m_Parent == null ? m_Name : Utility.Text.Format("{0}{1}{2}", m_Parent.FullName, PathSplitSeparator[0], m_Name);
                }
            }

            public IDataNode Parent
            {
                get
                {
                    return m_Parent;
                }
            }

            public IDataNode Next
            {
                get
                {
                    if (m_Childs == null || m_Childs.Count < 1)
                        return null;
                    return m_Childs[0];
                }
            }

            public int ChildCount
            {
                get
                {
                    return m_Childs != null ? m_Childs.Count : 0;
                }
            }

            public bool IsBranch
            {
                get
                {
                    return ChildCount > 1;
                }
            }

            public bool IsLeaf
            {
                get
                {
                    if (m_Childs == null)
                        return false;
                    return ChildCount <= 0;
                }
            }

            public bool IsRoot
            {
                get
                {
                    return Parent == null;
                }
            }

            public void ChangeName(string name)
            {
                m_Name = name;
            }

            public T GetData<T>() where T : DataNodeVariableBase
            {
                return (T)m_Data;
            }

            public DataNodeVariableBase GetData()
            {
                return m_Data;
            }

            public void SetData<T>(T data) where T : DataNodeVariableBase
            {
                SetData((DataNodeVariableBase)data);
            }

            public void SetData(DataNodeVariableBase data)
            {
                if (m_Data != null)
                {
                    ReferencePool.Release(m_Data);
                }

                m_Data = data;
            }

            public bool HasChild(int index)
            {
                return index >= 0 && index < ChildCount;
            }

            public bool HasChild(string name)
            {
                if (!IsValidName(name))
                {
                    throw new GameKitException("Name is invalid.");
                }

                if (m_Childs == null)
                {
                    return false;
                }

                foreach (DataNode child in m_Childs)
                {
                    if (child.Name == name)
                    {
                        return true;
                    }
                }
                return false;
            }

            public IDataNode GetChild(int index)
            {
                return index >= 0 && index < ChildCount ? m_Childs[index] : null;
            }

            public IDataNode GetChild(string name)
            {
                if (!IsValidName(name))
                {
                    throw new GameKitException("Name is invalid.");
                }

                if (m_Childs == null)
                {
                    return null;
                }

                foreach (DataNode child in m_Childs)
                {
                    if (child.Name == name)
                    {
                        return child;
                    }
                }

                return null;
            }

            public IDataNode GetOrAddChild(string name)
            {
                DataNode node = (DataNode)GetChild(name);
                if (node != null)
                {
                    return node;
                }

                node = Create(name, this);

                if (m_Childs == null)
                {
                    m_Childs = new List<DataNode>();
                }

                m_Childs.Add(node);

                return node;
            }

            public IDataNode AddChild(IDataNode child)
            {
                if (m_Childs == null)
                {
                    m_Childs = new List<DataNode>();
                }
                m_Childs.Add(child as DataNode);
                return child;
            }

            public IDataNode[] GetAllChild()
            {
                if (m_Childs == null)
                {
                    return EmptyDataNodeArray;
                }

                return m_Childs.ToArray();
            }

            public void GetAllChild(List<IDataNode> results)
            {
                if (results == null)
                {
                    throw new GameKitException("Results is invalid.");
                }

                results.Clear();
                if (m_Childs == null)
                {
                    return;
                }

                foreach (DataNode child in m_Childs)
                {
                    results.Add(child);
                }
            }

            public void RemoveChild(int index)
            {
                DataNode node = (DataNode)GetChild(index);
                if (node == null)
                {
                    return;
                }

                m_Childs.Remove(node);
                ReferencePool.Release(node);
            }

            public void RemoveChild(string name)
            {
                DataNode node = (DataNode)GetChild(name);
                if (node == null)
                {
                    return;
                }

                m_Childs.Remove(node);
                ReferencePool.Release(node);
            }

            public void Clear()
            {
                if (m_Data != null)
                {
                    ReferencePool.Release(m_Data);
                    m_Data = null;
                }

                if (m_Childs != null)
                {
                    foreach (DataNode child in m_Childs)
                    {
                        ReferencePool.Release(child);
                    }

                    m_Childs.Clear();
                }
            }

            public override string ToString()
            {
                return Utility.Text.Format("{0}: {1}", FullName, ToDataString());
            }

            public string ToDataString()
            {
                if (m_Data == null)
                {
                    return "<Null>";
                }

                return Utility.Text.Format("[{0}] {1}", m_Data.Type.Name, m_Data);
            }

            private static bool IsValidName(string name)
            {
                if (string.IsNullOrEmpty(name))
                {
                    return false;
                }

                foreach (string pathSplitSeparator in PathSplitSeparator)
                {
                    if (name.Contains(pathSplitSeparator))
                    {
                        return false;
                    }
                }

                return true;
            }

            void IReference.Clear()
            {
                m_Name = null;
                m_Parent = null;
                Clear();
            }
        }
    }
}
