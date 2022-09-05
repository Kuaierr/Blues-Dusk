using GameKit;

public static class AssetUtility
{
    public static string GetFontAsset(string assetName)
    {
        return Utility.Text.Format("Assets/GameMain/Fonts/{0}.ttf", assetName);
    }

    public static string GetSceneAsset(string assetName)
    {
        return Utility.Text.Format("Assets/GameMain/Scenes/{0}.unity", assetName);
    }

    public static string GetMusicAsset(string assetName)
    {
        return Utility.Text.Format("Assets/GameMain/Music/{0}.mp3", assetName);
    }

    public static string GetSoundAsset(string assetName)
    {
        return Utility.Text.Format("Assets/GameMain/Sounds/{0}.wav", assetName);
    }

    public static string GetEntityAsset(string assetName)
    {
        return Utility.Text.Format("Assets/GameMain/Entities/{0}.prefab", assetName);
    }

    public static string GetElementAsset(string assetName)
    {
        return Utility.Text.Format("Assets/GameMain/Elements/{0}.prefab", assetName);
    }

    public static string GetUIFormAsset(string assetName)
    {
        return Utility.Text.Format("Assets/GameMain/UI/UIForms/{0}.prefab", assetName);
    }

    public static string GetUISoundAsset(string assetName)
    {
        return Utility.Text.Format("Assets/GameMain/UI/UISounds/{0}.wav", assetName);
    }

    public static string GetDialogAsset(string assetName)
    {
        return Utility.Text.Format("Assets/GameMain/Data/Dialog/Text/Raw/{0}.txt", assetName);
    }
}

