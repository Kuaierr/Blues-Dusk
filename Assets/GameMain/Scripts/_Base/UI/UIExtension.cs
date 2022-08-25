using GameKit.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityGameKit.Runtime;
using UiConfig = LubanConfig.DataTable.UIConfig;

public static class UIExtension
{
    public static IEnumerator FadeToAlpha(this CanvasGroup canvasGroup, float alpha, float duration)
    {
        float time = 0f;
        float originalAlpha = canvasGroup.alpha;
        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(originalAlpha, alpha, time / duration);
            yield return new WaitForEndOfFrame();
        }
        canvasGroup.alpha = alpha;
    }

    public static IEnumerator SmoothValue(this Slider slider, float value, float duration)
    {
        float time = 0f;
        float originalValue = slider.value;
        while (time < duration)
        {
            time += Time.deltaTime;
            slider.value = Mathf.Lerp(originalValue, value, time / duration);
            yield return new WaitForEndOfFrame();
        }

        slider.value = value;
    }

    public static bool HasUIForm(this UIComponent uiComponent, string uiFormName, string uiGroupName = null)
    {
        string assetName = AssetUtility.GetUIFormAsset(uiFormName);
        if (string.IsNullOrEmpty(uiGroupName))
        {
            return uiComponent.HasUIForm(assetName);
        }

        IUIGroup uiGroup = uiComponent.GetUIGroup(uiGroupName);
        if (uiGroup == null)
        {
            return false;
        }
        return uiGroup.HasUIForm(assetName);
    }

    public static bool HasUIForm(this UIComponent uiComponent, int uiFormId, string uiGroupName = null)
    {
        if (string.IsNullOrEmpty(uiGroupName))
        {
            return uiComponent.HasUIForm(uiFormId);
        }

        IUIGroup uiGroup = uiComponent.GetUIGroup(uiGroupName);
        if (uiGroup == null)
        {
            return false;
        }
        return uiGroup.HasUIForm(uiFormId);
    }

    public static UIFormBase GetUIForm(this UIComponent uiComponent, string uiFormName, string uiGroupName = null)
    {
        string assetName = AssetUtility.GetUIFormAsset(uiFormName);
        UIForm uiForm = null;
        if (string.IsNullOrEmpty(uiGroupName))
        {
            uiForm = uiComponent.GetUIForm(assetName);
            if (uiForm == null)
            {
                return null;
            }

            return (UIFormBase)uiForm.Logic;
        }

        IUIGroup uiGroup = uiComponent.GetUIGroup(uiGroupName);
        if (uiGroup == null)
        {
            return null;
        }

        uiForm = (UIForm)uiGroup.GetUIForm(assetName);
        if (uiForm == null)
        {
            return null;
        }

        return (UIFormBase)uiForm.Logic;
    }

    public static UIFormBase GetUIForm(this UIComponent uiComponent, int uiFormId, string uiGroupName = null)
    {
        UIForm uiForm = null;
        if (string.IsNullOrEmpty(uiGroupName))
        {
            uiForm = uiComponent.GetUIForm(uiFormId);
            if (uiForm == null)
            {
                return null;
            }

            return (UIFormBase)uiForm.Logic;
        }

        IUIGroup uiGroup = uiComponent.GetUIGroup(uiGroupName);
        if (uiGroup == null)
        {
            return null;
        }

        uiForm = (UIForm)uiGroup.GetUIForm(uiFormId);
        if (uiForm == null)
        {
            return null;
        }

        return (UIFormBase)uiForm.Logic;
    }

    public static void CloseUIForm(this UIComponent uiComponent, UIFormBase uiForm)
    {
        uiComponent.CloseUIForm(uiForm.UIForm);
    }

    public static int? TryOpenUIForm(this UIComponent uiComponent, string uiFormName, object userData = null)
    {
        UiConfig uiConfig = DataTable.instance.UIConfigTable.GetByAssetName(uiFormName);
        if (uiConfig == null)
        {
            Log.Warning("Can not load UI form '{0}' from data table.", uiFormName);
            return null;
        }
        string assetName = AssetUtility.GetUIFormAsset(uiFormName);
        if (!uiConfig.AllowMultiInstance)
        {
            if (uiComponent.IsLoadingUIForm(assetName))
            {
                return null;
            }

            if (uiComponent.HasUIForm(assetName))
            {
                return null;
            }
        }
        return uiComponent.OpenUIForm(assetName, uiConfig.UiGroupName, Constant.CorePriority.UIFormAsset, uiConfig.PauseCoveredUiForm, userData);
    }

    public static int? TryOpenUIForm(this UIComponent uiComponent, int uiFormId, object userData = null)
    {
        UiConfig uiConfig = DataTable.instance.UIConfigTable.GetById(uiFormId);
        // IDataTable<TbUIConfig> uiConfigTable = GameKitCenter.DataTable.GetDataTable<TbUIConfig>();
        if (uiConfig == null)
        {
            Log.Warning("Can not load UI form '{0}' from data table.", uiFormId);
            return null;
        }
        string assetName = AssetUtility.GetUIFormAsset(uiConfig.AssetName);
        if (!uiConfig.AllowMultiInstance)
        {
            if (uiComponent.IsLoadingUIForm(assetName))
            {
                return null;
            }

            if (uiComponent.HasUIForm(assetName))
            {
                return null;
            }
        }
        return uiComponent.OpenUIForm(assetName, uiConfig.UiGroupName, Constant.CorePriority.UIFormAsset, uiConfig.PauseCoveredUiForm, userData);
    }
}

