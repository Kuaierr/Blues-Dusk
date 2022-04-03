#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
public static class ScenesList
{
    [MenuItem("Scenes/Demo")]
    public static void Assets_Extenals_Modern_UI_Pack_Scenes_Demo_unity() { ScenesUpdate.OpenScene("Assets/Extenals/Modern UI Pack/Scenes/Demo.unity"); }
    [MenuItem("Scenes/Demo_UI")]
    public static void Assets_GameKit_Core_UGUI_Demo_Demo_UI_unity() { ScenesUpdate.OpenScene("Assets/GameKit/Core/UGUI/Demo/Demo_UI.unity"); }
    [MenuItem("Scenes/Demo_TopdownMove")]
    public static void Assets_GameKit_Prototype_CharacterControl_TopdownMove_Scene_Demo_TopdownMove_unity() { ScenesUpdate.OpenScene("Assets/GameKit/Prototype/CharacterControl/TopdownMove/Scene/Demo_TopdownMove.unity"); }
    [MenuItem("Scenes/Prototype")]
    public static void Assets_GameMain_Scenes_Prototype_unity() { ScenesUpdate.OpenScene("Assets/GameMain/Scenes/Prototype.unity"); }
}
#endif
