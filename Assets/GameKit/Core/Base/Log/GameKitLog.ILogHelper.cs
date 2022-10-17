namespace GameKit
{
    public static partial class GameKitLog
    {
        public interface ILogHelper
        {
            void Log(GameKitLogType type, object message);
        }
    }
}
