

using GameKit;
using GameKit.Event;

namespace UnityGameKit.Runtime
{
    public sealed class LoadSceneFailureEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(LoadSceneFailureEventArgs).GetHashCode();

        public LoadSceneFailureEventArgs()
        {
            SceneAssetName = null;
            ErrorMessage = null;
            UserData = null;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public string SceneAssetName
        {
            get;
            private set;
        }

        public string ErrorMessage
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static LoadSceneFailureEventArgs Create(GameKit.Scene.LoadSceneFailureEventArgs e)
        {
            LoadSceneFailureEventArgs loadSceneFailureEventArgs = ReferencePool.Acquire<LoadSceneFailureEventArgs>();
            loadSceneFailureEventArgs.SceneAssetName = e.SceneAssetName;
            loadSceneFailureEventArgs.ErrorMessage = e.ErrorMessage;
            loadSceneFailureEventArgs.UserData = e.UserData;
            return loadSceneFailureEventArgs;
        }

        public override void Clear()
        {
            SceneAssetName = null;
            ErrorMessage = null;
            UserData = null;
        }
    }
}
