

using GameKit;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
namespace UnityGameKit.Editor
{
    internal sealed class PathInfo
    {
        private const string DefaultPath = "Assets/GameMain/";
        private const int DefaultButtonWidth = 60;
        private const int DefaultPrefixWidth = 150;
        private readonly string m_PropertyName;
        private SerializedProperty m_Property;
        private SerializedObject m_SerializedObject;
        private string m_PathString;
        private bool m_DisableUpdatePath;

        public PathInfo(string name)
        {
            m_PropertyName = name;
            m_Property = null;
            m_PathString = string.Empty;
            m_DisableUpdatePath = false;
        }

        public void Init(SerializedObject serializedObject)
        {
            m_SerializedObject = serializedObject;
            m_Property = serializedObject.FindProperty(m_PropertyName);
            m_PathString = m_Property.stringValue;
        }

        public void Draw()
        {
            string displayName = FieldNameForDisplay(m_PropertyName);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(displayName, GUILayout.Width(DefaultPrefixWidth));
            EditorGUI.BeginDisabledGroup(m_DisableUpdatePath);
            {
                m_PathString = EditorGUILayout.TextField(m_PathString);
                m_Property.stringValue = m_PathString;
            }
            EditorGUI.EndDisabledGroup();
            if (GUILayout.Button("Update", GUILayout.Width(DefaultButtonWidth)))
            {
                EnableUpdatePath();
            }
            EditorGUILayout.EndHorizontal();

        }

        public void Refresh()
        {
            m_DisableUpdatePath = true;
            
            if (m_PathString == string.Empty)
            {
                m_PathString = DefaultPath;
            }
            m_Property.stringValue = m_PathString;
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

        private void EnableUpdatePath()
        {
            m_DisableUpdatePath = false;
        }
    }
}
