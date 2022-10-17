﻿

using GameKit;
using GameKit.Event;
using UnityEngine.SceneManagement;

namespace UnityGameKit.Runtime
{
    public sealed class ActiveSceneChangedEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(ActiveSceneChangedEventArgs).GetHashCode();

        public ActiveSceneChangedEventArgs()
        {
            LastActiveScene = default(Scene);
            ActiveScene = default(Scene);
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public Scene LastActiveScene
        {
            get;
            private set;
        }

        public Scene ActiveScene
        {
            get;
            private set;
        }

        public static ActiveSceneChangedEventArgs Create(Scene lastActiveScene, Scene activeScene)
        {
            ActiveSceneChangedEventArgs activeSceneChangedEventArgs = ReferencePool.Acquire<ActiveSceneChangedEventArgs>();
            activeSceneChangedEventArgs.LastActiveScene = lastActiveScene;
            activeSceneChangedEventArgs.ActiveScene = activeScene;
            return activeSceneChangedEventArgs;
        }

        public override void Clear()
        {
            LastActiveScene = default(Scene);
            ActiveScene = default(Scene);
        }
    }
}
