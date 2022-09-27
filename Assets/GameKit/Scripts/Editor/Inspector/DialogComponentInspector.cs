using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using GameKit;
using UnityGameKit.Runtime;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
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
            else
            {
                if (GUILayout.Button("生成对话编辑器配置表"))
                {
                    GenerateTable();
                }
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
            GenerateTable();
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


        private void GenerateTable()
        {

            List<string> tmpDialogTreeNames = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string guid in AssetDatabase.FindAssets("t:TextAsset", new string[] { "Assets/GameMain/Data/Dialog/Text/Raw" }))
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                string name = Path.GetFileNameWithoutExtension(path);
                TextAsset asset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                string[] lines = asset.text.Replace(((char)13).ToString(), "").Replace("\t", "").Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < lines.Length; i++)
                {
                    string nodeInfo = Regex.Match(lines[i], @"(?i)(?<=\[)(.*)(?=\])").Value.Trim();
                    string semanic = nodeInfo.Split('-').First().Correction();
                    string value = nodeInfo.Split('-').Last().Correction();

                    if (semanic == "title" && value != "")
                    {
                        stringBuilder.Append(string.Format("({0}){1}", name, value));
                        stringBuilder.Append(",");
                    }
                }
            }
            File.WriteAllText("Assets/GameMain/Configs/DialogCollection.txt", stringBuilder.ToString().RemoveLast());
            // TextAsset textAsset = new TextAsset(stringBuilder.ToString());
            // UnityEditor.AssetDatabase.CreateAsset(textAsset, "Assets/GameMain/Data/Dialog/Text/Configs/DialogCollection.txt");
            // UnityEditor.AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
