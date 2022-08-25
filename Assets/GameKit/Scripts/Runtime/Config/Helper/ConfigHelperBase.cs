using GameKit;
using GameKit.Config;
using UnityEngine;

namespace UnityGameKit.Runtime
{
    public abstract class ConfigHelperBase : MonoBehaviour, IDataProviderHelper<IConfigManager>, IConfigHelper
    {
        public abstract bool ReadData(IConfigManager configManager, string configAssetName, object configAsset, object userData);

        public abstract bool ReadData(IConfigManager configManager, string configAssetName, byte[] configBytes, int startIndex, int length, object userData);

        public abstract bool ParseData(IConfigManager configManager, string configString, object userData);

        public abstract bool ParseData(IConfigManager configManager, byte[] configBytes, int startIndex, int length, object userData);

        public abstract void ReleaseDataAsset(IConfigManager configManager, object configAsset);
    }
}
