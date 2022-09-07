using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GameKit;
using GameKit.DataNode;
using GameKit.DataStructure;


namespace GameKit.Dialog
{
    internal sealed partial class DialogManager : GameKitModule, IDialogManager
    {
        public sealed partial class DialogOptionSet : IDialogOptionSet, IReference
        {
            private List<IDialogOption> m_Options;
            private int m_CurrentIndex = 0;
            public List<IDialogOption> Options
            {
                get
                {
                    return m_Options;
                }
            }

            public DialogOptionSet()
            {
                m_CurrentIndex = 0;
                m_Options = new List<IDialogOption>();
            }

            public static DialogOptionSet Create(List<IDataNode> nodes)
            {
                DialogOptionSet dialogOptions = ReferencePool.Acquire<DialogOptionSet>();
                for (int i = 0; i < nodes.Count; i++)
                {
                    IDataNode dialogNode = nodes[i] as IDataNode;
                    DialogDataNodeVariable data = dialogNode.GetData<DialogDataNodeVariable>();
                    string contents = data.Contents;
                    if (data.IsDiceCheckOption)
                        dialogOptions.CreateOption(i, contents, data.DiceConditions);
                    else
                        dialogOptions.CreateOption(i, contents);
                }
                return dialogOptions;
            }

            public static DialogOptionSet Create(IDataNode[] nodes)
            {
                DialogOptionSet dialogOptions = ReferencePool.Acquire<DialogOptionSet>();
                for (int i = 0; i < nodes.Length; i++)
                {
                    IDataNode dialogNode = nodes[i] as IDataNode;
                    DialogDataNodeVariable data = dialogNode.GetData<DialogDataNodeVariable>();
                    string contents = data.Contents;
                    if (data.IsDiceCheckOption)
                        dialogOptions.CreateOption(i, contents, data.DiceConditions);
                    else
                        dialogOptions.CreateOption(i, contents);
                }
                return dialogOptions;
            }

            private void CreateOption(int index, string text, Dictionary<string, int> diceConditions = null)
            {
                DialogOption newOption = DialogOption.Create(index, text, diceConditions);
                m_Options.Add(newOption);
            }

            public void Clear()
            {
                m_Options.Clear();
                m_CurrentIndex = 0;
            }

            public void Release()
            {
                for (int i = 0; i < m_Options.Count; i++)
                {
                    ReferencePool.Release(m_Options[i] as DialogOption);
                }
                ReferencePool.Release(this);
            }
        }
    }
}
