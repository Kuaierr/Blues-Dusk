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
    public sealed class VarQuaternion : Variable<Quaternion>
    {
        public VarQuaternion()
        {
        }

        public static implicit operator VarQuaternion(Quaternion value)
        {
            VarQuaternion varValue = ReferencePool.Acquire<VarQuaternion>();
            varValue.Value = value;
            return varValue;
        }

        public static implicit operator Quaternion(VarQuaternion value)
        {
            return value.Value;
        }
    }
}
