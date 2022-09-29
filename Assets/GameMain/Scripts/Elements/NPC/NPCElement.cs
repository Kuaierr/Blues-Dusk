using System.Reflection;
using UnityEngine;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;

[DisallowMultipleComponent]
[AddComponentMenu("BluesDusk/NPC")]
public class NPCElement : NPCElementBase
{
    [Dialog] public string Dialog = "<None>";
    public string Posture = "<None>";
    public bool CanRepeatDialog = false;
    [SerializeField] private bool m_HasDialoged;
    public override void OnInit()
    {
        base.OnInit();
        m_HasDialoged = false;
    }

    public override void OnInteract()
    {
        base.OnInteract();
        if (!m_HasDialoged)
        {
            if (Dialog != "<None>")
            {
                Vector3 middlePos = (Player.Current.transform.position + this.transform.position) / 2;
                QuickCinemachineCamera.current.SetFocus(middlePos);
                DialogSystem.current.StartDialog(Dialog);
                if (CanRepeatDialog)
                    m_HasDialoged = true;
            }
        }
    }

    private void LookAt(Transform transform)
    {

    }
}