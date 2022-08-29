//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://GameKit.cn/
// Feedback: mailto:ellan@GameKit.cn
//------------------------------------------------------------

using GameKit;
using UnityEngine;

namespace UnityGameKit.Runtime
{
    public sealed class VarRect : Variable<Rect>
    {
        public VarRect()
        {
        }

        public static implicit operator VarRect(Rect value)
        {
            VarRect varValue = ReferencePool.Acquire<VarRect>();
            varValue.Value = value;
            return varValue;
        }

        public static implicit operator Rect(VarRect value)
        {
            return value.Value;
        }
    }
}
