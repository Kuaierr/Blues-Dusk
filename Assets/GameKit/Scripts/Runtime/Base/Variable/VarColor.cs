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
    public sealed class VarColor : Variable<Color>
    {
        public VarColor()
        {
        }

        public static implicit operator VarColor(Color value)
        {
            VarColor varValue = ReferencePool.Acquire<VarColor>();
            varValue.Value = value;
            return varValue;
        }

        public static implicit operator Color(VarColor value)
        {
            return value.Value;
        }
    }
}
