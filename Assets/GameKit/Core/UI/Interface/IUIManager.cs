using GameKit;
using System;
using System.Collections.Generic;

namespace GameKit.UI
{
    public interface IUIManager
    {
        int UIGroupCount { get; }
        float InstanceAutoReleaseInterval { get; set; }

        int InstanceCapacity { get; set; }

        float InstanceExpireTime { get; set; }

        int InstancePriority { get; set; }

        event EventHandler<OpenUIFormSuccessEventArgs> OpenUIFormSuccess;

        event EventHandler<OpenUIFormFailureEventArgs> OpenUIFormFailure;

        event EventHandler<CloseUIFormCompleteEventArgs> CloseUIFormComplete;

        void SetObjectPoolManager(IObjectPoolManager objectPoolManager);
        // void SetResourceManager(IResourceManager resourceManager);
        void SetUIFormHelper(IUIFormHelper uiFormHelper);

        bool HasUIGroup(string uiGroupName);

        IUIGroup GetUIGroup(string uiGroupName);

        IUIGroup[] GetAllUIGroups();

        void GetAllUIGroups(List<IUIGroup> results);

        bool AddUIGroup(string uiGroupName, IUIGroupHelper uiGroupHelper, int uiGroupDepth = 0);

        bool HasUIForm(int serialId);

        bool HasUIForm(string uiFormAssetName);

        IUIForm GetUIForm(int serialId);

        IUIForm GetUIForm(string uiFormAssetName);

        IUIForm[] GetUIForms(string uiFormAssetName);

        void GetUIForms(string uiFormAssetName, List<IUIForm> results);

        IUIForm[] GetAllLoadedUIForms();

        void GetAllLoadedUIForms(List<IUIForm> results);

        int[] GetAllLoadingUIFormSerialIds();

        void GetAllLoadingUIFormSerialIds(List<int> results);

        bool IsLoadingUIForm(int serialId);

        bool IsLoadingUIForm(string uiFormAssetName);

        bool IsValidUIForm(IUIForm uiForm);

        int OpenUIForm(string uiFormAssetName, string uiGroupName, int priority = 0, bool pauseCoveredUIForm = false, object userData = null);

        void CloseUIForm(int serialId, object userData = null);

        void CloseUIForm(IUIForm uiForm, object userData = null);

        void CloseAllLoadedUIForms(object userData = null);

        void CloseAllLoadingUIForms();

        void RefocusUIForm(IUIForm uiForm, object userData = null);

        void SetUIFormInstanceLocked(object uiFormInstance, bool locked);

        void SetUIFormInstancePriority(object uiFormInstance, int priority);
    }
}
