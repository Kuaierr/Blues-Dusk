using GameKit;
using GameKit.Fsm;
// using GameKit.Resource;
using GameKit.Event;
using GameKit.Dialog;
using GameKit.DataNode;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameKit.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Kit/GameKit Dialog Component")]
    public sealed class DialogComponent : GameKitComponent
    {

        private const int DefaultPriority = 0;
        private IDialogManager m_DialogManager = null;
        private EventComponent m_EventComponent = null;
        private string m_DialogHelperTypeName = "UnityGameKit.Runtime.TDMLDialogTreeParseHelper";
        [SerializeField]
        private DialogTreePharseHelperBase m_CustomDialogHelper = null;
        private IDialogTree m_CachedCurrentTree;

        public IDialogTree CurrentTree
        {
            get
            {
                return m_CachedCurrentTree;
            }
            set
            {
                m_CachedCurrentTree = value;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            m_DialogManager = GameKitModuleCenter.GetModule<IDialogManager>();
            if (m_DialogManager == null)
            {
                Log.Fatal("Dialog manager is invalid.");
                return;
            }
            m_DialogManager.StartDialogSuccess += OnStartDialogSuccess;
            m_DialogManager.StartDialogFailure += OnStartDialogFailure;
            m_DialogManager.FinishDialogComplete += OnFinishDialogComplete;

            InitDialogHelper();
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

            m_EventComponent.Subscribe(StartDialogSuccessEventArgs.EventId, OnStartDialogSuccess);

            // if (coreComponent.EditorResourceMode)
            // {
            //     m_DialogManager.SetResourceManager(coreComponent.EditorResourceHelper);
            // }
            // else
            // {
            //     m_DialogManager.SetResourceManager(GameKitModuleCenter.GetModule<IResourceManager>());
            // }
        }

        public void GetOrCreatetDialogTree(string treeName)
        {
            m_DialogManager.GetOrCreatetDialogTree(treeName);
        }

        public IDialogOptionSet CreateOptionSet(IDataNode node)
        {
            return m_DialogManager.CreateOptionSet(node);
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

        public void StartDialog(string name, string contents)
        {
            if (name == string.Empty)
            {
                Log.Fail("Empty Dialog Name {0}", name);
                return;
            }
            m_DialogManager.CreateDialogTree(name, contents);
        }

        private void OnStartDialogSuccess(object sender, GameEventArgs e)
        {
            Log.Success("Fire StartDialogSuccessEventArg");
            StartDialogSuccessEventArgs ne = (StartDialogSuccessEventArgs)e;
            m_CachedCurrentTree = ne.DialogTree;
        }

        private void OnStartDialogSuccess(object sender, GameKit.Dialog.StartDialogSuccessEventArgs e)
        {
            Log.Success("Fire GameKit.Dialog.StartDialogSuccessEventArg");
            m_EventComponent.Fire(this, StartDialogSuccessEventArgs.Create(e));
            // StartCoroutine(FireNextFrame(sender, e));
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

        IEnumerator FireNextFrame(object sender, GameKit.Dialog.StartDialogSuccessEventArgs e)
        {
            yield return null;
            m_EventComponent.Fire(this, StartDialogSuccessEventArgs.Create(e));
        }
    }
}
