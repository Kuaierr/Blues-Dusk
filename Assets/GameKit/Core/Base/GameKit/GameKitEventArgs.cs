using System;

namespace GameKit
{
    public abstract class GameKitEventArgs : EventArgs, IReference
    {
        public abstract void Clear();
    }
}

