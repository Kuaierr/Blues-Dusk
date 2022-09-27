using UnityEngine;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;
using UnityEngine.Events;
[DisallowMultipleComponent]
[AddComponentMenu("BluesDusk/Custom Object")]
public class CustomElement : SceneElementBase
{
    [Dialog] public string Dialog;
    public bool m_CanRepeatDialog = false;
    [SerializeField] private bool m_HasDialoged;
    public UnityEvent OnInteractAfter;

    public override void OnInit()
    {
        base.OnInit();
        m_HasDialoged = false;
        GameKitCenter.Event.Subscribe(FinishDialogCompleteEventArgs.EventId, OnDialogFinish);
    }

    public override void OnInteract()
    {
        if (Dialog != string.Empty && Dialog != "<None>" && !m_HasDialoged)
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
                if (m_CanRepeatDialog)
                    m_HasDialoged = true;
            }
        }
    }
}