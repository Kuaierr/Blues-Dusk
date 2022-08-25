// using GameKit.Resource;
using System;
using System.Collections.Generic;

namespace GameKit.Config
{
    internal sealed partial class ConfigManager : GameKitModule, IConfigManager
    {
        private readonly Dictionary<string, ConfigData> m_ConfigDatas;
        private readonly DataProvider<IConfigManager> m_DataProvider;
        private IConfigHelper m_ConfigHelper;

        public ConfigManager()
        {
            m_ConfigDatas = new Dictionary<string, ConfigData>(StringComparer.Ordinal);
            m_DataProvider = new DataProvider<IConfigManager>(this);
            m_ConfigHelper = null;
        }

        public int Count
        {
            get
            {
                return m_ConfigDatas.Count;
            }
        }

        public int CachedBytesSize
        {
            get
            {
                return DataProvider<IConfigManager>.CachedBytesSize;
            }
        }

        public event EventHandler<ReadDataSuccessEventArgs> ReadDataSuccess
        {
            add
            {
                m_DataProvider.ReadDataSuccess += value;
            }
            remove
            {
                m_DataProvider.ReadDataSuccess -= value;
            }
        }

        public event EventHandler<ReadDataFailureEventArgs> ReadDataFailure
        {
            add
            {
                m_DataProvider.ReadDataFailure += value;
            }
            remove
            {
                m_DataProvider.ReadDataFailure -= value;
            }
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        internal override void Shutdown()
        {
        }

        // public void SetResourceManager(IResourceManager resourceManager)
        // {
        //     m_DataProvider.SetResourceManager(resourceManager);
        // }

        public void SetDataProviderHelper(IDataProviderHelper<IConfigManager> dataProviderHelper)
        {
            m_DataProvider.SetDataProviderHelper(dataProviderHelper);
        }

        public void SetConfigHelper(IConfigHelper configHelper)
        {
            if (configHelper == null)
            {
                throw new GameKitException("Config helper is invalid.");
            }

            m_ConfigHelper = configHelper;
        }

        public void EnsureCachedBytesSize(int ensureSize)
        {
            DataProvider<IConfigManager>.EnsureCachedBytesSize(ensureSize);
        }

        public void FreeCachedBytes()
        {
            DataProvider<IConfigManager>.FreeCachedBytes();
        }

        public void ReadData(string configAssetName)
        {
            m_DataProvider.ReadData(configAssetName);
        }

        public void ReadData(string configAssetName, int priority)
        {
            m_DataProvider.ReadData(configAssetName, priority);
        }

        public void ReadData(string configAssetName, object userData)
        {
            m_DataProvider.ReadData(configAssetName, userData);
        }

        public void ReadData(string configAssetName, int priority, object userData)
        {
            m_DataProvider.ReadData(configAssetName, priority, userData);
        }

        public bool ParseData(string configString)
        {
            return m_DataProvider.ParseData(configString);
        }

        public bool ParseData(string configString, object userData)
        {
            return m_DataProvider.ParseData(configString, userData);
        }

        public bool ParseData(byte[] configBytes)
        {
            return m_DataProvider.ParseData(configBytes);
        }

        public bool ParseData(byte[] configBytes, object userData)
        {
            return m_DataProvider.ParseData(configBytes, userData);
        }

        public bool ParseData(byte[] configBytes, int startIndex, int length)
        {
            return m_DataProvider.ParseData(configBytes, startIndex, length);
        }

        public bool ParseData(byte[] configBytes, int startIndex, int length, object userData)
        {
            return m_DataProvider.ParseData(configBytes, startIndex, length, userData);
        }

        public bool HasConfig(string configName)
        {
            return GetConfigData(configName).HasValue;
        }

        public bool GetBool(string configName)
        {
            ConfigData? configData = GetConfigData(configName);
            if (!configData.HasValue)
            {
                throw new GameKitException(Utility.Text.Format("Config name '{0}' is not exist.", configName));
            }

            return configData.Value.BoolValue;
        }

        public bool GetBool(string configName, bool defaultValue)
        {
            ConfigData? configData = GetConfigData(configName);
            return configData.HasValue ? configData.Value.BoolValue : defaultValue;
        }

        public int GetInt(string configName)
        {
            ConfigData? configData = GetConfigData(configName);
            if (!configData.HasValue)
            {
                throw new GameKitException(Utility.Text.Format("Config name '{0}' is not exist.", configName));
            }

            return configData.Value.IntValue;
        }

        public int GetInt(string configName, int defaultValue)
        {
            ConfigData? configData = GetConfigData(configName);
            return configData.HasValue ? configData.Value.IntValue : defaultValue;
        }

        public float GetFloat(string configName)
        {
            ConfigData? configData = GetConfigData(configName);
            if (!configData.HasValue)
            {
                throw new GameKitException(Utility.Text.Format("Config name '{0}' is not exist.", configName));
            }

            return configData.Value.FloatValue;
        }

        public float GetFloat(string configName, float defaultValue)
        {
            ConfigData? configData = GetConfigData(configName);
            return configData.HasValue ? configData.Value.FloatValue : defaultValue;
        }

        public string GetString(string configName)
        {
            ConfigData? configData = GetConfigData(configName);
            if (!configData.HasValue)
            {
                throw new GameKitException(Utility.Text.Format("Config name '{0}' is not exist.", configName));
            }

            return configData.Value.StringValue;
        }

        public string GetString(string configName, string defaultValue)
        {
            ConfigData? configData = GetConfigData(configName);
            return configData.HasValue ? configData.Value.StringValue : defaultValue;
        }

        public bool AddConfig(string configName, string configValue)
        {
            bool boolValue = false;
            bool.TryParse(configValue, out boolValue);

            int intValue = 0;
            int.TryParse(configValue, out intValue);

            float floatValue = 0f;
            float.TryParse(configValue, out floatValue);

            return AddConfig(configName, boolValue, intValue, floatValue, configValue);
        }

        public bool AddConfig(string configName, bool boolValue, int intValue, float floatValue, string stringValue)
        {
            if (HasConfig(configName))
            {
                return false;
            }

            m_ConfigDatas.Add(configName, new ConfigData(boolValue, intValue, floatValue, stringValue));
            return true;
        }

        public bool RemoveConfig(string configName)
        {
            if (!HasConfig(configName))
            {
                return false;
            }

            return m_ConfigDatas.Remove(configName);
        }

        public void RemoveAllConfigs()
        {
            m_ConfigDatas.Clear();
        }

        private ConfigData? GetConfigData(string configName)
        {
            if (string.IsNullOrEmpty(configName))
            {
                throw new GameKitException("Config name is invalid.");
            }

            ConfigData configData = default(ConfigData);
            if (m_ConfigDatas.TryGetValue(configName, out configData))
            {
                return configData;
            }

            return null;
        }
    }
}
