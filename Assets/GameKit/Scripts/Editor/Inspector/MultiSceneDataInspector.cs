using UnityEditor;
using UnityEngine;
using UnityGameKit.Runtime;

namespace UnityGameKit.Editor
{
    [CustomEditor(typeof(SceneSO))]
    internal sealed class MultiSceneDataInspector : GameKitInspector
    {
        private SceneInfo m_SceneInfo = new SceneInfo("SceneName");
        private EnumInfo m_SceneTypeInfo = new EnumInfo("SceneType");
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            SceneSO t = (SceneSO)target;
            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                m_SceneTypeInfo.Draw();
                m_SceneInfo.Draw();
            }
            EditorGUI.EndDisabledGroup();
            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            m_SceneInfo.Init(serializedObject);
            m_SceneTypeInfo.Init(serializedObject);
            RefreshTypeNames();
        }

        private void RefreshTypeNames()
        {
            m_SceneInfo.Refresh();
            m_SceneTypeInfo.Refresh();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
