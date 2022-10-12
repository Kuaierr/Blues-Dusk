using System;
using System.Reflection;
using GameKit.Event;
using UnityEngine;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;

[DisallowMultipleComponent]
[AddComponentMenu("BluesDusk/NPC")]
public class NPCElement : NPCElementBase
{
    [Dialog]
    public string Dialog = "<None>";

    public string Posture = "<None>";
    public bool CanRepeatDialog = false;

    [SerializeField]
    private bool m_HasDialoged;

    public override void OnInit()
    {
        base.OnInit();
        m_HasDialoged = false;
        
        GameKitCenter.Event.Subscribe(FinishDialogCompleteEventArgs.EventId, OnFinishDialog);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameKitCenter.Event.Unsubscribe(FinishDialogCompleteEventArgs.EventId,OnFinishDialog);
    }

    public override void OnInteract()
    {
        base.OnInteract();
        if (!m_HasDialoged)
        {
            if (Dialog != "<None>")
            {
                Vector3 middlePos = (Player.Current.CameraFollower.position + 
                                     (this.transform.position + new Vector3(0, Player.Current.CameraFollower.position.y, 0))) / 2;
                QuickCinemachineCamera.current.SetDialogFocus(middlePos);
                DialogSystem.current.StartDialog(Dialog);
                if (CanRepeatDialog)
                    m_HasDialoged = true;
            }
        }
    }

    private void OnFinishDialog(object sender, GameEventArgs e)
    {
        FinishDialogCompleteEventArgs eventArgs = (FinishDialogCompleteEventArgs)e;
        if (eventArgs.UserData == null)
            return;
        if (eventArgs.UserData.GetType() == typeof(DialogSystem))
        {
            if (eventArgs.AssetName == Dialog)
            {   
                OnInteractAfter?.Invoke();
            }
        }
    }

    private void LookAt(Transform transform) { }
}