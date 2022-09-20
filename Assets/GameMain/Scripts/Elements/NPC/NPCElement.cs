using UnityEngine;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;

[DisallowMultipleComponent]
[AddComponentMenu("BluesDusk/NPC")]
public class NPCElement : NPCElementBase
{
    [UnityGameKit.Editor.Dialog] public string Dialog = "<None>";
    [UnityGameKit.Editor.Dialog] public string AfterDialog = "<None>";
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
                DialogSystem.current.StartDialog(Dialog);
                if (IsDisposable)
                    m_IsDialoged = true;
            }
        }
        else
        {
            if (AfterDialog != "<None>")
            {
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