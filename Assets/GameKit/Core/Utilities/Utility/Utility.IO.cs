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


            public static string GetRegularPath(string path)
            {
                if (path == null)
                {
                    return null;
                }

                return path.Replace('\\', '/');
            }

            public static string GetRemotePath(string path)
            {
                string regularPath = GetRegularPath(path);
                if (regularPath == null)
                {
                    return null;
                }

                return regularPath.Contains("://") ? regularPath : ("file:///" + regularPath).Replace("file:////", "file:///");
            }

            public static bool RemoveEmptyDirectory(string directoryName)
            {
                if (string.IsNullOrEmpty(directoryName))
                {
                    throw new GameKitException("Directory name is invalid.");
                }

                try
                {
                    if (!Directory.Exists(directoryName))
                    {
                        return false;
                    }

                    // 不使用 SearchOption.AllDirectories，以便于在可能产生异常的环境下删除尽可能多的目录
                    string[] subDirectoryNames = Directory.GetDirectories(directoryName, "*");
                    int subDirectoryCount = subDirectoryNames.Length;
                    foreach (string subDirectoryName in subDirectoryNames)
                    {
                        if (RemoveEmptyDirectory(subDirectoryName))
                        {
                            subDirectoryCount--;
                        }
                    }

                    if (subDirectoryCount > 0)
                    {
                        return false;
                    }

                    if (Directory.GetFiles(directoryName, "*").Length > 0)
                    {
                        return false;
                    }

                    Directory.Delete(directoryName);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
