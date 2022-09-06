

using GameKit;
using GameKit.Event;

namespace UnityGameKit.Runtime
{
    public sealed class UndoTransitionCompleteEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(UndoTransitionCompleteEventArgs).GetHashCode();

        public UndoTransitionCompleteEventArgs()
        {
            TransitionType = SceneTransitionType.Immediately;
            UserData = null;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public SceneTransitionType TransitionType
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static UndoTransitionCompleteEventArgs Create(SceneTransitionType transitionType, object userData)
        {
            UndoTransitionCompleteEventArgs undoTransitionCompleteEventArgs = ReferencePool.Acquire<UndoTransitionCompleteEventArgs>();
            undoTransitionCompleteEventArgs.TransitionType = transitionType;
            undoTransitionCompleteEventArgs.UserData = userData;
            return undoTransitionCompleteEventArgs;
        }

        public override void Clear()
        {
            TransitionType = SceneTransitionType.Immediately;
            UserData = null;
        }
    }
}
