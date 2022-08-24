using GameKit;
using UnityEngine;

namespace UnityGameKit.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("GameKit/GameKit Core Component")]
    public sealed class GameKitCoreComponent : GameKitComponent
    {
        private const int DefaultDpi = 96;  // default windows dpi

        private float m_GameSpeedBeforePause = 1f;

        [SerializeField]
        private bool m_EditorResourceMode = true;

        // [SerializeField]
        // private Language m_EditorLanguage = Language.Unspecified;

        [SerializeField]
        private int m_FrameRate = 30;

        [SerializeField]
        private float m_GameSpeed = 1f;

        [SerializeField]
        private bool m_RunInBackground = true;

        [SerializeField]
        private bool m_NeverSleep = true;
        protected override void Awake()
        {
            base.Awake();

            //             InitVersionHelper();
            //             InitLogHelper();
            //             Log.Info("Game Framework Version: {0}", GameFramework.Version.GameFrameworkVersion);
            //             Log.Info("Game Version: {0} ({1})", GameFramework.Version.GameVersion, GameFramework.Version.InternalGameVersion.ToString());
            //             Log.Info("Unity Version: {0}", Application.unityVersion);

            // #if UNITY_5_3_OR_NEWER || UNITY_5_3
            //             InitZipHelper();
            //             InitJsonHelper();

            //             Utility.Converter.ScreenDpi = Screen.dpi;
            //             if (Utility.Converter.ScreenDpi <= 0)
            //             {
            //                 Utility.Converter.ScreenDpi = DefaultDpi;
            //             }

            //             m_EditorResourceMode &= Application.isEditor;
            //             if (m_EditorResourceMode)
            //             {
            //                 Log.Info("During this run, Game Framework will use editor resource files, which you should validate first.");
            //             }

            //             Application.targetFrameRate = m_FrameRate;
            //             Time.timeScale = m_GameSpeed;
            //             Application.runInBackground = m_RunInBackground;
            //             Screen.sleepTimeout = m_NeverSleep ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;
            // #else
            //             Log.Error("Game Framework only applies with Unity 5.3 and above, but current Unity version is {0}.", Application.unityVersion);
            //             GameEntry.Shutdown(ShutdownType.Quit);
            // #endif
            // #if UNITY_5_6_OR_NEWER
            //             Application.lowMemory += OnLowMemory;
            // #endif
        }

        private void Start()
        {

        }

        private void Update()
        {
            GameKitModuleCenter.Update(Time.deltaTime, Time.unscaledDeltaTime);
        }

        public void Shutdown()
        {

        }
    }
}