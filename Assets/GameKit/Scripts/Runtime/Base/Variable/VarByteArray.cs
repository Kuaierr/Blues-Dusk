﻿

using GameKit;

namespace UnityGameKit.Runtime
{
    public sealed class VarByteArray : Variable<byte[]>
    {
        public VarByteArray()
        {
        }

        public static implicit operator VarByteArray(byte[] value)
        {
            VarByteArray varValue = ReferencePool.Acquire<VarByteArray>();
            varValue.Value = value;
            return varValue;
        }

        public static implicit operator byte[](VarByteArray value)
        {
            return value.Value;
        }
    }
}
