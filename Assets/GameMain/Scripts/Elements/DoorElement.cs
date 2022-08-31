using UnityEngine;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;

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
            Log.Warning("Target Scene for {0} has not set.", gameObject.name);
        }
        else
        {
            Log.Warning("Door for {0} can not pass.", gameObject.name);
        }
    }
}