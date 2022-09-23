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
    [UnityGameKit.Editor.Dialog] public string Dialog;
    [SerializeField] private bool m_HasDialoged = false;
    [SerializeField] private bool m_CanRepeatDialog = false;

    public override void OnInit()
    {
        base.OnInit();
        GameKitCenter.Event.Subscribe(FinishDialogCompleteEventArgs.EventId, OnDialogFinish);
        if (EnterTranform == null)
        {
            EnterTranform = transform.Find("EnterTranformation");
        }
    }
    public override void OnInteract()
    {
        base.OnInteract();
        if (Dialog != string.Empty && Dialog != "<None>")
        {
            if (!m_HasDialoged)
                DialogSystem.current.StartDialog(Dialog);
        }
        else
            Pass();
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
            EnterTranform = ((GameObject)enterTransform).transform;
        }
    }

    private void OnDialogFinish(object sender, GameKit.Event.GameEventArgs e)
    {
        FinishDialogCompleteEventArgs eventArgs = (FinishDialogCompleteEventArgs)e;
        if (eventArgs.UserData == null)
            return;
        if (eventArgs.UserData.GetType() == typeof(DialogSystem))
        {
            if (eventArgs.AssetName == Dialog)
            {
                Pass();
                if (m_CanRepeatDialog)
                    m_HasDialoged = true;
            }
        }

    }

    private void Pass()
    {
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
}