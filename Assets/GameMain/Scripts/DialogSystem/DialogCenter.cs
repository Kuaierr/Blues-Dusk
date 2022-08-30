using UnityEngine;
using UnityGameKit.Runtime;

public class DialogCenter : MonoSingletonBase<DialogCenter>
{
    public void StartDialog(string dialogName)
    {
        GameKitCenter.UI.TryOpenUIForm("UI_Dialog");
        GameKitCenter.Dialog.StartDialog(dialogName);
    }

    public void StartDialog(string dialogName, string dialogContent)
    {
        GameKitCenter.UI.TryOpenUIForm("UI_Dialog");
        GameKitCenter.Dialog.StartDialog(dialogName, dialogContent);
    }
}