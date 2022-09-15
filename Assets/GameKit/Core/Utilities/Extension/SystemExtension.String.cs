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

        public static string RemoveBrackets(this string str)
        {
            return str.RemoveCurlyBrackets().RemoveParentheses().RemoveSquareBrackets();
        }

        public static string RemoveCurlyBrackets(this string str)
        {
            return str.Replace("{", "").Replace("}", "");
        }

        public static string RemoveParentheses(this string str)
        {
            return str.Replace("(", "").Replace(")", "");
        }

        public static string RemoveSquareBrackets(this string str)
        {
            return str.Replace("[", "").Replace("]", "");
        }

        public static string RemoveEmptySpaceLine(this string str)
        {
            return str.Replace(" ", "").Replace("\n", "").Replace("\t", "");
        }

        public static string RemoveLast(this string str)
        {
            return str.Remove(str.Length - 1);
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

        public static string ReadLine(this string rawString, ref int position)
        {
            if (position < 0)
            {
                return null;
            }

            int length = rawString.Length;
            int offset = position;
            while (offset < length)
            {
                char ch = rawString[offset];
                switch (ch)
                {
                    case '\r':
                    case '\n':
                        if (offset > position)
                        {
                            string line = rawString.Substring(position, offset - position);
                            position = offset + 1;
                            if ((ch == '\r') && (position < length) && (rawString[position] == '\n'))
                            {
                                position++;
                            }

                            return line;
                        }

                        offset++;
                        position++;
                        break;

                    default:
                        offset++;
                        break;
                }
            }

            if (offset > position)
            {
                string line = rawString.Substring(position, offset - position);
                position = offset;
                return line;
            }

            return null;
        }
    }
}
