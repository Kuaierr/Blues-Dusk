

namespace GameKit
{

    public abstract class BaseEventArgs : GameKitEventArgs
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

