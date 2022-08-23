using System;
using GameKit;
using UnityGameKit.Runtime;

public static class EntityExtension
{
    private const string ENTITY_PATH = "Assets/GameMain/Prefabs/{0}.prefab";
    private const string DEMO_ENTITY_PATH = "Assets/GameKit/Entity/Demo/Reousrce/Prefab/{0}.prefab";
    public static void ShowEntity(this EntityComponent entityComponent, Type logicType, string AssetName, string entityGroup, int priority, EntityData data)
    {
        if (data == null)
        {
            Utility.Debugger.LogWarning("Data is invalid.");
            return;
        }
        entityComponent.ShowEntity(data.Id, logicType, GetEntityAsset(AssetName), entityGroup, priority, data);
    }

    public static void ShowEntityDemo(this EntityComponent entityComponent, Type logicType, string AssetName, string entityGroup, int priority, EntityData data)
    {
        if (data == null)
        {
            Utility.Debugger.LogWarning("Data is invalid.");
            return;
        }
        entityComponent.ShowEntity(data.Id, logicType, GetEntityAssetDemo(AssetName), entityGroup, priority, data);
    }
    public static string GetEntityAsset(string assetName) => string.Format(ENTITY_PATH, assetName);
    public static string GetEntityAssetDemo(string assetName) => string.Format(DEMO_ENTITY_PATH, assetName);
}

