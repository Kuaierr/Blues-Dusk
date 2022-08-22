using GameKit.UI;
using UnityEngine;

namespace GameKit
{
    public abstract class UIFormHelperBase : MonoBehaviour, IUIFormHelper
    {
        public abstract object InstantiateUIForm(object uiFormAsset);

        public abstract IUIForm CreateUIForm(object uiFormInstance, IUIGroup uiGroup, object userData);

        public abstract void ReleaseUIForm(object uiFormAsset, object uiFormInstance);
    }
}
