using System;
using UnityEngine;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;
using UnityEngine.Events;
[DisallowMultipleComponent]
[AddComponentMenu("BluesDusk/Custom Object")]
public class CustomElement : SceneElementBase
{
    [Dialog] public string Dialog;
    public bool CanRepeatDialog = false;
    public bool HasDialoged;

    public override void OnInit()
    {
        base.OnInit();
        HasDialoged = false;
        GameKitCenter.Event.Subscribe(FinishDialogCompleteEventArgs.EventId, OnDialogFinish);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameKitCenter.Event.Unsubscribe(FinishDialogCompleteEventArgs.EventId,OnDialogFinish);
    }

    public override void OnInteract()
    {
        if (Dialog != string.Empty && Dialog != "<None>" && !HasDialoged)
        {
            Vector3 middlePos = (Player.Current.transform.position + this.transform.position) / 2;
            QuickCinemachineCamera.current.SetFocus(middlePos);
            DialogSystem.current.StartDialog(Dialog);
        }
        base.OnInteract();
    }

    private void OnDialogFinish(object sender, GameKit.Event.GameEventArgs e)
    {
        FinishDialogCompleteEventArgs eventArgs = (FinishDialogCompleteEventArgs)e;
        if (eventArgs.UserData == null)
            return;
        if (eventArgs.UserData.GetType() == typeof(DialogSystem))
        {
            if (eventArgs.AssetName == Dialog)
            {   
                OnInteractAfter?.Invoke();
                HasDialoged = true;
                if (CanRepeatDialog)
                    HasDialoged = false;
            }
        }
    }
    
    
}