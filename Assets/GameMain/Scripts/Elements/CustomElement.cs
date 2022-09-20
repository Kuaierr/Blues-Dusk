using UnityEngine;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;
[DisallowMultipleComponent]
[AddComponentMenu("BluesDusk/Custom Object")]
public class CustomElement : SceneElementBase
{
    [UnityGameKit.Editor.Dialog] public string Dialog;
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
            DialogSystem.current.StartDialog(Dialog);
            if (IsDisposable)
                m_IsDialoged = true;
        }

        base.OnInteract();
    }
}