using GameKit.UI;
using UnityEngine;

namespace GameKit
{
    public class DefaultUIFormHelper : UIFormHelperBase
    {
        // private ResourceComponent m_ResourceComponent = null;

        public override object InstantiateUIForm(object uiFormAsset)
        {
            return Instantiate((Object)uiFormAsset);
        }

        public override IUIForm CreateUIForm(object uiFormInstance, IUIGroup uiGroup, object userData)
        {
            GameObject gameObject = uiFormInstance as GameObject;
            if (gameObject == null)
            {
                Utility.Debugger.LogError("UI form instance is invalid.");
                return null;
            }

            Transform transform = gameObject.transform;
            transform.SetParent(((MonoBehaviour)uiGroup.Helper).transform);
            transform.localScale = Vector3.one;

            return gameObject.GetOrAddComponent<UIForm>();
        }

        public override void ReleaseUIForm(object uiFormAsset, object uiFormInstance)
        {
            // m_ResourceComponent.UnloadAsset(uiFormAsset);
            Destroy((Object)uiFormInstance);
        }

        private void Start()
        {
            // m_ResourceComponent = GameEntry.GetComponent<ResourceComponent>();
            // if (m_ResourceComponent == null)
            // {
            //     Log.Fatal("Resource component is invalid.");
            //     return;
            // }
        }
    }
}
