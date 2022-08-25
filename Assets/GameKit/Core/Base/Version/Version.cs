namespace GameKit
{
    public static partial class Version
    {
        private const string GameKitVersionString = "20220925_v0.1";

        private static IVersionHelper s_VersionHelper = null;

        public static string GameKitVersion
        {
            get
            {
                return GameKitVersionString;
            }
        }

        public static string GameVersion
        {
            get
            {
                if (s_VersionHelper == null)
                {
                    return string.Empty;
                }

                return s_VersionHelper.GameVersion;
            }
        }

        public static int InternalGameVersion
        {
            get
            {
                if (s_VersionHelper == null)
                {
                    return 0;
                }

                return s_VersionHelper.InternalGameVersion;
            }
        }

        public static void SetVersionHelper(IVersionHelper versionHelper)
        {
            s_VersionHelper = versionHelper;
        }
    }
}
