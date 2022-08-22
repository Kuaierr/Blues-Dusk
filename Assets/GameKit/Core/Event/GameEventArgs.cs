using System;

namespace GameKit.Event
{
    public abstract class GameEventArgs : GameKitEventArgs
    {
        public abstract int Id { get; }
    }
}

