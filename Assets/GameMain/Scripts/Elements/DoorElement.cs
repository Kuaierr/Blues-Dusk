using UnityEngine;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;
[DisallowMultipleComponent]
[AddComponentMenu("BluesDusk/Door Object")]
public class DoorElement : GameElementBase
{
    public bool CanPass = true;
    public string TargetScene = "<None>";
    public override void OnInteract()
    {
        base.OnInteract();
        if (CanPass && TargetScene != "<None>")
        {

        }
        else if(TargetScene != "<None>")
        {
            Log.Fail("Target Scene for {0} has not set.", gameObject.name);
        }
        else
        {
            Log.Fail("Door for {0} can not pass.", gameObject.name);
        }
    }
}