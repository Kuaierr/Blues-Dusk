﻿

using GameKit;
using UnityEngine;

namespace UnityGameKit.Runtime
{
    public sealed class VarTexture : Variable<Texture>
    {
        public VarTexture()
        {
        }

        public static implicit operator VarTexture(Texture value)
        {
            VarTexture varValue = ReferencePool.Acquire<VarTexture>();
            varValue.Value = value;
            return varValue;
        }

        public static implicit operator Texture(VarTexture value)
        {
            return value.Value;
        }
    }
}
