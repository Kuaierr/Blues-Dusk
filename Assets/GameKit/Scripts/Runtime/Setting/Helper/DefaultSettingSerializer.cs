using GameKit;

namespace UnityGameKit.Runtime
{
    public sealed class DefaultSettingSerializer : GameKitSerializer<DefaultSetting>
    {
        private static readonly byte[] Header = new byte[] { (byte)'G', (byte)'F', (byte)'S' };

        public DefaultSettingSerializer()
        {

        }

        protected override byte[] GetHeader()
        {
            return Header;
        }
    }
}
