using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameKit;
using UnityGameKit.Runtime;
using Sirenix.OdinInspector;
using System.IO;

[CreateAssetMenu(fileName = "StageConfigPool", menuName = "GameMain/StageConfigPool", order = 0)]
public class StageConfigPool_SO : PoolBase_SO
{
    [SerializeField] private List<StageConfig_SO> m_Configs;
    public override StageConfig_SO GetData<StageConfig_SO>(string name)
    {
        foreach (var config in m_Configs)
        {
            Log.Info(config.Id.Correction());
            if (config.Id.Correction() == name.Correction())
            {
                return config as StageConfig_SO;
            }
        }
        Log.Fail("Can not find data {0} for {1}.", name, this.name);
        return null;
    }

    public override StageConfig_SO GetData<StageConfig_SO>(int id)
    {
        Log.Fail("GetData by id for {0} is no implement yet.", this.name);
        return null;
    }

#if UNITY_EDITOR
    [Button("加载场景元素配置")]
    public void LoadAllConfig()
    {
        m_Configs = new List<StageConfig_SO>();
        foreach (string assetGuid in UnityEditor.AssetDatabase.FindAssets("t:StageConfig_SO", new string[] { AssetUtility.ElementConfigPath + "/Configs" }))
        {
            string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(assetGuid);
            StageConfig_SO config = UnityEditor.AssetDatabase.LoadAssetAtPath<StageConfig_SO>(assetPath);
            m_Configs.Add(config);
        }
    }

    [Button("清空场景元素配置")]
    public void ClearAllConfig()
    {
        if (m_Configs != null)
            m_Configs.Clear();
    }

    [Button("保存配置为JSON")]
    public void SaveConfigToJson()
    {
        if (m_Configs != null)
        {
            foreach (var config in m_Configs)
            {
                string strData = JsonUtility.ToJson(config, true);
                string path = AssetUtility.ElementConfigPath + "/Json/" + config.name + ".json";
                // Debug.Log(strData);
                // TextAsset asset = new TextAsset(strData);
                if (File.Exists(path))
                    File.Delete(path);
                File.WriteAllText(path, strData);
            }
            // UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
        }
    }

    [Button("从JSON中加载配置")]
    public void LoadConfigFromJson()
    {
        if (m_Configs == null)
            m_Configs = new List<StageConfig_SO>();


        foreach (string assetGuid in UnityEditor.AssetDatabase.FindAssets("t:TextAsset", new string[] { AssetUtility.ElementConfigPath + "/Json" }))
        {
            string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(assetGuid);
            TextAsset textAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(assetPath);
            // StageConfig_SO config = UnityEngine.ScriptableObject.CreateInstance<StageConfig_SO>();
            // m_Configs.Add(config); 
        }
    }
# endif
}
