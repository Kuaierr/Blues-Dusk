namespace GameKit
{
    public static partial class Version
    {
        public interface IVersionHelper
        {
            string GameVersion
            {
                get;
            }

            int InternalGameVersion
            {
                get;
            }
        }
    }
}
