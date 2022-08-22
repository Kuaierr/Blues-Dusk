namespace GameKit
{
    public enum TaskStartStatus : byte
    {
        Done = 0,

        CanResume,

        HasToWait,

        UnknownError
    }
}
