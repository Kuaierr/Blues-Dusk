

namespace GameKit.Event
{
    public abstract class GameEventArgs : GameKitEventArgs
    {
        public abstract int Id { get; }
        public virtual bool IsManuallyRelease
        {
            get
            {
                return false;
            }
        }
    }
}

