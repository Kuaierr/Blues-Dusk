using UnityEditor;
using UnityEngine;
using UnityGameKit.Runtime;

namespace UnityGameKit.Editor
{
    [CustomEditor(typeof(SchedulerComponent))]
    internal sealed class SchedulerComponentInspector : GameKitInspector
    {
        private SceneInfo m_StartSceneInfo = new SceneInfo("Start");
        private SceneInfo m_LoadSceneInfo = new SceneInfo("Load");
        private SerializedProperty m_StartScene = null;
        private SerializedProperty m_LoadScene =  null;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            SchedulerComponent t = (SchedulerComponent)target;

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                m_StartSceneInfo.Draw();
                m_LoadSceneInfo.Draw();
            }
            EditorGUI.EndDisabledGroup();
            serializedObject.ApplyModifiedProperties();

            if (EditorApplication.isPlaying && IsPrefabInHierarchy(t.gameObject))
            {
                EditorGUILayout.LabelField("Current Scenes", GetSceneNameString(t.GetLoadedSceneAssetNames()));
                Repaint();
            }
        }

        private void OnEnable()
        {
            m_StartSceneInfo.Init(serializedObject);
            m_LoadSceneInfo.Init(serializedObject);
            RefreshTypeNames();
        }

        private void RefreshTypeNames()
        {
            m_StartSceneInfo.Refresh();
            m_LoadSceneInfo.Refresh();
            serializedObject.ApplyModifiedProperties();
        }

        private string GetSceneNameString(string[] sceneAssetNames)
        {
            if (sceneAssetNames == null || sceneAssetNames.Length <= 0)
            {
                return "<Empty>";
            }

            string sceneNameString = string.Empty;
            foreach (string sceneAssetName in sceneAssetNames)
            {
                if (!string.IsNullOrEmpty(sceneNameString))
                {
                    sceneNameString += ", ";
                }

                sceneNameString += SceneComponent.GetSceneName(sceneAssetName);
            }
            return sceneNameString;
        }
    }
}
