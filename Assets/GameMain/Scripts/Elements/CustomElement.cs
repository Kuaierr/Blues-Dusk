using UnityEngine;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;
[DisallowMultipleComponent]
[AddComponentMenu("BluesDusk/Custom Object")]
public class CustomElement : SceneElementBase
{
    [UnityGameKit.Editor.Dialog] public string DefaultDialog;
    public bool IsDialogDisposable;
    public override void OnInteract()
    {
        if (DefaultDialog != string.Empty && DefaultDialog != "<None>")
        {

        }

        base.OnInteract();
    }
}