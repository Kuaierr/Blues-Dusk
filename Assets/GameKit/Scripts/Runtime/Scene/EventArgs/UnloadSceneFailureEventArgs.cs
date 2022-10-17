

using GameKit;
using GameKit.Event;

namespace UnityGameKit.Runtime
{
    public sealed class UnloadSceneFailureEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(UnloadSceneFailureEventArgs).GetHashCode();

        public UnloadSceneFailureEventArgs()
        {
            SceneAssetName = null;
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

        public object UserData
        {
            get;
            private set;
        }

        public static UnloadSceneFailureEventArgs Create(GameKit.Scene.UnloadSceneFailureEventArgs e)
        {
            UnloadSceneFailureEventArgs unloadSceneFailureEventArgs = ReferencePool.Acquire<UnloadSceneFailureEventArgs>();
            unloadSceneFailureEventArgs.SceneAssetName = e.SceneAssetName;
            unloadSceneFailureEventArgs.UserData = e.UserData;
            return unloadSceneFailureEventArgs;
        }

        public override void Clear()
        {
            SceneAssetName = null;
            UserData = null;
        }
    }
}
