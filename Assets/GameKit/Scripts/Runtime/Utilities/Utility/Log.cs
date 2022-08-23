using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using UnityEngine;

namespace UnityGameKit.Runtime
{
    public static partial class Log
    {
        [ThreadStatic]
        private static StringBuilder s_CachedStringBuilder = null;
        public static void Info(object info) => Debug.Log(info);
        public static void Warning(object info) => Debug.Log(string.Format("<b><color=orange>[Warning]</color></b> {0}", info));
        public static void Error(object info) => Debug.LogError(string.Format("<b><color=red>[Error]</color></b> {0}", info));
        public static void Success(object info) => Debug.Log(string.Format("<b><color=green>[Success]</color></b> {0}", info));
        public static void Fatal(object info) => Debug.Log(string.Format("<b><color=red>[Failed]</color></b> {0}", info));

        public static void Warning(object info, object arg0)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0);
            Warning(s_CachedStringBuilder.ToString());
        }

        public static void Warning(object info, object arg0, object arg1)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0, arg1);
            Warning(s_CachedStringBuilder.ToString());
        }

        public static void Warning(object info, object arg0, object arg1, object arg2)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0, arg1, arg2);
            Warning(s_CachedStringBuilder.ToString());
        }

        public static void Warning(object info, object arg0, object arg1, object arg2, object arg3)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0, arg1, arg2, arg3);
            Warning(s_CachedStringBuilder.ToString());
        }
        public static void Warning(object info, object arg0, object arg1, object arg2, object arg3, object arg4)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0, arg1, arg2, arg3, arg4);
            Warning(s_CachedStringBuilder.ToString());
        }

        public static void Error(object info, object arg0)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0);
            Error(s_CachedStringBuilder.ToString());
        }

        public static void Error(object info, object arg0, object arg1)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0, arg1);
            Error(s_CachedStringBuilder.ToString());
        }

        public static void Error(object info, object arg0, object arg1, object arg2)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0, arg1, arg2);
            Error(s_CachedStringBuilder.ToString());
        }

        public static void Error(object info, object arg0, object arg1, object arg2, object arg3)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0, arg1, arg2, arg3);
            Error(s_CachedStringBuilder.ToString());
        }

        public static void Error(object info, object arg0, object arg1, object arg2, object arg3, object arg4)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0, arg1, arg2, arg3, arg4);
            Error(s_CachedStringBuilder.ToString());
        }

        public static void Info(object info, object arg0)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0);
            Info(s_CachedStringBuilder.ToString());
        }

        public static void Info(object info, object arg0, object arg1)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0, arg1);
            Info(s_CachedStringBuilder.ToString());
        }

        public static void Info(object info, object arg0, object arg1, object arg2)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0, arg1, arg2);
            Info(s_CachedStringBuilder.ToString());
        }

        public static void Info(object info, object arg0, object arg1, object arg2, object arg3)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0, arg1, arg2, arg3);
            Info(s_CachedStringBuilder.ToString());
        }

        public static void Info(object info, object arg0, object arg1, object arg2, object arg3, object arg4)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0, arg1, arg2, arg3, arg4);
            Info(s_CachedStringBuilder.ToString());
        }

        public static void Success(object info, object arg0)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0);
            Success(s_CachedStringBuilder.ToString());
        }

        public static void Success(object info, object arg0, object arg1)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0, arg1);
            Success(s_CachedStringBuilder.ToString());
        }

        public static void Success(object info, object arg0, object arg1, object arg2)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0, arg1, arg2);
            Success(s_CachedStringBuilder.ToString());
        }

        public static void Success(object info, object arg0, object arg1, object arg2, object arg3)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0, arg1, arg2, arg3);
            Success(s_CachedStringBuilder.ToString());
        }

        public static void Success(object info, object arg0, object arg1, object arg2, object arg3, object arg4)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0, arg1, arg2, arg3, arg4);
            Success(s_CachedStringBuilder.ToString());
        }

        public static void Fatal(object info, object arg0)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0);
            Fatal(s_CachedStringBuilder.ToString());
        }

        public static void Fatal(object info, object arg0, object arg1)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0, arg1);
            Fatal(s_CachedStringBuilder.ToString());
        }

        public static void Fatal(object info, object arg0, object arg1, object arg2)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0, arg1, arg2);
            Fatal(s_CachedStringBuilder.ToString());
        }

        public static void Fatal(object info, object arg0, object arg1, object arg2, object arg3)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0, arg1, arg2, arg3);
            Fatal(s_CachedStringBuilder.ToString());
        }

        public static void Fatal(object info, object arg0, object arg1, object arg2, object arg3, object arg4)
        {
            CheckCachedStringBuilder();
            s_CachedStringBuilder.Length = 0;
            s_CachedStringBuilder.AppendFormat(info.ToString(), arg0, arg1, arg2, arg3, arg4);
            Fatal(s_CachedStringBuilder.ToString());
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

