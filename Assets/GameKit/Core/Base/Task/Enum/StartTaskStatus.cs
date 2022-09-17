namespace GameKit
{
    public enum StartTaskStatus : byte
    {
        Done = 0,

        CanResume,

        HasToWait,

        UnknownError
    }
}
