using UnityEngine;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;

[DisallowMultipleComponent]
[AddComponentMenu("BluesDusk/NPC")]
public class NPCElement : GameElementBase
{
    public TextAsset DialogAsset;
    public string DialogName = "<None>";

    public override void OnInit()
    {
        base.OnInit();
    }
    public override void OnInteract()
    {
        base.OnInteract();
        if (DialogName != "<None>")
        {
            DialogSystem.current.StartDialog(DialogName);
        }
        else
        {
            Log.Fail("Dialog Name for {0} is not set correctly.", gameObject.name);
        }

    }

    private void LookAt(Transform transform)
    {

    }

    private void DoDialogAnimation()
    {

    }
}