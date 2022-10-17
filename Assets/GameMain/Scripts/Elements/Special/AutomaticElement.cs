using UnityEngine;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;
using UnityEngine.Events;

[DisallowMultipleComponent]
[AddComponentMenu("BluesDusk/Automatic Element")]
[RequireComponent(typeof(BoxCollider))]
public class AutomaticElement : MonoBehaviour
{
    [Dialog] public string Dialog;
    public bool HasDialoged;
    [SerializeField] private BoxCollider m_DetectColl;

    void Start()
    {
        HasDialoged = false;
        GameKitCenter.Event.Subscribe(FinishDialogCompleteEventArgs.EventId, OnDialogFinish);
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
                HasDialoged = true;
                m_DetectColl.enabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Log.Warning(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            if (Dialog != string.Empty && Dialog != "<None>" && !HasDialoged)
            {
                // Vector3 middlePos = (Player.Current.transform.position + this.transform.position) / 2;
                QuickCinemachineCamera.current.SetFocus(Player.Current.transform.position, false);
                DialogSystem.current.StartDialog(Dialog);
            }
        }
    }
    private void OnValidate()
    {
        m_DetectColl = GetComponent<BoxCollider>();
    }
}