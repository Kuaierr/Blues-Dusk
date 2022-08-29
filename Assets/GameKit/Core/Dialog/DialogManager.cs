
using System;
using System.Collections.Generic;


namespace GameKit.Dialog
{
    internal sealed partial class DialogManager : GameKitModule, IDialogManager
    {
        private Dictionary<string, DialogTree> m_DialogTrees = new Dictionary<string, DialogTree>();
        private DialogTree m_CachedCreateTree;
        private string m_CachedCreateTreeName;
        public void AddTree(DialogTree tree)
        {
            if (!m_DialogTrees.ContainsValue(tree))
            {
                m_DialogTrees.Add(tree.Name, tree);
            }
        }

        public void RemoveTree(string treeName)
        {
            if (m_DialogTrees.Count > 0)
            {
                m_DialogTrees.Remove(treeName);
            }
        }

        public void RemoveTree(DialogTree tree)
        {
            RemoveTree(tree.Name);
        }

        public DialogTree GetTree(string treeName)
        {
            if (m_DialogTrees.ContainsKey(treeName))
            {
                return m_DialogTrees[treeName];
            }
            return null;
        }

        public DialogTree GetOrCreateTree(string treeName)
        {
            m_CachedCreateTreeName = treeName;
            if (m_DialogTrees.ContainsKey(treeName))
            {
                return m_DialogTrees[treeName];
            }
            AddressableManager.instance.GetTextAsyn(treeName, LoadDialogSuccessCallback, LoadDialogFailCallback);
            return null;
        }

        internal override void Shutdown()
        {

        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {

        }

        private void LoadDialogSuccessCallback(string rawData)
        {
            m_CachedCreateTree = DialogTree.Create(m_CachedCreateTreeName, null);
            m_CachedCreateTree.Initialize(rawData);
            AddTree(m_CachedCreateTree);
        }

        private void LoadDialogFailCallback()
        {
            Utility.Debugger.LogFail("Create Dialog Tree for {0} fail.", m_CachedCreateTreeName);
        }
    }
}