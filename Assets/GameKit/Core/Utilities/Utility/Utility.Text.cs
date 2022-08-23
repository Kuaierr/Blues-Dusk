using System;
using System.Text;

namespace GameKit
{
    public static partial class Utility
    {
        public static class Text
        {
            [ThreadStatic]
            private static StringBuilder s_CachedStringBuilder = null;
            public static string Format(string format, object arg0)
            {
                if (format == null)
                {
                    throw new GameKitException("Format is invalid.");
                }

                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendFormat(format, arg0);
                return s_CachedStringBuilder.ToString();
            }

            public static string Combine(string str1, string str2)
            {
                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendLine(str1);
                s_CachedStringBuilder.AppendLine(str2);
                return s_CachedStringBuilder.ToString();
            }

            public static string Format(string format, object arg0, object arg1)
            {
                if (format == null)
                {
                    throw new GameKitException("Format is invalid.");
                }

                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendFormat(format, arg0, arg1);
                return s_CachedStringBuilder.ToString();
            }

            public static string Format(string format, object arg0, object arg1, object arg2)
            {
                if (format == null)
                {
                    throw new GameKitException("Format is invalid.");
                }

                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendFormat(format, arg0, arg1, arg2);
                return s_CachedStringBuilder.ToString();
            }

            public static string Format(string format, params object[] args)
            {
                if (format == null)
                {
                    throw new GameKitException("Format is invalid.");
                }

                if (args == null)
                {
                    throw new GameKitException("Args is invalid.");
                }

                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendFormat(format, args);
                return s_CachedStringBuilder.ToString();
            }

            private static void CheckCachedStringBuilder()
            {
                if (s_CachedStringBuilder == null)
                {
                    s_CachedStringBuilder = new StringBuilder(1024);
                }
            }
        }
    }
}
