using GameKit.ObjectPool;

namespace GameKit.UI
{
    internal sealed partial class UIManager : GameKitModule, IUIManager
    {
        private sealed class UIFormObject : ObjectBase
        {
            private object m_UIFormAsset;
            private IUIFormHelper m_UIFormHelper;

            public UIFormObject()
            {
                m_UIFormAsset = null;
                m_UIFormHelper = null;
            }

            public static UIFormObject Create(string name, object uiFormAsset, object uiFormInstance, IUIFormHelper uiFormHelper)
            {
                if (uiFormAsset == null)
                {
                    throw new GameKitException("UI form asset is invalid.");
                }

                if (uiFormHelper == null)
                {
                    throw new GameKitException("UI form helper is invalid.");
                }

                UIFormObject uiFormInstanceObject = ReferencePool.Acquire<UIFormObject>();
                uiFormInstanceObject.Initialize(name, uiFormInstance);
                uiFormInstanceObject.m_UIFormAsset = uiFormAsset;
                uiFormInstanceObject.m_UIFormHelper = uiFormHelper;
                return uiFormInstanceObject;
            }

            public override void Clear()
            {
                base.Clear();
                m_UIFormAsset = null;
                m_UIFormHelper = null;
            }

            protected internal override void Release(bool isShutdown)
            {
                m_UIFormHelper.ReleaseUIForm(m_UIFormAsset, Target);
            }
        }
    }
}
