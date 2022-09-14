

using GameKit;
using UnityEngine;
using GameKit.Element;
using UnityEditor;
using UnityGameKit.Runtime;

namespace UnityGameKit.Editor
{
    [CustomEditor(typeof(ElementComponent))]
    internal sealed class ElementComponentInspector : GameKitInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!EditorApplication.isPlaying)
            {
                EditorGUILayout.HelpBox("Available during runtime only.", MessageType.Info);
                return;
            }

            serializedObject.Update();

            ElementComponent t = (ElementComponent)target;
            if (EditorApplication.isPlaying && IsPrefabInHierarchy(t.gameObject))
            {
                EditorGUILayout.LabelField("Element Count ", t.ElementCount.ToString());
                IElement[] elements = t.GetAllElements();
                for (int i = 0; i < elements.Length; i++)
                {
                    if (GUILayout.Button(elements[i].Name))
                    {
                        elements[i].OnInteract();
                    }
                }
            }
            serializedObject.ApplyModifiedProperties();
            Repaint();
        }

        protected override void OnCompileComplete()
        {
            base.OnCompileComplete();
            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}
