

using GameKit;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using System.Text;
using UnityEditor.SceneManagement;

namespace UnityGameKit.Editor
{
    internal sealed class SceneInfo
    {
        private const string CustomOptionName = "<None>";
        private readonly string m_Name;
        private SerializedProperty m_SceneName;
        private string[] m_SceneNames;
        private int m_SceneNameIndex;

        public SceneInfo(string name)
        {
            m_Name = name;
            m_SceneName = null;
            m_SceneNames = null;
            m_SceneNameIndex = 0;
        }

        public void Init(SerializedObject serializedObject)
        {
            m_SceneName = serializedObject.FindProperty(Utility.Text.Format("{0}Scene", m_Name));
        }

        public void Draw()
        {
            string displayName = FieldNameForDisplay(m_Name);
            int selectedIndex = EditorGUILayout.Popup(Utility.Text.Format("{0} Scene", displayName), m_SceneNameIndex, m_SceneNames);
            if (selectedIndex != m_SceneNameIndex)
            {
                m_SceneNameIndex = selectedIndex;
                m_SceneName.stringValue = selectedIndex <= 0 ? null : m_SceneNames[selectedIndex];
            }
        }

        public void Refresh()
        {
            List<string> m_TempSceneNames = new List<string>
            {
                CustomOptionName
            };

            foreach (string sceneGuid in AssetDatabase.FindAssets("t:Scene", new string[] { ScenesConfig.GAMEMAIN_PATH }))
            {
                string scenePath = AssetDatabase.GUIDToAssetPath(sceneGuid);
                string sceneName = Path.GetFileNameWithoutExtension(scenePath);
                m_TempSceneNames.Add(sceneName);
            }

            m_SceneNames = m_TempSceneNames.ToArray();
            m_SceneNameIndex = 0;

            if (!string.IsNullOrEmpty(m_SceneName.stringValue))
            {
                m_SceneNameIndex = m_TempSceneNames.IndexOf(m_SceneName.stringValue);
                if (m_SceneNameIndex <= 0)
                {
                    m_SceneNameIndex = 0;
                    m_SceneName.stringValue = null;
                }
            }
        }

        private string FieldNameForDisplay(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                return string.Empty;
            }
            string str = Regex.Replace(fieldName, @"^m_", string.Empty);
            str = Regex.Replace(str, @"((?<=[a-z])[A-Z]|[A-Z](?=[a-z]))", @" $1").TrimStart();
            return str;
        }
    }
}
