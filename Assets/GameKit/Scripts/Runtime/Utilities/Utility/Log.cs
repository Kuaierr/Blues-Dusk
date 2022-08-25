//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameKit;
using System.Diagnostics;

namespace UnityGameKit.Runtime
{
    public static class Log
    {
        public static void Success(object message)
        {
            GameKitLog.Success(message);
        }

        public static void Success(string message)
        {
            GameKitLog.Success(message);
        }

        public static void Success<T>(string format, T arg)
        {
            GameKitLog.Success(format, arg);
        }

        public static void Success<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            GameKitLog.Success(format, arg1, arg2);
        }

        public static void Success<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            GameKitLog.Success(format, arg1, arg2, arg3);
        }

        public static void Success<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            GameKitLog.Success(format, arg1, arg2, arg3, arg4);
        }

        public static void Success<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            GameKitLog.Success(format, arg1, arg2, arg3, arg4, arg5);
        }

        public static void Fail(object message)
        {
            GameKitLog.Fail(message);
        }

        public static void Fail(string message)
        {
            GameKitLog.Fail(message);
        }

        public static void Fail<T>(string format, T arg)
        {
            GameKitLog.Fail(format, arg);
        }

        public static void Fail<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            GameKitLog.Fail(format, arg1, arg2);
        }

        public static void Fail<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            GameKitLog.Fail(format, arg1, arg2, arg3);
        }

        public static void Fail<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            GameKitLog.Fail(format, arg1, arg2, arg3, arg4);
        }

        public static void Fail<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            GameKitLog.Fail(format, arg1, arg2, arg3, arg4, arg5);
        }

        public static void Info(object message)
        {
            GameKitLog.Info(message);
        }

        public static void Info(string message)
        {
            GameKitLog.Info(message);
        }

        public static void Info<T>(string format, T arg)
        {
            GameKitLog.Info(format, arg);
        }

        public static void Info<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            GameKitLog.Info(format, arg1, arg2);
        }

        public static void Info<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            GameKitLog.Info(format, arg1, arg2, arg3);
        }

        public static void Info<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            GameKitLog.Info(format, arg1, arg2, arg3, arg4);
        }

        public static void Info<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            GameKitLog.Info(format, arg1, arg2, arg3, arg4, arg5);
        }

        public static void Warning(object message)
        {
            GameKitLog.Warning(message);
        }

        public static void Warning(string message)
        {
            GameKitLog.Warning(message);
        }

        public static void Warning<T>(string format, T arg)
        {
            GameKitLog.Warning(format, arg);
        }

        public static void Warning<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            GameKitLog.Warning(format, arg1, arg2);
        }

        public static void Warning<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            GameKitLog.Warning(format, arg1, arg2, arg3);
        }

        public static void Warning<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            GameKitLog.Warning(format, arg1, arg2, arg3, arg4);
        }

        public static void Warning<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            GameKitLog.Warning(format, arg1, arg2, arg3, arg4, arg5);
        }

        public static void Error(object message)
        {
            GameKitLog.Error(message);
        }

        public static void Error(string message)
        {
            GameKitLog.Error(message);
        }

        public static void Error<T>(string format, T arg)
        {
            GameKitLog.Error(format, arg);
        }

        public static void Error<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            GameKitLog.Error(format, arg1, arg2);
        }

        public static void Error<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            GameKitLog.Error(format, arg1, arg2, arg3);
        }

        public static void Error<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            GameKitLog.Error(format, arg1, arg2, arg3, arg4);
        }

        public static void Error<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            GameKitLog.Error(format, arg1, arg2, arg3, arg4, arg5);
        }

        public static void Fatal(object message)
        {
            GameKitLog.Fatal(message);
        }

        public static void Fatal(string message)
        {
            GameKitLog.Fatal(message);
        }

        public static void Fatal<T>(string format, T arg)
        {
            GameKitLog.Fatal(format, arg);
        }

        public static void Fatal<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            GameKitLog.Fatal(format, arg1, arg2);
        }

        public static void Fatal<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            GameKitLog.Fatal(format, arg1, arg2, arg3);
        }

        public static void Fatal<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            GameKitLog.Fatal(format, arg1, arg2, arg3, arg4);
        }

        public static void Fatal<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            GameKitLog.Fatal(format, arg1, arg2, arg3, arg4, arg5);
        }

        #region Params Args
        public static void Fatal(string format, params object[] args)
        {
            GameKitLog.Fatal(format, args);
        }

        public static void Fail(string format, params object[] args)
        {
            GameKitLog.Fail(format, args);
        }

        public static void Success(string format, params object[] args)
        {
            GameKitLog.Success(format, args);
        }

        public static void Info(string format, params object[] args)
        {
            GameKitLog.Info(format, args);
        }
        public static void Warning(string format, params object[] args)
        {
            GameKitLog.Warning(format, args);
        }

        public static void Error(string format, params object[] args)
        {
            GameKitLog.Error(format, args);
        }
        #endregion
    }
}
