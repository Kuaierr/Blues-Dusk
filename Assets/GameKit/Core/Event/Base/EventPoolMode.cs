using System;

namespace GameKit
{
    [Flags]
    internal enum EventPoolMode : byte
    {
        Default = 0,

        AllowNoHandler = 1,

        AllowMultiHandler = 2,

        AllowDuplicateHandler = 4
    }
}
