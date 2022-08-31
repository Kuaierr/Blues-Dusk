using GameKit.UI;
using GameKit;
using UnityEngine;
using UnityGameKit.Runtime;

public class UIFormBaseHelper : UIFormHelperBase
{
    // private ResourceComponent m_ResourceComponent = null;
    public override object InstantiateUIForm(object uiFormAsset)
    {
        GameObject uIInstance = Instantiate((GameObject)uiFormAsset); 
        UIFormBase uiForm = uIInstance.GetComponent<UIFormBase>();
        uiForm.OnInstantiate();       
        return uIInstance;
    }

    public override IUIForm CreateUIForm(object uiFormInstance, IUIGroup uiGroup, object userData)
    {
        GameObject gameObject = uiFormInstance as GameObject;
        
        if (gameObject == null)
        {
            Utility.Debugger.LogError("UI form instance is invalid.");
            return null;
        }

        // RectTransform transform = gameObject.GetOrAddComponent<RectTransform>();
        Transform transform = gameObject.transform;
        transform.SetParent(((MonoBehaviour)uiGroup.Helper).transform);
        transform.localScale = Vector3.one;


        return gameObject.GetOrAddComponent<UIForm>();
    }

    public override void ReleaseUIForm(object uiFormAsset, object uiFormInstance)
    {
        // m_ResourceComponent.UnloadAsset(uiFormAsset);
        AddressableManager.instance.ReleaseHandle(uiFormAsset);
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

