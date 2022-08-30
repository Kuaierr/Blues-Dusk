

using GameKit;
using UnityEngine;

namespace UnityGameKit.Runtime
{
    public sealed class VarAnimator : Variable<Animator>
    {
        public VarAnimator()
        {
        }

        public static implicit operator VarAnimator(Animator value)
        {
            VarAnimator varValue = ReferencePool.Acquire<VarAnimator>();
            varValue.Value = value;
            return varValue;
        }

        public static implicit operator Animator(VarAnimator value)
        {
            return value.Value;
        }
    }
}
