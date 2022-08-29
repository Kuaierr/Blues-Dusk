
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
        private EventHandler<LoadDialogSuccessEventArgs> m_LoadDialogSuccessEventHandler;
        private EventHandler<LoadDialogFailureEventArgs> m_LoadDialogFailureEventHandler;
        private DialogTree m_CachedCurrentTree;
        private string m_CachedCurrentTreeName;

        public DialogManager()
        {
            m_DialogTrees = new Dictionary<string, DialogTree>();
            m_LoadingDialogAssetNames = new List<string>();
            m_UnloadingDialogAssetNames = new List<string>();
            m_CachedCurrentTree = null;
            m_CachedCurrentTreeName = string.Empty;
            m_LoadDialogSuccessEventHandler = null;
            m_LoadDialogFailureEventHandler = null;
        }

        public event EventHandler<LoadDialogSuccessEventArgs> LoadDialogSuccess
        {
            add
            {
                m_LoadDialogSuccessEventHandler += value;
            }
            remove
            {
                m_LoadDialogSuccessEventHandler -= value;
            }
        }

        public event EventHandler<LoadDialogFailureEventArgs> LoadDialogFailure
        {
            add
            {
                m_LoadDialogFailureEventHandler += value;
            }
            remove
            {
                m_LoadDialogFailureEventHandler -= value;
            }
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

        public DialogTree GetOrCreateTree(string treeName)
        {
            m_CachedCurrentTreeName = treeName;
            if (HasDialogTree(treeName))
            {
                return m_DialogTrees[treeName];
            }
            AddressableManager.instance.GetTextAsyn(treeName, LoadDialogSuccessCallback, LoadDialogFailCallback);
            return null;
        }

        public void StartDialog(string title, string dialogText)
        {
            Utility.Debugger.LogSuccess("Start Dialog {0}", title);

            // dialogTree = DialogDManager.instance.CreateTree(title, dialogText);
            // dialogTree.Reset();
        }

        public void ParseNode(int index = 0)
        {
            if (m_CachedCurrentTree == null)
            {
                Utility.Debugger.LogError("Cached Current Tree is Empty");
                return;
            }
            IDataNode nextNode = m_CachedCurrentTree.GetChildNode(index);
            ParseNode(nextNode);
        }

        public IDataNode ParseNode(IDataNode currentNode)
        {
            if (m_CachedCurrentTree == null)
            {
                Utility.Debugger.LogError("Cached Current Tree is Empty");
                return null;
            }

            IDataNode nextNode = null;
            if (currentNode == null)
            {
                ReachTheEndOfConversation();
                return null;
            }

            DialogDataNodeVariable tempDialogData = currentNode.GetData<DialogDataNodeVariable>();
            if (tempDialogData.IsFunctional)
            {
                if (tempDialogData.IsCompleter)
                {
                    for (int j = 0; j < tempDialogData.CompleteConditons.Count; j++)
                    {
                        m_CachedCurrentTree.SetCondition(tempDialogData.CompleteConditons[j], true);
                    }
                }

                if (tempDialogData.IsDivider)
                {
                    bool isComplete = true;
                    for (int j = 0; j < tempDialogData.DividerConditions.Count; j++)
                    {
                        if (!m_CachedCurrentTree.LocalConditions[tempDialogData.DividerConditions[j]])
                        {
                            isComplete = false;
                            break;
                        }
                    }

                    if (isComplete)
                    {
                        nextNode = m_CachedCurrentTree.GetChildNode(0);
                    }
                    else
                    {
                        nextNode = m_CachedCurrentTree.GetChildNode(1);
                    }
                }
            }
            else
            {
                if (currentNode.IsBranch)
                {
                    nextNode = currentNode;
                }
                else
                {
                    nextNode = currentNode;
                }
            }
            return nextNode;
        }

        private void ReachTheEndOfConversation()
        {
            Utility.Debugger.Log("Reach The End Of Conversation.");
            m_CachedCurrentTree.Clear();
            m_CachedCurrentTree = null;
        }

        internal override void Shutdown()
        {

        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {

        }



        private void LoadDialogSuccessCallback(string rawData)
        {
            m_CachedCurrentTree = DialogTree.Create(m_CachedCurrentTreeName, null);
            m_CachedCurrentTree.Initialize(rawData);
            AddDialogTree(m_CachedCurrentTree);
            if (m_LoadDialogSuccessEventHandler != null)
            {
                LoadDialogSuccessEventArgs loadDialogSuccessEventArgs = LoadDialogSuccessEventArgs.Create(m_CachedCurrentTreeName, null);
                m_LoadDialogSuccessEventHandler(this, loadDialogSuccessEventArgs);
                ReferencePool.Release(loadDialogSuccessEventArgs);
            }
        }

        private void LoadDialogFailCallback()
        {
            Utility.Debugger.LogFail("Create Dialog Tree for {0} fail.", m_CachedCurrentTreeName);
            m_LoadingDialogAssetNames.Remove(m_CachedCurrentTreeName);
            string appendErrorMessage = Utility.Text.Format("Load scene failure, scene asset name '{0}', error message '{1}'.", m_CachedCurrentTreeName, "Can not find shit.");
            if (m_LoadDialogFailureEventHandler != null)
            {
                LoadDialogFailureEventArgs loadDialogFailureEventArgs = LoadDialogFailureEventArgs.Create(m_CachedCurrentTreeName, appendErrorMessage, null);
                m_LoadDialogFailureEventHandler(this, loadDialogFailureEventArgs);
                ReferencePool.Release(loadDialogFailureEventArgs);
                return;
            }
        }
    }
}