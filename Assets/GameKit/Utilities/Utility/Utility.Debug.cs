using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace GameKit
{
    public static partial class Utility
    {
        public static class Debug
        {
            public static void Log(string info) => Debug.Log(info);
            public static void LogWarning(string info) => Debug.LogWarning(info);
            public static void LogError(string info) => Debug.LogError(info);
            public static void LogSuccess(string info) => Debug.Log(Utility.Text.Format("<color=green>[Success]</color> {0}", info));
            public static void LogFail(string info) => Debug.Log(Utility.Text.Format("<color=red>[Failed]</color> {0}", info));
        }
    }
}

