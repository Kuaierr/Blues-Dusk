using UnityEngine;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;
[DisallowMultipleComponent]
[AddComponentMenu("BluesDusk/Door Object")]
public class DoorElement : SceneElementBase
{
    [UnityGameKit.Editor.Scene] public string TargetScene = "<None>";
    public bool CanPass = true;
    public Transform EnterTranform;
    public override void OnInteract()
    {
        base.OnInteract();
        if (CanPass && TargetScene != "<None>")
        {
            GameKitCenter.Procedure.ChangeSceneByDoor(TargetScene, Name);
        }
        else if (TargetScene != "<None>")
        {
            Log.Fail("Target Scene for {0} has not set.", gameObject.name);
        }
        else
        {
            Log.Fail("Door for {0} can not pass.", gameObject.name);
        }
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        if(this.transform.Find("EnterTranformation") == null)
        {
            Debug.Log("Generate EnterTranformation");
            Object enterTransform = UnityEditor.AssetDatabase.LoadAssetAtPath<Object>("Assets/GameMain/Elements/Utility/EnterTranformation.prefab");
            enterTransform = UnityEditor.PrefabUtility.InstantiatePrefab(enterTransform, this.transform);
            enterTransform.name = "EnterTranformation";
        }
    }
}