

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
    internal sealed class EnumInfo
    {
        private readonly string m_PropertyName;
        private SerializedProperty m_EnumProperty;
        private string[] m_EnumNames;
        private int m_EnumIndex;

        public EnumInfo(string name)
        {
            m_PropertyName = name;
            m_EnumProperty = null;
            m_EnumNames = null;
            m_EnumIndex = 0;
        }

        public void Init(SerializedObject serializedObject)
        {
            m_EnumProperty = serializedObject.FindProperty(m_PropertyName);

        }

        public void Draw()
        {
            string displayName = FieldNameForDisplay(m_PropertyName);
            int selectedIndex = EditorGUILayout.Popup(displayName, m_EnumIndex, m_EnumNames);
            if (selectedIndex != m_EnumIndex)
            {
                m_EnumIndex = selectedIndex;
                m_EnumProperty.enumValueIndex = selectedIndex <= 0 ? 0 : selectedIndex;
            }
        }

        public void Refresh()
        {
            List<string> m_TempEnumNames = new List<string>();
            for (int i = 0; i < m_EnumProperty.enumNames.Length; i++)
                m_TempEnumNames.Add(m_EnumProperty.enumNames[i]);
            m_EnumNames = m_TempEnumNames.ToArray();
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
