
using System;
using GameKit;

namespace UnityGameKit.Runtime
{
    public sealed class VarType : Variable<Type>
    {
        public VarType()
        {
        }


        public static implicit operator VarType(Type value)
        {
            VarType varValue = ReferencePool.Acquire<VarType>();
            varValue.Value = value;
            return varValue;
        }

        public static implicit operator Type(VarType value)
        {
            return value.Value;
        }
    }
}
