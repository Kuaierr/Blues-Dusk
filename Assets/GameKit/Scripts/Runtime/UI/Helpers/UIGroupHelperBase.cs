using GameKit.UI;
using UnityEngine;

namespace GameKit
{
    public abstract class UIGroupHelperBase : MonoBehaviour, IUIGroupHelper
    {
        public abstract void SetDepth(int depth);
    }
}
