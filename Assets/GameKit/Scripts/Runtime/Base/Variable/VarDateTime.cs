//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://GameKit.cn/
// Feedback: mailto:ellan@GameKit.cn
//------------------------------------------------------------

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
