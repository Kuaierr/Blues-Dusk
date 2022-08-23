using GameKit.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityGameKit.Runtime;

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
        return false;
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

    public static UIFormBase GetUIForm(this UIComponent uiComponent, int uiFormName, string uiGroupName = null)
    {
        return null;
    }

    public static void CloseUIForm(this UIComponent uiComponent, UIFormBase uiForm)
    {
        uiComponent.CloseUIForm(uiForm.UIForm);
    }

    public static int? OpenUIForm(this UIComponent uiComponent, string uiFormName, object userData = null)
    {
        return null;
        
        // string assetName = AssetUtility.GetUIFormAsset(uiFormName);
        // if (!drUIForm.AllowMultiInstance)
        // {
        //     if (uiComponent.IsLoadingUIForm(assetName))
        //     {
        //         return null;
        //     }

        //     if (uiComponent.HasUIForm(assetName))
        //     {
        //         return null;
        //     }
        // }

        // return uiComponent.OpenUIForm(assetName, drUIForm.UIGroupName, Constant.CorePriority.UIFormAsset, drUIForm.PauseCoveredUIForm, userData);
    }

    public static int? OpenUIForm(this UIComponent uiComponent, int uiFormName, object userData = null)
    {
        return null;
    }
}

