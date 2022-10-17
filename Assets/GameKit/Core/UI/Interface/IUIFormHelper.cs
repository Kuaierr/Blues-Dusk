namespace GameKit.UI
{
    public interface IUIFormHelper
    {
        object InstantiateUIForm(object uiFormAsset);
        IUIForm CreateUIForm(object uiFormInstance, IUIGroup uiGroup, object userData);
        void ReleaseUIForm(object uiFormAsset, object uiFormInstance);
    }
}
