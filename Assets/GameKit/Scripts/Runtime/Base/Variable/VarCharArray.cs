//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://GameKit.cn/
// Feedback: mailto:ellan@GameKit.cn
//------------------------------------------------------------

using GameKit;

namespace UnityGameKit.Runtime
{
    public sealed class VarCharArray : Variable<char[]>
    {
        public VarCharArray()
        {
        }

        public static implicit operator VarCharArray(char[] value)
        {
            VarCharArray varValue = ReferencePool.Acquire<VarCharArray>();
            varValue.Value = value;
            return varValue;
        }

        public static implicit operator char[](VarCharArray value)
        {
            return value.Value;
        }
    }
}
