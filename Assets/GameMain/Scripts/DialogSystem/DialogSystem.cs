using UnityEngine;
using UnityGameKit.Runtime;

public class DialogSystem : MonoSingletonBase<DialogSystem>
{
    public void StartDialog(string dialogName)
    {
        GameKitCenter.UI.TryOpenUIForm("UI_Dialog");
        string dialogAssetName = AssetUtility.GetDialogAsset(dialogName);
        GameKitCenter.Dialog.StartDialog(dialogAssetName);
    }

    public void StartDialog(string dialogName, string dialogContent)
    {
        GameKitCenter.UI.TryOpenUIForm("UI_Dialog");
        GameKitCenter.Dialog.StartDialog(dialogName, dialogContent);
    }
}