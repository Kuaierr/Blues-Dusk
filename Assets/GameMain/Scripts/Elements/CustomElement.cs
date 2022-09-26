using UnityEngine;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;
[DisallowMultipleComponent]
[AddComponentMenu("BluesDusk/Custom Object")]
public class CustomElement : SceneElementBase
{
    public string Dialog;
    public bool IsDisposable = false;
    [SerializeField] private bool m_IsDialoged;

    public override void OnInit()
    {
        base.OnInit();
        m_IsDialoged = false;
    }

    public override void OnInteract()
    {
        if (Dialog != string.Empty && Dialog != "<None>" && !m_IsDialoged)
        {
            Vector3 middlePos = (Player.Current.transform.position + this.transform.position) / 2;
            QuickCinemachineCamera.current.SetFocus(middlePos);
            DialogSystem.current.StartDialog(Dialog);
            if (IsDisposable)
                m_IsDialoged = true;
        }

        base.OnInteract();
    }
}