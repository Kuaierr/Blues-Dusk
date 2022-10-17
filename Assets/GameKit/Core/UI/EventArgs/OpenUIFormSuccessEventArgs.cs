
namespace GameKit.UI
{
    public sealed class OpenUIFormSuccessEventArgs : GameKitEventArgs
    {
        public OpenUIFormSuccessEventArgs()
        {
            UIForm = null;
            Duration = 0f;
            UserData = null;
        }

        public IUIForm UIForm
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

        public static OpenUIFormSuccessEventArgs Create(IUIForm uiForm, float duration, object userData)
        {
            OpenUIFormSuccessEventArgs openUIFormSuccessEventArgs = ReferencePool.Acquire<OpenUIFormSuccessEventArgs>();
            openUIFormSuccessEventArgs.UIForm = uiForm;
            openUIFormSuccessEventArgs.Duration = duration;
            openUIFormSuccessEventArgs.UserData = userData;
            return openUIFormSuccessEventArgs;
        }

        public override void Clear()
        {
            UIForm = null;
            Duration = 0f;
            UserData = null;
        }
    }
}
