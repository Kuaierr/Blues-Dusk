using UnityEditor;
using UnityEngine;
using UnityGameKit.Runtime;

namespace UnityGameKit.Editor
{
    [CustomEditor(typeof(InventoryComponent))]
    internal sealed class InventoryComponentInspector : GameKitInspector
    {
        private HelperInfo<InventoryHelperBase> m_InventoryHelperInfo = new HelperInfo<InventoryHelperBase>("Inventory");
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            InventoryComponent t = (InventoryComponent)target;

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                m_InventoryHelperInfo.Draw();
            }

            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();

            if (EditorApplication.isPlaying && IsPrefabInHierarchy(t.gameObject))
            {
                // EditorGUILayout.LabelField("Loaded Inventory Asset Names", GetInventoryNameString(t.GetLoadedInventoryAssetNames()));
                // EditorGUILayout.LabelField("Loading Inventory Asset Names", GetInventoryNameString(t.GetLoadingInventoryAssetNames()));
                // EditorGUILayout.LabelField("Unloading Inventory Asset Names", GetInventoryNameString(t.GetUnloadingInventoryAssetNames()));
                Repaint();
            }
        }

        private void OnEnable()
        {
            m_InventoryHelperInfo.Init(serializedObject);
            RefreshTypeNames();
        }

        private void RefreshTypeNames()
        {
            m_InventoryHelperInfo.Refresh();
            serializedObject.ApplyModifiedProperties();
        }

        private string GetInventoryNameString(string[] inventoryAssetNames)
        {
            if (inventoryAssetNames == null || inventoryAssetNames.Length <= 0)
            {
                return "<Empty>";
            }

            string inventoryNameString = string.Empty;
            foreach (string inventoryAssetName in inventoryAssetNames)
            {
                if (!string.IsNullOrEmpty(inventoryNameString))
                {
                    inventoryNameString += ", ";
                }

                inventoryNameString += inventoryAssetName;
            }

            return inventoryNameString;
        }
    }
}
