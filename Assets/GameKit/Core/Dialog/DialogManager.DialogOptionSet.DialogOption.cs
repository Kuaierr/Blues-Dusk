namespace GameKit.Dialog
{
    internal sealed partial class DialogManager : GameKitModule, IDialogManager
    {
        public sealed partial class DialogOptionSet : IDialogOptionSet, IReference
        {
            public class DialogOption : IDialogOption, IReference
            {
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
                public DialogOption()
                {
                    this.m_SerialId = 0;
                    this.m_Text = string.Empty;
                }

                public static DialogOption Create(int index, string text)
                {
                    DialogOption dialogOption = ReferencePool.Acquire<DialogOption>();
                    dialogOption.m_SerialId = index;
                    dialogOption.m_Text = text;
                    return dialogOption;
                }

                public void Clear()
                {
                    this.m_SerialId = 0;
                    this.m_Text = string.Empty;
                }
            }
        }
    }
}