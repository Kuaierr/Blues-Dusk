using GameKit.UI;
using UnityEngine;

namespace UnityGameKit.Runtime
{
    public abstract class UIGroupHelperBase : MonoBehaviour, IUIGroupHelper
    {
        public abstract void SetDepth(int depth);
    }
}
