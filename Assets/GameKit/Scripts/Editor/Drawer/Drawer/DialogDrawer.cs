using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;

namespace UnityGameKit.Editor
{
    [CustomPropertyDrawer(typeof(DialogAttribute), false)]
    public class DialogDrawer : PropertyDrawer
    {
        private const string CustomOptionName = "<None>";
        private readonly string m_Name;
        private string[] m_SceneNames;
        private int m_SceneNameIndex;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent content)
        {
            DialogAttribute attr = (attribute as DialogAttribute);

            List<string> m_TempSceneNames = new List<string>
            {
                CustomOptionName
            };

            foreach (string sceneGuid in AssetDatabase.FindAssets("t:TextAsset", new string[] { "Assets/GameMain/Data/Dialog/Text/Raw" }))
            {
                string scenePath = AssetDatabase.GUIDToAssetPath(sceneGuid);
                string sceneName = Path.GetFileNameWithoutExtension(scenePath);
                m_TempSceneNames.Add(sceneName);
            }

            m_SceneNames = m_TempSceneNames.ToArray();
            m_SceneNameIndex = 0;

            if (!string.IsNullOrEmpty(property.stringValue))
            {
                m_SceneNameIndex = m_TempSceneNames.IndexOf(property.stringValue);
                if (m_SceneNameIndex <= 0)
                {
                    m_SceneNameIndex = 0;
                    property.stringValue = null;
                }
            }

            int selectedIndex = EditorGUILayout.Popup(fieldInfo.Name, m_SceneNameIndex, m_SceneNames);

            if (selectedIndex != m_SceneNameIndex)
            {
                m_SceneNameIndex = selectedIndex;
                property.stringValue = selectedIndex <= 0 ? null : m_SceneNames[selectedIndex];
            }

            // EditorGUI.PropertyField(position, property, content);
        }
    }
}