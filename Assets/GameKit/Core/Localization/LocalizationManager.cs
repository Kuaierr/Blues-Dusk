// using GameKit.Resource;
using System;
using System.Collections.Generic;

namespace GameKit.Localization
{
    internal sealed partial class LocalizationManager : GameKitModule, ILocalizationManager
    {
        private readonly Dictionary<string, string> m_Dictionary;
        private readonly DataProvider<ILocalizationManager> m_DataProvider;
        private ILocalizationHelper m_LocalizationHelper;
        private Language m_Language;

        public LocalizationManager()
        {
            m_Dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
            m_DataProvider = new DataProvider<ILocalizationManager>(this);
            m_LocalizationHelper = null;
            m_Language = Language.Unspecified;
        }

        public Language Language
        {
            get
            {
                return m_Language;
            }
            set
            {
                if (value == Language.Unspecified)
                {
                    throw new GameKitException("Language is invalid.");
                }

                m_Language = value;
            }
        }

        public Language SystemLanguage
        {
            get
            {
                if (m_LocalizationHelper == null)
                {
                    throw new GameKitException("You must set localization helper first.");
                }

                return m_LocalizationHelper.SystemLanguage;
            }
        }

        public int DictionaryCount
        {
            get
            {
                return m_Dictionary.Count;
            }
        }

        public int CachedBytesSize
        {
            get
            {
                return DataProvider<ILocalizationManager>.CachedBytesSize;
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

        public void SetDataProviderHelper(IDataProviderHelper<ILocalizationManager> dataProviderHelper)
        {
            m_DataProvider.SetDataProviderHelper(dataProviderHelper);
        }

        public void SetLocalizationHelper(ILocalizationHelper localizationHelper)
        {
            if (localizationHelper == null)
            {
                throw new GameKitException("Localization helper is invalid.");
            }

            m_LocalizationHelper = localizationHelper;
        }

        public void EnsureCachedBytesSize(int ensureSize)
        {
            DataProvider<ILocalizationManager>.EnsureCachedBytesSize(ensureSize);
        }

        public void FreeCachedBytes()
        {
            DataProvider<ILocalizationManager>.FreeCachedBytes();
        }

        public void ReadData(string dictionaryAssetName)
        {
            m_DataProvider.ReadData(dictionaryAssetName);
        }

        public void ReadData(string dictionaryAssetName, int priority)
        {
            m_DataProvider.ReadData(dictionaryAssetName, priority);
        }

        public void ReadExternalData(string dictionaryAssetName, int priority, object userData)
        {
            m_DataProvider.ReadExternalData(dictionaryAssetName, priority, userData);
        }


        public void ReadData(string dictionaryAssetName, object userData)
        {
            m_DataProvider.ReadData(dictionaryAssetName, userData);
        }

        public void ReadData(string dictionaryAssetName, int priority, object userData)
        {
            m_DataProvider.ReadData(dictionaryAssetName, priority, userData);
        }

        public bool ParseData(string dictionaryString)
        {
            return m_DataProvider.ParseData(dictionaryString);
        }

        public bool ParseData(string dictionaryString, object userData)
        {
            return m_DataProvider.ParseData(dictionaryString, userData);
        }

        public bool ParseData(byte[] dictionaryBytes)
        {
            return m_DataProvider.ParseData(dictionaryBytes);
        }

        public bool ParseData(byte[] dictionaryBytes, object userData)
        {
            return m_DataProvider.ParseData(dictionaryBytes, userData);
        }

        public bool ParseData(byte[] dictionaryBytes, int startIndex, int length)
        {
            return m_DataProvider.ParseData(dictionaryBytes, startIndex, length);
        }

        public bool ParseData(byte[] dictionaryBytes, int startIndex, int length, object userData)
        {
            return m_DataProvider.ParseData(dictionaryBytes, startIndex, length, userData);
        }

        public string GetString(string key)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            return value;
        }

        public string GetString<T>(string key, T arg)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg);
            }
            catch (Exception exception)
            {
                return Utility.Text.Format("<Error>{0},{1},{2},{3}", key, value, arg, exception);
            }
        }

        public string GetString<T1, T2>(string key, T1 arg1, T2 arg2)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg1, arg2);
            }
            catch (Exception exception)
            {
                return Utility.Text.Format("<Error>{0},{1},{2},{3},{4}", key, value, arg1, arg2, exception);
            }
        }

        public string GetString<T1, T2, T3>(string key, T1 arg1, T2 arg2, T3 arg3)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg1, arg2, arg3);
            }
            catch (Exception exception)
            {
                return Utility.Text.Format("<Error>{0},{1},{2},{3},{4},{5}", key, value, arg1, arg2, arg3, exception);
            }
        }

        public string GetString<T1, T2, T3, T4>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg1, arg2, arg3, arg4);
            }
            catch (Exception exception)
            {
                return Utility.Text.Format("<Error>{0},{1},{2},{3},{4},{5},{6}", key, value, arg1, arg2, arg3, arg4, exception);
            }
        }

        public string GetString<T1, T2, T3, T4, T5>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            string value = GetRawString(key);
            if (value == null)
            {
                return Utility.Text.Format("<NoKey>{0}", key);
            }

            try
            {
                return Utility.Text.Format(value, arg1, arg2, arg3, arg4, arg5);
            }
            catch (Exception exception)
            {
                return Utility.Text.Format("<Error>{0},{1},{2},{3},{4},{5},{6},{7}", key, value, arg1, arg2, arg3, arg4, arg5, exception);
            }
        }


        public bool HasRawString(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new GameKitException("Key is invalid.");
            }

            return m_Dictionary.ContainsKey(key);
        }

        public string GetRawString(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new GameKitException("Key is invalid.");
            }

            string value = null;
            if (m_Dictionary.TryGetValue(key, out value))
            {
                return value;
            }

            return null;
        }

        public bool AddRawString(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new GameKitException("Key is invalid.");
            }

            if (m_Dictionary.ContainsKey(key))
            {
                return false;
            }

            m_Dictionary.Add(key, value ?? string.Empty);
            return true;
        }

        public bool RemoveRawString(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new GameKitException("Key is invalid.");
            }

            return m_Dictionary.Remove(key);
        }

        public void RemoveAllRawStrings()
        {
            m_Dictionary.Clear();
        }
    }
}
