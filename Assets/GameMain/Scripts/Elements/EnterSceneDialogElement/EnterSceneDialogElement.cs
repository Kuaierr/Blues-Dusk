using UnityEngine;
using UnityGameKit.Runtime;
using UnityEngine.Events;
using System.Collections;
using GameKit.Event;

[DisallowMultipleComponent]
[AddComponentMenu("BluesDusk/Enter Scene Object")]
public class EnterSceneDialogElement : SceneElementBase
{
    [Dialog] public string Dialog;
    [SerializeField] private float waitTimeForDialog = 0.5f;

    //saved information
    [SerializeField]public bool hasSpeaked = false;


    private bool canRepeatDialog = false;

    public override void OnLoad(object sender, GameEventArgs e)
    {
        base.OnLoad(sender, e);
        hasSpeaked = GameKitCenter.Setting.GetBool(string.Format("{0}({1})", Name, "hasSpeaked"), false);
    }

    public override void OnSave(object sender, GameEventArgs e)
    {
        base.OnSave(sender, e);
        GameKitCenter.Setting.SetBool(string.Format("{0}({1})", Name, "hasSpeaked"), hasSpeaked);
    }

    public override void OnInit()
    {
        base.OnInit();
        GameKitCenter.Event.Subscribe(FinishDialogCompleteEventArgs.EventId, OndialogFinish);
        StartCoroutine(WaitAndStartDialog());
    }

    public void OndialogFinish(object sender, GameKit.Event.GameEventArgs e)
    {
        FinishDialogCompleteEventArgs eventArgs = (FinishDialogCompleteEventArgs)e;
        if(eventArgs.UserData == null)
            return;
        if (eventArgs.UserData.GetType() == typeof(DialogSystem))
        {
            if(eventArgs.AssetName == Dialog)
            {
                OnInteractAfter?.Invoke();
                if (!canRepeatDialog)
                {
                    hasSpeaked = true;
                    StopCoroutine(WaitAndStartDialog());
                }
            }
        }
    }

    private IEnumerator WaitAndStartDialog()
    {
        for(float i = 0; i < 0.1f+ waitTimeForDialog; i += Time.deltaTime)
        {
            yield return null;
        }

        if(Dialog!=string.Empty && Dialog != "<None>")
        {
            if (!hasSpeaked)
                DialogSystem.current.StartDialog(Dialog);
        }
    }
}
