﻿

using GameKit;

namespace UnityGameKit.Runtime
{
    public sealed class VarString : Variable<string>
    {
        public VarString()
        {
        }

        public static implicit operator VarString(string value)
        {
            VarString varValue = ReferencePool.Acquire<VarString>();
            varValue.Value = value;
            return varValue;
        }

        public static implicit operator string(VarString value)
        {
            return value.Value;
        }
    }
}
