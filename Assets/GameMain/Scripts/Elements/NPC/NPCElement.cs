using System.Reflection;
using UnityEngine;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;

[DisallowMultipleComponent]
[AddComponentMenu("BluesDusk/NPC")]
public class NPCElement : NPCElementBase
{
    [Dialog] public string Dialog = "<None>";
    [Dialog] public string AfterDialog = "<None>";
    public bool IsDisposable = false;
    [SerializeField] private bool m_IsDialoged;
    public override void OnInit()
    {
        base.OnInit();
        m_IsDialoged = false;
    }

    public override void OnInteract()
    {
        base.OnInteract();
        if (!m_IsDialoged)
        {
            if (Dialog != "<None>")
            {
                Vector3 middlePos = (Player.Current.transform.position + this.transform.position) / 2;
                QuickCinemachineCamera.current.SetFocus(middlePos);
                DialogSystem.current.StartDialog(Dialog);
                if (IsDisposable)
                    m_IsDialoged = true;
            }
        }
        else
        {
            if (AfterDialog != "<None>")
            {
                Vector3 middlePos = (Player.Current.transform.position + this.transform.position) / 2;
                QuickCinemachineCamera.current.SetFocus(middlePos);
                DialogSystem.current.StartDialog(AfterDialog);
            }
        }
    }

    private void LookAt(Transform transform)
    {

    }

    private void DoDialogAnimation()
    {

    }
}