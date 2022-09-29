using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameKit.Runtime;
using System.Linq;
using Sirenix.Serialization;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "ElementConfigCreator", menuName = "GameMain/ElementConfigCreator", order = 0)]
public class ElementConfigCreatorSO : ScriptableObject
{
    [SerializeField, ValueDropdown("GetDay"), LabelText("天数")] public int Day;
    [SerializeField, ValueDropdown("GetStage"), LabelText("时间")] public int Stage;
    [SerializeField, ValueDropdown("GetConfigs"), LabelText("当前配置")] public StageConfig_SO CurrentConfig;


    [Button("同步至场景")]
    public void LoadToCurrentScene()
    {
        if (UnityEngine.SceneManagement.SceneManager.sceneCount < 2)
        {
            Log.Warning("同步失败，没有打开可用的场景");
            return;
        }
        if (CurrentConfig != null)
            GameSettings.EditorLoadElementConfig(CurrentConfig);
    }

    [Button("从场景保存")]
    public void SceneToConfig()
    {
        if (UnityEngine.SceneManagement.SceneManager.sceneCount < 2)
        {
            Log.Warning("同步失败，没有打开可用的场景");
            return;
        }
        GameSettings.EditorSaveElementConfig(Day, Stage);
    }

    public IEnumerable GetConfigs()
    {
        foreach (string assetGuid in UnityEditor.AssetDatabase.FindAssets("t:StageConfig_SO", new string[] { AssetUtility.ElementConfigPath + "/Configs" }))
        {
            string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(assetGuid);
            StageConfig_SO config = UnityEditor.AssetDatabase.LoadAssetAtPath<StageConfig_SO>(assetPath);
            yield return config;
        }
    }

    private IEnumerable GetDay()
    {
        for (int i = 1; i <= 30; i++)
        {
            yield return i;
        }
    }

    private IEnumerable GetStage()
    {
        for (int i = 1; i <= 4; i++)
        {
            yield return i;
        }
    }
}
