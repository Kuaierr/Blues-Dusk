
using System;
using System.Collections.Generic;
using GameKit.DataNode;
namespace GameKit.Dialog
{
    internal sealed partial class DialogManager : GameKitModule, IDialogManager
    {
        private readonly Dictionary<string, DialogTree> m_DialogTrees;
        private readonly List<string> m_LoadingDialogAssetNames;
        private readonly List<string> m_UnloadingDialogAssetNames;
        private IDialogTreeParseHelper m_DialogTreeParseHelper;
        private DialogTree m_CachedCurrentTree;
        private string m_CachedCurrentTreeName;
        private EventHandler<FinishDialogCompleteEventArgs> m_FinishDialogCompleteEventHandler;
        private EventHandler<StartDialogFailureEventArgs> m_StartDialogFailureEventHandler;
        private EventHandler<StartDialogSuccessEventArgs> m_StartDialogSuccessEventHandler;

        public DialogManager()
        {
            m_DialogTrees = new Dictionary<string, DialogTree>();
            m_LoadingDialogAssetNames = new List<string>();
            m_UnloadingDialogAssetNames = new List<string>();
            m_CachedCurrentTree = null;
            m_CachedCurrentTreeName = string.Empty;
            m_FinishDialogCompleteEventHandler = null;
            m_StartDialogFailureEventHandler = null;
        }

        public event EventHandler<FinishDialogCompleteEventArgs> FinishDialogComplete
        {
            add
            {
                m_FinishDialogCompleteEventHandler += value;
            }
            remove
            {
                m_FinishDialogCompleteEventHandler -= value;
            }
        }

        public event EventHandler<StartDialogFailureEventArgs> StartDialogFailure
        {
            add
            {
                m_StartDialogFailureEventHandler += value;
            }
            remove
            {
                m_StartDialogFailureEventHandler -= value;
            }
        }

        public event EventHandler<StartDialogSuccessEventArgs> StartDialogSuccess
        {
            add
            {
                m_StartDialogSuccessEventHandler += value;
            }
            remove
            {
                m_StartDialogSuccessEventHandler -= value;
            }
        }
        public void SetDialogHelper(IDialogTreeParseHelper helper)
        {
            m_DialogTreeParseHelper = helper;
        }

        public bool HasDialogTree(string treeName)
        {
            if (string.IsNullOrEmpty(treeName))
            {
                throw new GameKitException("Dialog Tree name is invalid.");
            }
            return m_DialogTrees.ContainsKey(treeName);
        }
        public void AddDialogTree(DialogTree tree)
        {
            if (!HasDialogTree(tree.Name))
            {
                m_DialogTrees.Add(tree.Name, tree);
            }
        }

        public void RemoveDialogTree(string treeName)
        {
            if (m_DialogTrees.Count > 0)
            {
                m_DialogTrees.Remove(treeName);
            }
        }

        public void RemoveDialogTree(DialogTree tree)
        {
            RemoveDialogTree(tree.Name);
        }

        public DialogTree GetDialogTree(string treeName)
        {
            if (HasDialogTree(treeName))
            {
                return m_DialogTrees[treeName];
            }
            return null;
        }

        public void CreateDialogTree(string treeName, string content)
        {
            m_CachedCurrentTreeName = treeName;
            if(content==string.Empty)
            {
                Utility.Debugger.LogError("Content for phasing is invalid");
                return;
            }

            if (HasDialogTree(treeName))
            {
                m_CachedCurrentTree = m_DialogTrees[treeName];
                m_CachedCurrentTree.Reset();
                InternalStartDialog(m_CachedCurrentTree);
                return;
            }

            LoadDialogSuccessCallback(content);
        }

        public void GetOrCreatetDialogTree(string treeName)
        {
            m_CachedCurrentTreeName = treeName;
            if (HasDialogTree(treeName))
            {
                m_CachedCurrentTree = m_DialogTrees[treeName];
                m_CachedCurrentTree.Reset();
                InternalStartDialog(m_CachedCurrentTree);
                return;
            }
            AddressableManager.instance.GetTextAsyn(treeName, LoadDialogSuccessCallback, LoadDialogFailCallback);
        }

        public IDialogOptionSet CreateOptionSet(IDataNode node)
        {
            return DialogOptionSet.Create(node.GetAllChild());
        }

        // public List<Option> GetOptions()
        // {
        //     if (m_CurrentNode.Sons.Count > 1)
        //     {
        //         List<Option> options = DialogSelection.CreateSelection(m_CurrentNode.Sons);
        //         return options;
        //     }
        //     return null;
        // }

        internal override void Shutdown()
        {
            m_LoadingDialogAssetNames.Clear();
            m_UnloadingDialogAssetNames.Clear();
            m_DialogTrees.Clear();
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {

        }

        private void InternalStartDialog(DialogTree dialogTree)
        {
            if (m_StartDialogSuccessEventHandler != null)
            {
                StartDialogSuccessEventArgs startDialogSuccessEventArgs = StartDialogSuccessEventArgs.Create(dialogTree, null);
                m_StartDialogSuccessEventHandler.Invoke(this, startDialogSuccessEventArgs);
                ReferencePool.Release(startDialogSuccessEventArgs);
                return;
            }
            Utility.Debugger.LogError("Start Dialog Success EventHandler is Null.");
        }

        private void LoadDialogSuccessCallback(string rawData)
        {
            m_CachedCurrentTree = DialogTree.Create(m_CachedCurrentTreeName);
            m_DialogTreeParseHelper.Phase(rawData, m_CachedCurrentTree);
            AddDialogTree(m_CachedCurrentTree);
            InternalStartDialog(m_CachedCurrentTree);
        }


        private void FinishDialogCompleteCallback()
        {
            if (m_FinishDialogCompleteEventHandler != null)
            {
                FinishDialogCompleteEventArgs finishDialogCompleteEventArgs = FinishDialogCompleteEventArgs.Create(m_CachedCurrentTreeName, null);
                m_FinishDialogCompleteEventHandler.Invoke(this, finishDialogCompleteEventArgs);
                ReferencePool.Release(finishDialogCompleteEventArgs);
            }
        }

        private void LoadDialogFailCallback()
        {
            m_LoadingDialogAssetNames.Remove(m_CachedCurrentTreeName);
            string appendErrorMessage = Utility.Text.Format("Load dialog failure, scene asset name '{0}', error message '{1}'.", m_CachedCurrentTreeName, "Can not find shit.");
            if (m_StartDialogFailureEventHandler != null)
            {
                StartDialogFailureEventArgs startDialogFailureEventArgs = StartDialogFailureEventArgs.Create(m_CachedCurrentTreeName, appendErrorMessage, null);
                m_StartDialogFailureEventHandler.Invoke(this, startDialogFailureEventArgs);
                ReferencePool.Release(startDialogFailureEventArgs);
                return;
            }
        }
    }
}