using UnityEditor;
using UnityEngine;
using UnityGameKit.Runtime;

namespace UnityGameKit.Editor
{
    [CustomEditor(typeof(DialogComponent))]
    internal sealed class DialogComponentInspector : GameKitInspector
    {
        private HelperInfo<DialogTreePharseHelperBase> m_DialogHelperInfo = new HelperInfo<DialogTreePharseHelperBase>("DialogTreePharse");
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            DialogComponent t = (DialogComponent)target;

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                m_DialogHelperInfo.Draw();
            }

            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();

            if (EditorApplication.isPlaying && IsPrefabInHierarchy(t.gameObject))
            {
                EditorGUILayout.LabelField("Loaded Dialog Asset Names", GetDialogNameString(t.GetLoadedDialogAssetNames()));
                // EditorGUILayout.LabelField("Loading Dialog Asset Names", GetDialogNameString(t.GetLoadingDialogAssetNames()));
                // EditorGUILayout.LabelField("Unloading Dialog Asset Names", GetDialogNameString(t.GetUnloadingDialogAssetNames()));
                Repaint();
            }
        }

        private void OnEnable()
        {
            m_DialogHelperInfo.Init(serializedObject);
            RefreshTypeNames();
        }

        private void RefreshTypeNames()
        {
            m_DialogHelperInfo.Refresh();
            serializedObject.ApplyModifiedProperties();
        }

        private string GetDialogNameString(string[] dialogAssetNames)
        {
            if (dialogAssetNames == null || dialogAssetNames.Length <= 0)
            {
                return "<Empty>";
            }

            string dialogNameString = string.Empty;
            foreach (string dialogAssetName in dialogAssetNames)
            {
                if (!string.IsNullOrEmpty(dialogNameString))
                {
                    dialogNameString += ", ";
                }

                dialogNameString += dialogAssetName;
            }

            return dialogNameString;
        }
    }
}
