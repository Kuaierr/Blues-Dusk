using UnityEngine;
using System.Collections.Generic;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;
[DisallowMultipleComponent]
[AddComponentMenu("BluesDusk/LeaveDoor Object")]
public class LeaveDoorElement : SceneElementBase
{
    [UnityGameKit.Editor.Scene] public string TargetScene = "<None>";
    public bool CanPass = true;
    public Transform EnterTranform;
    public override void OnInteract()
    {
        base.OnInteract();
        if (CanPass && TargetScene != "<None>")
        {
            GeneralSystem.current.OpenTipUI("你想要此离开场景吗？", OpenSelectUI);
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


    private void ChangeScene()
    {
        GameKitCenter.Procedure.ChangeSceneBySelect(TargetScene);
    }

    private void OpenSelectUI()
    {
        GeneralSystem.current.OpenSelectSceneUI(new List<string>() { "S_Parlor", "S_Tower", "S_FengHospital" });
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        if (this.transform.Find("EnterTranformation") == null)
        {
            Debug.Log("Generate EnterTranformation");
            Object enterTransform = UnityEditor.AssetDatabase.LoadAssetAtPath<Object>("Assets/GameMain/Elements/Utility/EnterTranformation.prefab");
            enterTransform = UnityEditor.PrefabUtility.InstantiatePrefab(enterTransform, this.transform);
            enterTransform.name = "EnterTranformation";
        }
    }
}