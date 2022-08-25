//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameKit
{
    public static partial class GameKitLog
    {
        private static ILogHelper s_LogHelper = null;

        public static void SetLogHelper(ILogHelper logHelper)
        {
            s_LogHelper = logHelper;
        }

        // SegPoint

        public static void Info(object message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Info, message);
        }

        public static void Info(string message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Info, message);
        }

        public static void Info<T>(string format, T arg)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Info, Utility.Text.Format(format, arg));
        }

        public static void Info<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Info, Utility.Text.Format(format, arg1, arg2));
        }

        public static void Info<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Info, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        public static void Info<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        public static void Info<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Info, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        // SegPoint

        public static void Warning(object message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Warning, message);
        }

        public static void Warning(string message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Warning, message);
        }

        public static void Warning<T>(string format, T arg)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Warning, Utility.Text.Format(format, arg));
        }

        public static void Warning<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Warning, Utility.Text.Format(format, arg1, arg2));
        }

        public static void Warning<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Warning, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        public static void Warning<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        public static void Warning<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Warning, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        // SegPoint

        public static void Error(object message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Error, message);
        }

        public static void Error(string message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Error, message);
        }

        public static void Error<T>(string format, T arg)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Error, Utility.Text.Format(format, arg));
        }

        public static void Error<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Error, Utility.Text.Format(format, arg1, arg2));
        }

        public static void Error<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Error, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        public static void Error<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        public static void Error<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Error, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        // SegPoint

        public static void Fatal(object message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Fatal, message);
        }

        public static void Fatal(string message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Fatal, message);
        }

        public static void Fatal<T>(string format, T arg)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Fatal, Utility.Text.Format(format, arg));
        }

        public static void Fatal<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Fatal, Utility.Text.Format(format, arg1, arg2));
        }

        public static void Fatal<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Fatal, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        public static void Fatal<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        public static void Fatal<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Fatal, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        // SegPoint

        public static void Success(object message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Success, message);
        }

        public static void Success(string message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Success, message);
        }

        public static void Success<T>(string format, T arg)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Success, Utility.Text.Format(format, arg));
        }

        public static void Success<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Success, Utility.Text.Format(format, arg1, arg2));
        }

        public static void Success<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Success, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        public static void Success<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Success, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        public static void Success<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Success, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        // SegPoint

        public static void Fail(object message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Fail, message);
        }

        public static void Fail(string message)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Fail, message);
        }

        public static void Fail<T>(string format, T arg)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Fail, Utility.Text.Format(format, arg));
        }

        public static void Fail<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Fail, Utility.Text.Format(format, arg1, arg2));
        }

        public static void Fail<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Fail, Utility.Text.Format(format, arg1, arg2, arg3));
        }

        public static void Fail<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Fail, Utility.Text.Format(format, arg1, arg2, arg3, arg4));
        }

        public static void Fail<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (s_LogHelper == null)
            {
                return;
            }

            s_LogHelper.Log(GameKitLogType.Fail, Utility.Text.Format(format, arg1, arg2, arg3, arg4, arg5));
        }

        // SegPoint

        #region Params Args
        public static void Fatal(string format, params object[] args)
        {
            if (s_LogHelper == null)
            {
                return;
            }
            s_LogHelper.Log(GameKitLogType.Fatal, Utility.Text.Format(format, args));
        }

        public static void Success(string format, params object[] args)
        {
            if (s_LogHelper == null)
            {
                return;
            }
            s_LogHelper.Log(GameKitLogType.Success, Utility.Text.Format(format, args));
        }

        public static void Fail(string format, params object[] args)
        {
            if (s_LogHelper == null)
            {
                return;
            }
            s_LogHelper.Log(GameKitLogType.Fail, Utility.Text.Format(format, args));
        }

        public static void Error(string format, params object[] args)
        {
            if (s_LogHelper == null)
            {
                return;
            }
            s_LogHelper.Log(GameKitLogType.Error, Utility.Text.Format(format, args));
        }

        public static void Warning(string format, params object[] args)
        {
            if (s_LogHelper == null)
            {
                return;
            }
            s_LogHelper.Log(GameKitLogType.Warning, Utility.Text.Format(format, args));
        }

        public static void Info(string format, params object[] args)
        {
            if (s_LogHelper == null)
            {
                return;
            }
            s_LogHelper.Log(GameKitLogType.Info, Utility.Text.Format(format, args));
        }

        #endregion
    }
}
