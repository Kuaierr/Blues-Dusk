using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityGameKit.Runtime;
using LubanConfig.DataTable;
[DisallowMultipleComponent]
[AddComponentMenu("BluesDusk/LeaveDoor Object")]
public class LeaveDoorElement : SceneElementBase
{
    public bool CanPass = true;
    public Transform EnterTranform;
    public UnityEvent OnInteractAfter;
    [SerializeField] private List<string> m_PossibleScenes;
    public override void OnInit()
    {
        base.OnInit();
        if (EnterTranform == null)
        {
            EnterTranform = transform.Find("EnterTranformation");
        }
        m_PossibleScenes = new List<string>();
        List<string> tmpScenes = new List<string>() { "S_Tower_Under", "S_Apartment_Living", "S_Bookstore_Instore", "S_Shire_Restaurant" };
        for (int i = 0; i < tmpScenes.Count; i++)
        {
            bool active = GameSettings.current.GetSceneState(tmpScenes[i]);
            if(active)
                m_PossibleScenes.Add(tmpScenes[i]);
        }
    }

    public override void OnInteract()
    {
        base.OnInteract();
        if (CanPass)
        {
            GeneralSystem.current.OpenTipUI("你想要此离开场景吗？", OpenSelectUI);
        }
    }

    private void OpenSelectUI()
    {
        GeneralSystem.current.OpenSelectSceneUI(m_PossibleScenes);
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
        }
#endif
    }
}