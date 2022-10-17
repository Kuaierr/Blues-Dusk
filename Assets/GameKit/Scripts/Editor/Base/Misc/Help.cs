using UnityEditor;
using UnityEngine;

namespace UnityGameKit.Editor
{
    /// <summary>
    /// 帮助相关的实用函数。
    /// </summary>
    public static class Help
    {
        [MenuItem("Game Kit/Documentation", false, 90)]
        public static void ShowDocumentation()
        {
            ShowHelp("https://gameframework.cn/document/");
        }

        [MenuItem("Game Kit/API Reference", false, 91)]
        public static void ShowApiReference()
        {
            ShowHelp("https://gameframework.cn/api/");
        }

        private static void ShowHelp(string uri)
        {
            Application.OpenURL(uri);
        }
    }
}
