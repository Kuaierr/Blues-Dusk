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
    [CustomEditor(typeof(SettingComponent))]
    internal sealed class SettingComponentInspector : GameKitInspector
    {
        private HelperInfo<SettingHelperBase> m_SettingHelperInfo = new HelperInfo<SettingHelperBase>("Setting");

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();


            SettingComponent t = (SettingComponent)target;

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                m_SettingHelperInfo.Draw();
            }
            EditorGUI.EndDisabledGroup();

            if (EditorApplication.isPlaying && IsPrefabInHierarchy(t.gameObject))
            {
                EditorGUILayout.LabelField("Setting Count", t.Count >= 0 ? t.Count.ToString() : "<Unknown>");
                if (t.Count > 0)
                {
                    string[] settingNames = t.GetAllSettingNames();
                    foreach (string settingName in settingNames)
                    {
                        EditorGUILayout.LabelField(settingName, t.GetString(settingName));
                    }
                }
            }

            if (EditorApplication.isPlaying)
            {
                if (GUILayout.Button("Save Settings"))
                {
                    t.Save();
                }
                if (GUILayout.Button("Remove All Settings"))
                {
                    t.RemoveAllSettings();
                }
            }
            else
            {
                // if (GUILayout.Button("生成游戏编辑器配置表"))
                // {
                //     Debug.Log("dad");
                //     GenerateTable();
                // }
            }

            serializedObject.ApplyModifiedProperties();

            Repaint();
        }

        protected override void OnCompileComplete()
        {
            base.OnCompileComplete();

            RefreshTypeNames();
        }

        private void OnEnable()
        {
            m_SettingHelperInfo.Init(serializedObject);

            RefreshTypeNames();
        }

        private void RefreshTypeNames()
        {
            m_SettingHelperInfo.Refresh();
            serializedObject.ApplyModifiedProperties();
            // GenerateTable();
        }

        private void GenerateTable()
        {
            List<string> tmpDialogTreeNames = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();
            TextAsset GameConfigs = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/LubanGen/Datas/json/datatable_tbgameconfig.json");
            string setting = GameConfigs.text.RemoveBrackets().RemoveEmptySpaceLine().Correction();
            Debug.Log(setting);
            // foreach (string guid in AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/LubanGen/Datas/json/datatable_tbgameconfig.json"))
            // {
            //     string path = AssetDatabase.GUIDToAssetPath(guid);
            //     string name = Path.GetFileNameWithoutExtension(path);
            //     TextAsset asset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
            //     string[] lines = asset.text.Replace(((char)13).ToString(), "").Replace("\t", "").Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

            //     for (int i = 0; i < lines.Length; i++)
            //     {
            //         string nodeInfo = Regex.Match(lines[i], @"(?i)(?<=\[)(.*)(?=\])").Value.Trim();
            //         string semanic = nodeInfo.Split('-').First().Correction();
            //         string value = nodeInfo.Split('-').Last().Correction();

            //         if (semanic == "title" && value != "")
            //         {
            //             stringBuilder.Append(string.Format("({0}){1}", name, value));
            //             stringBuilder.Append(",");
            //         }
            //     }
            // }
            // File.WriteAllText("Assets/GameMain/Configs/DialogCollection.txt", stringBuilder.ToString().RemoveLast());
            // AssetDatabase.Refresh();
        }
    }
}
