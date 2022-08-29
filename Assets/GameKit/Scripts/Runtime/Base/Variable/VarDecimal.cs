//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://GameKit.cn/
// Feedback: mailto:ellan@GameKit.cn
//------------------------------------------------------------

using GameKit;

namespace UnityGameKit.Runtime
{
    public sealed class VarDecimal : Variable<decimal>
    {
        public VarDecimal()
        {
        }

        public static implicit operator VarDecimal(decimal value)
        {
            VarDecimal varValue = ReferencePool.Acquire<VarDecimal>();
            varValue.Value = value;
            return varValue;
        }

        public static implicit operator decimal(VarDecimal value)
        {
            return value.Value;
        }
    }
}
