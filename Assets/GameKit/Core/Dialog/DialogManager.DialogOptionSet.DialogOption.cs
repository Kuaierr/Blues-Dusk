using System.Collections.Generic;
namespace GameKit.Dialog
{
    internal sealed partial class DialogManager : GameKitModule, IDialogManager
    {
        public sealed partial class DialogOptionSet : IDialogOptionSet, IReference
        {
            public class DialogOption : IDialogOption, IReference
            {
                private Dictionary<string, int> m_DiceConditions;
                private int m_SerialId;
                private string m_Text;
                public int Id
                {
                    get
                    {
                        return m_SerialId;
                    }
                }
                public string Text
                {
                    get
                    {
                        return m_Text;
                    }
                }

                public bool HasCondition
                {
                    get
                    {
                        return m_DiceConditions.Count > 0;
                    }
                }

                public Dictionary<string, int> DiceConditions
                {
                    get
                    {
                        return m_DiceConditions;
                    }
                }

                public DialogOption()
                {
                    this.m_SerialId = 0;
                    this.m_Text = string.Empty;
                    this.m_DiceConditions = new Dictionary<string, int>();
                }

                public static DialogOption Create(int index, string text, Dictionary<string, int> diceConditions = null)
                {
                    DialogOption dialogOption = ReferencePool.Acquire<DialogOption>();
                    dialogOption.m_SerialId = index;
                    dialogOption.m_Text = text;
                    if (diceConditions != null)
                        dialogOption.m_DiceConditions = diceConditions;
                    return dialogOption;
                }

                public void Clear()
                {
                    this.m_SerialId = 0;
                    this.m_Text = string.Empty;
                    this.m_DiceConditions.Clear();
                }
            }
        }
    }
}