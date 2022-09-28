using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameKit.Runtime;
using System.Linq;
using Sirenix.Serialization;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "StageConfig", menuName = "GameMain/StageConfig", order = 0)]
public class StageConfig_SO : ScriptableObject
{
    [InfoBox("收集品实体和NPC实体采用生成策略，其余实体采用更新策略")]
    [SerializeField, ValueDropdown("GetDay"), LabelText("天数")] public int Day;
    [SerializeField, ValueDropdown("GetStage"), LabelText("时间")] public string Stage;
    [SerializeField, ValueDropdown("GetScenes"), LabelText("场景名称")] public string SceneName;
    [SerializeField, LabelText("加载实体数据")] public List<ElementData> ElementDatas;

    public string Id
    {
        get
        {
            return string.Format("{0}-{1}-{2}", Day, Stage, SceneName);
        }
    }

    public void SetDayAndStage(int day, string stage, string sceneName)
    {
        Day = day;
        Stage = stage;
        SceneName = sceneName;
    }

    public ElementData GetData(string name)
    {
        foreach (var data in ElementDatas)
        {
            if (data.Name == name)
            {
                return data;
            }
        }
        return null;
    }

    public List<ElementData> GetAll()
    {
        return ElementDatas;
    }


    public void AddData(ElementData data)
    {
        if (ElementDatas == null)
            ElementDatas = new List<ElementData>();
        ElementDatas.Add(data);
    }

    public void EditorSave()
    {

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
        return new List<string>() { "早晨", "白日", "夜晚", "凌晨" };
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

    [Button("同步至场景")]
    public void LoadToCurrentScene()
    {
        if (UnityEngine.SceneManagement.SceneManager.sceneCount < 2)
        {
            Log.Warning("同步失败，没有打开可用的场景");
            return;
        }
        GameSettings.EditorLoadElementConfig(this);
    }

    [Button("从场景同步")]
    public void SceneToConfig()
    {
        if (UnityEngine.SceneManagement.SceneManager.sceneCount < 2)
        {
            Log.Warning("同步失败，没有打开可用的场景");
            return;
        }
        GameSettings.EditorSaveElementConfig(Day, Stage);
    }
}
