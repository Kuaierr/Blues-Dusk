using GameKit;
using System;
using System.Text;

namespace UnityGameKit.Runtime
{
    public class DefaultTextHelper : Utility.Text.ITextHelper
    {
        private const int StringBuilderCapacity = 1024;

        [ThreadStatic]
        private static StringBuilder s_CachedStringBuilder = null;

        public string Format<T>(string format, T arg)
        {
            if (format == null)
            {
                throw new GameKitException("Format is invalid.");
            }

            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(format, arg);
            return s_CachedStringBuilder.ToString();
        }

        public string Format<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (format == null)
            {
                throw new GameKitException("Format is invalid.");
            }

            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(format, arg1, arg2);
            return s_CachedStringBuilder.ToString();
        }

        public string Format<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (format == null)
            {
                throw new GameKitException("Format is invalid.");
            }

            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(format, arg1, arg2, arg3);
            return s_CachedStringBuilder.ToString();
        }

        public string Format<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (format == null)
            {
                throw new GameKitException("Format is invalid.");
            }

            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(format, arg1, arg2, arg3, arg4);
            return s_CachedStringBuilder.ToString();
        }

        public string Format<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (format == null)
            {
                throw new GameKitException("Format is invalid.");
            }

            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(format, arg1, arg2, arg3, arg4, arg5);
            return s_CachedStringBuilder.ToString();
        }


        public string Format<T1, T2, T3, T4, T5, T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (format == null)
            {
                throw new GameKitException("Format is invalid.");
            }

            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(format, arg1, arg2, arg3, arg4, arg5, arg6);
            return s_CachedStringBuilder.ToString();
        }

        public string Format(string format, params object[] args)
        {
            if (format == null)
            {
                throw new GameKitException("Format is invalid.");
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
                s_CachedStringBuilder = new StringBuilder(StringBuilderCapacity);
            }
        }
    }
}
