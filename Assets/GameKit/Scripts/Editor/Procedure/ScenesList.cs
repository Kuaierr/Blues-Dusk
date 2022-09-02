using UnityEditor;
using UnityEditor.SceneManagement;
namespace UnityGameKit.Editor
{
    public static class ScenesList
    {
        [MenuItem("Scenes/GameChangeScene")]
        public static void Assets_GameMain_Scenes_GameChangeScene_unity() { ScenesUpdate.OpenScene("Assets/GameMain/Scenes/GameChangeScene.unity"); }
        [MenuItem("Scenes/GameMenu")]
        public static void Assets_GameMain_Scenes_GameMenu_unity() { ScenesUpdate.OpenScene("Assets/GameMain/Scenes/GameMenu.unity"); }
        [MenuItem("Scenes/Prototype_Dialog")]
        public static void Assets_GameMain_Scenes_Prototype_Prototype_Dialog_unity() { ScenesUpdate.OpenScene("Assets/GameMain/Scenes/Prototype/Prototype_Dialog.unity"); }
        [MenuItem("Scenes/Prototype_Dialog_UIandDice")]
        public static void Assets_GameMain_Scenes_Prototype_Prototype_Dialog_UIandDice_unity() { ScenesUpdate.OpenScene("Assets/GameMain/Scenes/Prototype/Prototype_Dialog_UIandDice.unity"); }
        [MenuItem("Scenes/Prototype_Interact")]
        public static void Assets_GameMain_Scenes_Prototype_Prototype_Interact_unity() { ScenesUpdate.OpenScene("Assets/GameMain/Scenes/Prototype/Prototype_Interact.unity"); }
        [MenuItem("Scenes/Prototype_Inventory")]
        public static void Assets_GameMain_Scenes_Prototype_Prototype_Inventory_unity() { ScenesUpdate.OpenScene("Assets/GameMain/Scenes/Prototype/Prototype_Inventory.unity"); }
        [MenuItem("Scenes/Prototype_Dialog_DiceDev")]
        public static void Assets_GameMain_Scenes_Prototype_Dialog_DiceDev_unity() { ScenesUpdate.OpenScene("Assets/GameMain/Scenes/Prototype_Dialog_DiceDev.unity"); }
        [MenuItem("Scenes/S_Parlor")]
        public static void Assets_GameMain_Scenes_S_Parlor_unity() { ScenesUpdate.OpenScene("Assets/GameMain/Scenes/S_Parlor.unity"); }
        [MenuItem("Scenes/S_Toilet")]
        public static void Assets_GameMain_Scenes_S_Toilet_unity() { ScenesUpdate.OpenScene("Assets/GameMain/Scenes/S_Toilet.unity"); }
    }
}
