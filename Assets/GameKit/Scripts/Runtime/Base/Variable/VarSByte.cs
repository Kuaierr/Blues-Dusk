//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://GameKit.cn/
// Feedback: mailto:ellan@GameKit.cn
//------------------------------------------------------------

using GameKit;

namespace UnityGameKit.Runtime
{
    public sealed class VarSByte : Variable<sbyte>
    {
        public VarSByte()
        {
        }

        public static implicit operator VarSByte(sbyte value)
        {
            VarSByte varValue = ReferencePool.Acquire<VarSByte>();
            varValue.Value = value;
            return varValue;
        }

        public static implicit operator sbyte(VarSByte value)
        {
            return value.Value;
        }
    }
}
