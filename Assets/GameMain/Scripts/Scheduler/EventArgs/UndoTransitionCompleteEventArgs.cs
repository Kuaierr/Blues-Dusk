

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
            TargetName = string.Empty;
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

        public string TargetName
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static UndoTransitionCompleteEventArgs Create(string targetName, SceneTransitionType transitionType, object userData)
        {
            UndoTransitionCompleteEventArgs undoTransitionCompleteEventArgs = ReferencePool.Acquire<UndoTransitionCompleteEventArgs>();
            undoTransitionCompleteEventArgs.TransitionType = transitionType;
            undoTransitionCompleteEventArgs.TargetName = targetName;
            undoTransitionCompleteEventArgs.UserData = userData;
            return undoTransitionCompleteEventArgs;
        }

        public override void Clear()
        {
            TransitionType = SceneTransitionType.Immediately;
            TargetName = string.Empty;
            UserData = null;
        }
    }
}
