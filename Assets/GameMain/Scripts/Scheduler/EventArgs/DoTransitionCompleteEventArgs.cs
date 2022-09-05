

using GameKit;
using GameKit.Event;

namespace UnityGameKit.Runtime
{
    public sealed class DoTransitionCompleteEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(DoTransitionCompleteEventArgs).GetHashCode();

        public DoTransitionCompleteEventArgs()
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

        public static DoTransitionCompleteEventArgs Create(string targetName, SceneTransitionType transitionType, object userData)
        {
            DoTransitionCompleteEventArgs doTransitionCompleteEventArgs = ReferencePool.Acquire<DoTransitionCompleteEventArgs>();
            doTransitionCompleteEventArgs.TransitionType = transitionType;
            doTransitionCompleteEventArgs.TargetName = targetName;
            doTransitionCompleteEventArgs.UserData = userData;
            return doTransitionCompleteEventArgs;
        }

        public override void Clear()
        {
            TransitionType = SceneTransitionType.Immediately;
            TargetName = string.Empty;
            UserData = null;
        }
    }
}
