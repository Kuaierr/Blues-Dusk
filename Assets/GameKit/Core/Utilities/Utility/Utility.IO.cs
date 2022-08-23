using System.IO;
namespace GameKit
{
    public static partial class Utility
    {
        public static class IO
        {
            public static string AdaptivePath(string parentPath, string subPath)
            {
                string path = Path.Combine(parentPath, subPath);
                string iterPath = "";
                string[] eachDir = subPath.Split('/');
                for (int i = 0; i < eachDir.Length - 1; i++)
                {
                    iterPath = parentPath + Path.Combine(iterPath, eachDir[i]);
                    if (!Directory.Exists(iterPath))
                    {
                        Directory.CreateDirectory(iterPath);
                    }
                }
#if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
#endif
                return path;
            }

            public static string AdaptivePath(string path)
            {
                string[] eachDir = path.Split('/');
                string iterPath = "";
                for (int i = 0; i < eachDir.Length - 1; i++)
                {
                    iterPath = Path.Combine(iterPath, eachDir[i]);
                    if (!Directory.Exists(iterPath))
                        Directory.CreateDirectory(iterPath);
                }
#if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
#endif
                return path;
            }
        }
    }
}
