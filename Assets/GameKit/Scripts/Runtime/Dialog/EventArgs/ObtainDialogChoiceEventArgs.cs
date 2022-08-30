using GameKit;
using GameKit.Dialog;
using GameKit.Event;

namespace UnityGameKit.Runtime
{
    public sealed class ObtainDialogChoiceEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(ObtainDialogChoiceEventArgs).GetHashCode();

        public ObtainDialogChoiceEventArgs()
        {
            ChoosenIndex = 0;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
        }


        public int ChoosenIndex
        {
            get;
            private set;
        }

        public static ObtainDialogChoiceEventArgs Create(int ChoosenIndex)
        {
            ObtainDialogChoiceEventArgs obtainDialogChoiceEventArgs = ReferencePool.Acquire<ObtainDialogChoiceEventArgs>();
            obtainDialogChoiceEventArgs.ChoosenIndex = ChoosenIndex;
            return obtainDialogChoiceEventArgs;
        }

        public override void Clear()
        {
            ChoosenIndex = 0;
        }
    }
}
