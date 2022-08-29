using UnityEditor;
using UnityEditor.SceneManagement;
namespace UnityGameKit.Editor
{
    public static class ScenesList
    {
        [MenuItem("Scenes/GameMain")]
        public static void Assets_GameMain_Scenes_GameMain_unity() { ScenesUpdate.OpenScene("Assets/GameMain/Scenes/GameMain.unity"); }
        [MenuItem("Scenes/Prototype_Dialog")]
        public static void Assets_GameMain_Scenes_Prototype_Dialog_unity() { ScenesUpdate.OpenScene("Assets/GameMain/Scenes/Prototype_Dialog.unity"); }
        [MenuItem("Scenes/Prototype_Dialog_DiceDev")]
        public static void Assets_GameMain_Scenes_Prototype_Dialog_DiceDev_unity() { ScenesUpdate.OpenScene("Assets/GameMain/Scenes/Prototype_Dialog_DiceDev.unity"); }
        [MenuItem("Scenes/Prototype_Dialog_UIandDice")]
        public static void Assets_GameMain_Scenes_Prototype_Dialog_UIandDice_unity() { ScenesUpdate.OpenScene("Assets/GameMain/Scenes/Prototype_Dialog_UIandDice.unity"); }
        [MenuItem("Scenes/Prototype_Interact")]
        public static void Assets_GameMain_Scenes_Prototype_Interact_unity() { ScenesUpdate.OpenScene("Assets/GameMain/Scenes/Prototype_Interact.unity"); }
        [MenuItem("Scenes/Prototype_Inventory")]
        public static void Assets_GameMain_Scenes_Prototype_Inventory_unity() { ScenesUpdate.OpenScene("Assets/GameMain/Scenes/Prototype_Inventory.unity"); }
    }
}
