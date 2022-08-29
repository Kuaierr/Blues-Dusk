using System;
using System.Collections.Generic;
using GameKit.DataNode;

namespace GameKit.Dialog
{
    public sealed class DialogDataNodeVariable : DataNodeVariableBase
    {
        public string Speaker;
        public string Contents;
        public string MoodName;
        public bool IsFunctional = false;
        public bool IsDivider = false;
        public bool IsCompleter = false;
        public List<string> CompleteConditons;
        public List<string> DividerConditions;
        public DialogNodeCallback m_OnEnter, m_OnUpdate, m_OnExit;

        public DialogDataNodeVariable()
        {
            this.Speaker = "<Default>";
            this.Contents = "<Default>";
            this.MoodName = "<Default>";
            this.DividerConditions = new List<string>();
            this.CompleteConditons = new List<string>();
        }

        public override Type Type
        {
            get
            {
                return typeof(DialogDataNodeVariable);
            }
        }

        public static DialogDataNodeVariable Create(string speaker, string contents, string moodName = "Default")
        {
            DialogDataNodeVariable dialogVariable = ReferencePool.Acquire<DialogDataNodeVariable>();
            dialogVariable.Speaker = speaker;
            dialogVariable.Contents = contents;
            dialogVariable.MoodName = moodName;
            return dialogVariable;
        }

        public void OnEnter()
        {
            m_OnEnter?.Invoke();
        }

        public void OnUpdate()
        {
            m_OnUpdate?.Invoke();
        }
        public void OnExit()
        {
            m_OnExit?.Invoke();
        }

        public override void Clear()
        {
            m_OnEnter = (DialogNodeCallback)System.Delegate.RemoveAll(m_OnEnter, m_OnEnter);
            m_OnUpdate = (DialogNodeCallback)System.Delegate.RemoveAll(m_OnUpdate, m_OnUpdate);
            m_OnExit = (DialogNodeCallback)System.Delegate.RemoveAll(m_OnExit, m_OnExit);
        }
    }
}
