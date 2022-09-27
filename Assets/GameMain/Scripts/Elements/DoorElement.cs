using UnityEngine;
using UnityGameKit.Runtime;
using UnityEngine.Events;
using LubanConfig.DataTable;
using GameKit.Event;

[DisallowMultipleComponent]
[AddComponentMenu("BluesDusk/Door Object")]
public class DoorElement : SceneElementBase
{
    [Scene] public string TargetScene = "<None>";
    public string CanPassCondition = "<None>";
    public bool CanPass = true;
    public Transform EnterTranform;
    [Dialog] public string Dialog;
    [SerializeField] private bool m_HasDialoged = false;
    [SerializeField] private bool m_CanRepeatDialog = false;
    public UnityEvent OnInteractAfter;

    public override void OnLoad(object sender, GameEventArgs e)
    {
        base.OnLoad(sender, e);
        m_HasDialoged = GameKitCenter.Setting.GetBool(string.Format("{0}({1})", Name, "HasDialoged"), false);
        CanPass = GameKitCenter.Setting.GetBool(string.Format("{0}({1})", Name, "CanPass"), CanPass);
    }

    public override void OnSave(object sender, GameEventArgs e)
    {
        base.OnSave(sender, e);
        GameKitCenter.Setting.SetBool(string.Format("{0}({1})", Name, "HasDialoged"), m_HasDialoged);
        GameKitCenter.Setting.SetBool(string.Format("{0}({1})", Name, "CanPass"), CanPass);
    }

    public override void OnInit()
    {
        base.OnInit();
        if (CanPassCondition != "<None>" && CanPassCondition != string.Empty)
        {
            // Log.Warning(CanPassCondition);
            CanPass = false;
        }
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
            else
                Pass();
        }
        else
            Pass();
    }

    protected override void OnValidate()
    {
        base.OnValidate();
#if UNITY_EDITOR
        if (this.transform.Find("EnterTranformation") == null)
        {
            Debug.Log("Generate EnterTranformation");
            Object enterTransform = UnityEditor.AssetDatabase.LoadAssetAtPath<Object>("Assets/GameMain/Elements/Utility/EnterTranformation.prefab");
            enterTransform = UnityEditor.PrefabUtility.InstantiatePrefab(enterTransform, this.transform);
            enterTransform.name = "EnterTranformation";
            EnterTranform = ((GameObject)enterTransform).transform;
        }
#endif
    }

    private void OnDialogFinish(object sender, GameKit.Event.GameEventArgs e)
    {
        FinishDialogCompleteEventArgs eventArgs = (FinishDialogCompleteEventArgs)e;
        if (eventArgs.UserData == null)
            return;
        RefreshCondition();
        if (eventArgs.UserData.GetType() == typeof(DialogSystem))
        {
            if (eventArgs.AssetName == Dialog)
            {
                OnInteractAfter?.Invoke();
                if (!m_CanRepeatDialog && CanPass)
                {
                    m_HasDialoged = true;
                }
                Pass(); 
            }
        }
    }

    private void Pass()
    {
        if (CanPass && TargetScene != "<None>")
        {
            GameKitCenter.Procedure.ChangeSceneByDoor(TargetScene, Name);
        }
        else if (!CanPass)
        {
            Log.Warning("Door for {0} can not pass.", gameObject.name);
        }
        else if (TargetScene != "<None>")
        {
            Log.Warning("Target Scene for {0} has not set.", gameObject.name);
        }
    }

    public void RefreshCondition()
    {
        if (CanPassCondition != "<None>" || CanPassCondition != string.Empty)
            CanPass = GameSettings.current.GetBool(CanPassCondition);
    }
}