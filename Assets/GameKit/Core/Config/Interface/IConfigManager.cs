// using GameKit.Resource;

namespace GameKit.Config
{
    public interface IConfigManager : IDataProvider<IConfigManager>
    {
        int Count
        {
            get;
        }

        int CachedBytesSize
        {
            get;
        }

        // void SetResourceManager(IResourceManager resourceManager);

        void SetDataProviderHelper(IDataProviderHelper<IConfigManager> dataProviderHelper);

        void SetConfigHelper(IConfigHelper configHelper);

        void EnsureCachedBytesSize(int ensureSize);

        void FreeCachedBytes();

        bool HasConfig(string configName);

        bool GetBool(string configName);

        bool GetBool(string configName, bool defaultValue);

        int GetInt(string configName);

        int GetInt(string configName, int defaultValue);

        float GetFloat(string configName);

        float GetFloat(string configName, float defaultValue);

        string GetString(string configName);

        string GetString(string configName, string defaultValue);

        bool AddConfig(string configName, string configValue);

        bool AddConfig(string configName, bool boolValue, int intValue, float floatValue, string stringValue);

        bool RemoveConfig(string configName);

        void RemoveAllConfigs();
    }
}
