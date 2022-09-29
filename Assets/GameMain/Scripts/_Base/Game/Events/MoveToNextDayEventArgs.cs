using GameKit;
using GameKit.Event;
using GameKit.Entity;

public class MoveToNextDayEventArgs : GameEventArgs
{
    public static int EventId = typeof(MoveToNextDayEventArgs).GetHashCode();
    public MoveToNextDayEventArgs()
    {
        UserData = null;
    }

    public override int Id
    {
        get
        {
            return EventId;
        }
    }

    public object UserData
    {
        get;
        private set;
    }

    public static MoveToNextDayEventArgs Create(object userData)
    {
        MoveToNextDayEventArgs moveToNextDayEventArgs = ReferencePool.Acquire<MoveToNextDayEventArgs>();
        moveToNextDayEventArgs.UserData = userData;
        return moveToNextDayEventArgs;
    }

    public override void Clear()
    {
        UserData = null;
    }
}