using System.Collections.Generic;

namespace GameKit.UI
{
    public interface IUIGroup
    {
        string Name { get; }

        int Depth { get; set; }

        bool Pause { get; set; }

        int UIFormCount { get; }

        IUIForm CurrentUIForm { get; }

        IUIGroupHelper Helper { get; }

        bool HasUIForm(int serialId);

        bool HasUIForm(string uiFormAssetName);

        IUIForm GetUIForm(int serialId);

        IUIForm GetUIForm(string uiFormAssetName);

        IUIForm[] GetUIForms(string uiFormAssetName);

        void GetUIForms(string uiFormAssetName, List<IUIForm> results);

        IUIForm[] GetAllUIForms();

        void GetAllUIForms(List<IUIForm> results);
    }
}
