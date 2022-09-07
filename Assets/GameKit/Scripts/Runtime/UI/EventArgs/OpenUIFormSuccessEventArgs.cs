using GameKit;
using GameKit.Event;

namespace UnityGameKit.Runtime
{
    public sealed class OpenUIFormSuccessEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(OpenUIFormSuccessEventArgs).GetHashCode();

        public OpenUIFormSuccessEventArgs()
        {
            UIForm = null;
            Duration = 0f;
            UserData = null;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public UIForm UIForm
        {
            get;
            private set;
        }

        public float Duration
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static OpenUIFormSuccessEventArgs Create(GameKit.UI.OpenUIFormSuccessEventArgs e)
        {
            OpenUIFormSuccessEventArgs showUIFormSuccessEventArgs = ReferencePool.Acquire<OpenUIFormSuccessEventArgs>();
            showUIFormSuccessEventArgs.UIForm = (UIForm)e.UIForm;
            showUIFormSuccessEventArgs.Duration = e.Duration;
            showUIFormSuccessEventArgs.UserData = e.UserData;
            return showUIFormSuccessEventArgs;
        }

        public override void Clear()
        {
            UIForm = null;
            Duration = 0f;
            UserData = null;
        }
    }
}
