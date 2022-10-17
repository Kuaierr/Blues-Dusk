

using GameKit;
using System;

namespace UnityGameKit.Runtime
{
    public sealed class VarDateTime : Variable<DateTime>
    {
        public VarDateTime()
        {
        }

        public static implicit operator VarDateTime(DateTime value)
        {
            VarDateTime varValue = ReferencePool.Acquire<VarDateTime>();
            varValue.Value = value;
            return varValue;
        }

        public static implicit operator DateTime(VarDateTime value)
        {
            return value.Value;
        }
    }
}
