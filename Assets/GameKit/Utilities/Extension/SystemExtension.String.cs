using System.Linq;
using System.Collections.Generic;
namespace GameKit
{
    public static partial class SystemExtension
    {
        public static string Correction(this string str)
        {
            return str.Trim().Replace(((char)13).ToString(), "").Replace('\u200B'.ToString(), "");
        }

        public static string RemoveBracket(this string str)
        {
            return str.Replace("(", "").Replace(")", "");
        }

        public static string[] SafeSplit(this string str, params char[] separator)
        {
            List<string> split = str.Correction().Split(separator).ToList();
            for (int i = 0; i < split.Count; i++)
            {
                int strLen = split[i].Length;
                if (strLen == 0)
                    break;
                if (split[i][strLen - 1] == '\\')
                {
                    if (i != split.Count - 1)
                    {
                        split[i].Remove(strLen - 1);
                        split[i] += split[i + 1];
                        split.RemoveAt(i + 1);
                    }
                }
            }
            return split.ToArray();
        }
    }
}
