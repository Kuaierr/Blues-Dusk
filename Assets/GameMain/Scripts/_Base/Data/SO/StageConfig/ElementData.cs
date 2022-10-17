using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

[System.Serializable]
public class ElementData
{
    [LabelText("名称"), DisplayAsString] public string Name = null;
    [LabelText("互动位置"), DisplayAsString] public Vector3 DestinationPosition;
    [LabelText("位置"), DisplayAsString] public Vector3 Position;
    [LabelText("方向"), DisplayAsString] public Vector3 Rotation;
    [LabelText("类型名"), DisplayAsString] public string ElementType;
    [LabelText("预制体"), ShowIf("@ElementType == \"NPCElement\" || ElementType == \"CollectElement\"")] public GameElementBase Prefab;

    [ShowIf("@ElementType == \"NPCElement\""), ValueDropdown("GetDialogs"), LabelText("NPC-姿势")]
    public string NPC_Posture = null;
    [ShowIf("@ElementType == \"NPCElement\""), ValueDropdown("GetPostures"), LabelText("NPC-对话")]
    public string NPC_Dialog = null;
    [ShowIf("@ElementType == \"CollectElement\""), ValueDropdown("GetDialogs"), LabelText("物品-拾取前感想")]
    public string Item_Dialog;
    [ShowIf("@ElementType == \"DoorElement\""), LabelText("门-默认可通过")]
    public bool Door_CanPass = false;
    [ShowIf("@ElementType == \"DoorElement\""), LabelText("门-通过条件")]
    public string Door_CanPassCondition = null;
    [ShowIf("@ElementType == \"DoorElement\""), ValueDropdown("GetScenes"), LabelText("门-目标场景")]
    public string Door_TargetScene = null;
    [ShowIf("@ElementType == \"CustomElement\""), ValueDropdown("GetDialogs"), LabelText("自定义实体-对话名称")]
    public string Custom_Dialog;
    [ShowIf("@ElementType == \"CustomElement\""), LabelText("自定义实体-可重复对话")]
    public bool Custom_CanRepeatDialog = false;
    [ShowIf("@ElementType == \"CustomElement\""), LabelText("自定义实体-已对话")]
    public bool Custom_HasDialoged;
    [ShowIf("@ElementType == \"EnterSceneDialogElement\""), LabelText("开场对话-是否对话过")]
    public bool EnterSceneDialog_HasSpeaked;

    public UnityEvent onInteractBeginEvent;
    public UnityEvent onInteractAfterEvent;

#if UNITY_EDITOR
    private IEnumerable GetDialogs()
    {
        TextAsset textAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/GameMain/Configs/DialogCollection.txt");
        string[] splits = textAsset.text.Split(',');
        for (int i = 0; i < splits.Length; i++)
        {
            yield return splits[i];
        }
    }

    private IEnumerable GetScenes()
    {
        foreach (string sceneGuid in UnityEditor.AssetDatabase.FindAssets("t:Scene", new string[] { ScenesConfig.GAMEMAIN_PATH }))
        {
            string scenePath = UnityEditor.AssetDatabase.GUIDToAssetPath(sceneGuid);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath); 
            yield return sceneName;
        }
    }
#endif
    private IEnumerable GetPostures()
    {
        return new List<string>() { "站立", "正坐", "躺着" };
    }

}