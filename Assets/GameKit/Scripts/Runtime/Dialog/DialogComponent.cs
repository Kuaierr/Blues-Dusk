using GameKit;
using GameKit.Fsm;
// using GameKit.Resource;
using GameKit.Event;
using GameKit.Dialog;
using GameKit.DataNode;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameKit.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Kit/GameKit Dialog Component")]
    public sealed class DialogComponent : GameKitComponent
    {
        private const string DataNameForAnimatingNext = "Next State For Animating";
        private const int DefaultPriority = 0;
        private IDialogManager m_DialogManager = null;
        private IDialogTree m_CachedCurrentTree = null;
        private EventComponent m_EventComponent = null;
        private IFsm<DialogComponent> fsm;
        private List<FsmState<DialogComponent>> stateList;
        private InformUICallback m_UpdateDialogCallback;
        private InformUICallback m_UpdateDialogOptionCallback;
        private string m_DialogHelperTypeName = "UnityGameKit.Runtime.TDMLDialogTreeParseHelper";
        [SerializeField]
        private DialogTreePharseHelperBase m_CustomDialogHelper = null;
        private string m_FsmCreateHelperTypeName = "DialogFsmCreateHelper";
        private FsmCreateHelperBase<DialogComponent> m_FsmCreateHelper = null;
        [SerializeField]
        private FsmCreateHelperBase<DialogComponent> m_CustomFsmCreateHelper = null;

        public string AnimatingNextDataName
        {
            get
            {
                return DataNameForAnimatingNext;
            }
        }

        public IDialogTree CurrentDialog
        {
            get
            {
                return m_CachedCurrentTree;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            stateList = new List<FsmState<DialogComponent>>();
            m_DialogManager = GameKitModuleCenter.GetModule<IDialogManager>();
            if (m_DialogManager == null)
            {
                Log.Fatal("Dialog manager is invalid.");
                return;
            }
            m_UpdateDialogCallback += UpdateDialogCallback;
            m_UpdateDialogOptionCallback += UpdateDialogOptionCallback;
            m_DialogManager.StartDialogSuccess += OnStartDialogSuccess;
            m_DialogManager.StartDialogFailure += OnStartDialogFailure;
            m_DialogManager.FinishDialogComplete += OnFinishDialogComplete;

            InitDialogHelper();
            InitFsmCreateHelper();
        }
        private void InitDialogHelper()
        {
            DialogTreePharseHelperBase DialogHelper = Helper.CreateHelper(m_DialogHelperTypeName, m_CustomDialogHelper);
            if (DialogHelper == null)
            {
                Log.Error("Can not create Dialog helper.");
                return;
            }

            DialogHelper.name = "Dialog Helper";
            Transform transform = DialogHelper.transform;
            transform.SetParent(this.transform);
            transform.localScale = Vector3.one;

            m_DialogManager.SetDialogHelper(DialogHelper);
        }

        private void InitFsmCreateHelper()
        {
            m_FsmCreateHelper = Helper.CreateHelper(m_FsmCreateHelperTypeName, m_CustomFsmCreateHelper);
            if (m_FsmCreateHelper == null)
            {
                Log.Error("Can not create Dialog helper.");
                return;
            }

            m_FsmCreateHelper.name = "Dialog Fsm Create Helper";
            Transform transform = m_FsmCreateHelper.transform;
            transform.SetParent(this.transform);
            transform.localScale = Vector3.one;
            // m_DialogManager.SetDialogHelper(FsmCreateHelper);
        }

        private void Start()
        {
            GameKitCoreComponent coreComponent = GameKitComponentCenter.GetComponent<GameKitCoreComponent>();
            if (coreComponent == null)
            {
                Log.Fatal("Core component is invalid.");
                return;
            }

            m_EventComponent = GameKitComponentCenter.GetComponent<EventComponent>();
            if (m_EventComponent == null)
            {
                Log.Fatal("Event component is invalid.");
                return;
            }

            GameKitCenter.Event.Subscribe(StartDialogSuccessEventArgs.EventId, OnStartDialogSuccess);
            CreateFsm();
            StartFsm();

            // if (coreComponent.EditorResourceMode)
            // {
            //     m_DialogManager.SetResourceManager(coreComponent.EditorResourceHelper);
            // }
            // else
            // {
            //     m_DialogManager.SetResourceManager(GameKitModuleCenter.GetModule<IResourceManager>());
            // }
        }

        private void OnDestroy()
        {
            DestroyFsm();
        }

        public void StartDialog(string name)
        {
            if (name == string.Empty)
            {
                Log.Fail("Empty Dialog Name {0}", name);
                return;
            }
            m_DialogManager.GetOrCreatetDialogTree(name);
        }

        private void CreateFsm()
        {
            if (m_FsmCreateHelper == null)
            {
                Log.Error("Fsm Create Helper named '{0}' is null.", m_FsmCreateHelperTypeName);
                return;
            }
            m_FsmCreateHelper.CreateFsm(ref fsm, stateList, gameObject.name, this);
        }

        private void StartFsm()
        {
            if (m_FsmCreateHelper == null)
            {
                Log.Error("Fsm Create Helper named '{0}' is null.", m_FsmCreateHelperTypeName);
                return;
            }

            m_FsmCreateHelper.StartFsm(ref fsm);
        }

        private void DestroyFsm()
        {
            if (m_FsmCreateHelper == null)
            {
                Log.Error("Fsm Create Helper named '{0}' is null.", m_FsmCreateHelperTypeName);
                return;
            }
            m_FsmCreateHelper.DestroyFsm(ref fsm, stateList);
        }

        public void InformDialogUI(IDataNode dialogNode)
        {
            if (m_CachedCurrentTree == null || m_CachedCurrentTree.CurrentNode == null)
                return;
            DialogDataNodeVariable data = dialogNode.GetData<DialogDataNodeVariable>();
            InformDialogUIEventArgs informDialogUIEventArgs = InformDialogUIEventArgs.Create(data, m_UpdateDialogCallback);
            GameKitCenter.Event.Fire(this, informDialogUIEventArgs);
            ReferencePool.Release(informDialogUIEventArgs);
        }

        public void InformOptionUI()
        {
            if (m_CachedCurrentTree == null || m_CachedCurrentTree.CurrentNode == null)
                return;
            IDialogOptionSet dialogOptionSet = m_DialogManager.CreateOptionSet(m_CachedCurrentTree.CurrentNode);
            InformDialogOptionUIEventArgs informDialogOptionUIEventArgs = InformDialogOptionUIEventArgs.Create(dialogOptionSet, m_UpdateDialogOptionCallback);
            GameKitCenter.Event.Fire(this, informDialogOptionUIEventArgs);
            ReferencePool.Release(informDialogOptionUIEventArgs);
        }

        public void InterruptDialogDisplay(InformUICallback callback)
        {
            InterruptDialogDislayEventArgs interruptDialogDislayEventArgs = InterruptDialogDislayEventArgs.Create(callback);
            GameKitCenter.Event.Fire(this, interruptDialogDislayEventArgs);
            ReferencePool.Release(interruptDialogDislayEventArgs);
        }

        public IDataNode ParseNode(int index = 0)
        {
            if (m_CachedCurrentTree == null)
            {
                Log.Warning("Cached Current Tree is Empty");
                return null;
            }
            IDataNode nextNode = m_CachedCurrentTree.GetChildNode(index);
            return ParseNode(nextNode);
        }

        public IDataNode ParseNode(IDataNode currentNode)
        {
            if (m_CachedCurrentTree == null)
            {
                Log.Warning("Cached Current Tree is Empty");
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
                        m_CachedCurrentTree.LocalConditions[tempDialogData.CompleteConditons[j]] = true;
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
            Log.Info("Reach The End Of Conversation.");
            fsm.SetData<VarBoolean>("Dialog Started", false);
            m_CachedCurrentTree.Clear();
            m_CachedCurrentTree = null;
        }

        private void OnStartDialogSuccess(object sender, GameEventArgs e)
        {
            StartDialogSuccessEventArgs ne = (StartDialogSuccessEventArgs)e;
            m_CachedCurrentTree = ne.DialogTree;
            fsm.SetData<VarBoolean>("Dialog Started", true);
        }

        private void OnStartDialogSuccess(object sender, GameKit.Dialog.StartDialogSuccessEventArgs e)
        {
            m_EventComponent.Fire(this, StartDialogSuccessEventArgs.Create(e));
        }

        private void OnStartDialogFailure(object sender, GameKit.Dialog.StartDialogFailureEventArgs e)
        {
            Log.Warning("Start Dialog Failure Complete, Dialog asset name '{0}', ErrorMessage '{1}'", e.DialogAssetName, e.ErrorMessage);
            m_EventComponent.Fire(this, StartDialogFailureEventArgs.Create(e));
        }

        private void OnFinishDialogComplete(object sender, GameKit.Dialog.FinishDialogCompleteEventArgs e)
        {
            m_EventComponent.Fire(this, FinishDialogCompleteEventArgs.Create(e));
        }

        private void UpdateDialogCallback()
        {

        }

        private void UpdateDialogOptionCallback()
        {

        }

        public void OnChooseOption()
        {
            // if(fsm.CurrentState == )
        }
    }
}
