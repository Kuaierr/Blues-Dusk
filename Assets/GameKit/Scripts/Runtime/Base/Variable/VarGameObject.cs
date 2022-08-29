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
    public sealed class VarGameObject : Variable<GameObject>
    {
        public VarGameObject()
        {
        }

        public static implicit operator VarGameObject(GameObject value)
        {
            VarGameObject varValue = ReferencePool.Acquire<VarGameObject>();
            varValue.Value = value;
            return varValue;
        }

        public static implicit operator GameObject(VarGameObject value)
        {
            return value.Value;
        }
    }
}
