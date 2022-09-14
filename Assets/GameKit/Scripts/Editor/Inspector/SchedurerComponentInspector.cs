using UnityEditor;
using UnityEngine;
using System.IO;
using UnityGameKit.Runtime;

namespace UnityGameKit.Editor
{
    [CustomEditor(typeof(SchedulerComponent))]
    internal sealed class SchedulerComponentInspector : GameKitInspector
    {
        private const string ImportPath = "m_ScenesPath";
        private const string ExportPath = "m_MultiScenesPath";
        private SceneInfo m_StartSceneInfo = new SceneInfo("StartScene");
        private SceneInfo m_LoadSceneInfo = new SceneInfo("LoadScene");
        private PathInfo m_ImportPathInfo = new PathInfo(ImportPath);
        private PathInfo m_ExportPathInfo = new PathInfo(ExportPath);

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            SchedulerComponent t = (SchedulerComponent)target;

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                m_StartSceneInfo.Draw();
                m_LoadSceneInfo.Draw();
                m_ImportPathInfo.Draw();
                m_ExportPathInfo.Draw();
                EditorGUILayout.Space();
                if (GUILayout.Button("Build Scenes to Multi-Scenes"))
                {
                    BuildScenes();
                }
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
            m_ImportPathInfo.Init(serializedObject);
            m_ExportPathInfo.Init(serializedObject);
            Refresh();
        }

        private void Refresh()
        {
            m_StartSceneInfo.Refresh();
            m_LoadSceneInfo.Refresh();
            m_ImportPathInfo.Refresh();
            m_ExportPathInfo.Refresh();
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

        private void BuildScenes()
        {
            string importPath = serializedObject.FindProperty(ImportPath).stringValue;
            string exportPath = serializedObject.FindProperty(ExportPath).stringValue;
            foreach (string sceneGuid in AssetDatabase.FindAssets("t:Scene", new string[] { importPath }))
            {
                string scenePath = AssetDatabase.GUIDToAssetPath(sceneGuid);
                string sceneName = Path.GetFileNameWithoutExtension(scenePath);

                SceneSO scene = ScriptableObject.CreateInstance<SceneSO>();
                scene.SceneName = sceneName;
                scene.SceneType = 0;
                string scenesPath = string.Format("{0}/{1}", exportPath, "Scenes");
                if (!Directory.Exists(scenesPath))
                    Directory.CreateDirectory(scenesPath);
                scenesPath = string.Format("{0}/{1}.asset", scenesPath, scene.SceneName);
                UnityEditor.AssetDatabase.DeleteAsset(scenesPath);
                UnityEditor.AssetDatabase.CreateAsset(scene, scenesPath);
                
                SceneCollectionSO sceneCollection = ScriptableObject.CreateInstance<SceneCollectionSO>();
                sceneCollection.collections = new System.Collections.Generic.List<SceneSO>();
                sceneCollection.collections.Add(scene);
                string collectionsPath = string.Format("{0}/{1}", exportPath, "ScenesCollections");
                if (!Directory.Exists(collectionsPath))
                    Directory.CreateDirectory(collectionsPath);
                collectionsPath = string.Format("{0}/{1}.asset", collectionsPath, scene.SceneName);
                UnityEditor.AssetDatabase.DeleteAsset(collectionsPath);
                UnityEditor.AssetDatabase.CreateAsset(sceneCollection, collectionsPath);
            }
            UnityEditor.AssetDatabase.Refresh();
        }
    }
}
