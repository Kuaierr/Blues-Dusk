using GameKit.Event;
using UnityGameKit.Runtime;

public class DialogSystem : MonoSingletonBase<DialogSystem>
{
    private void Start()
    {
        GameKitCenter.Event.Subscribe(StartDialogSuccessEventArgs.EventId, OnStartDialogSuccess);
    }
    public void StartDialog(string dialogName)
    {
        string dialogAssetName = AssetUtility.GetDialogAsset(dialogName);
        GameKitCenter.Dialog.StartDialog(dialogAssetName, userData: this);
    }

    public void StartDialog(string dialogName, string dialogContent)
    {
        string dialogAssetName = AssetUtility.GetDialogAsset(dialogName);
        GameKitCenter.Dialog.StartDialog(dialogAssetName, dialogContent, userData: this);
    }

    private void OnStartDialogSuccess(object sender, GameEventArgs e)
    {
        // Log.Success("DialogSystem detect dialog started.");
        StartDialogSuccessEventArgs args = (StartDialogSuccessEventArgs)e;
        if (args == null || args.UserData == null)
        {
            Log.Warning("UserData is null.");
            return;
        }
        if (args.UserData.GetType() != typeof(DialogSystem))
            return;
            
        GameKitCenter.UI.TryOpenUIForm("UI_Dialog", this);
    }
}