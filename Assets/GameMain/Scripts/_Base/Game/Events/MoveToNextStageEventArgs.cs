using GameKit;
using GameKit.Event;


public class MoveToNextStageEventArgs : GameEventArgs
{
    public static int EventId = typeof(MoveToNextStageEventArgs).GetHashCode();
    public MoveToNextStageEventArgs()
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

    public static MoveToNextStageEventArgs Create(object userData)
    {
        MoveToNextStageEventArgs moveToNextStageEventArgs = ReferencePool.Acquire<MoveToNextStageEventArgs>();
        moveToNextStageEventArgs.UserData = userData;
        return moveToNextStageEventArgs;
    }

    public override void Clear()
    {
        UserData = null;
    }
}