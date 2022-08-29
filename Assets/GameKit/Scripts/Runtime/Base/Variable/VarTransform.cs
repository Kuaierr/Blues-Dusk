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
    public sealed class VarTransform : Variable<Transform>
    {
        public VarTransform()
        {
        }

        public static implicit operator VarTransform(Transform value)
        {
            VarTransform varValue = ReferencePool.Acquire<VarTransform>();
            varValue.Value = value;
            return varValue;
        }

        public static implicit operator Transform(VarTransform value)
        {
            return value.Value;
        }
    }
}
